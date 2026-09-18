using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator losaAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D box2d;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float damageStaggerDuration = 1.5f;
    [SerializeField] float damageFlashInterval = 0.1f;
    [SerializeField] Color damageFlashColor = Color.gray;
    [SerializeField] float deathColliderHeight = 1.5f;

    Color normalColor;

    //Controller Variable WASD
    float keyHorizontal;
    bool keyJump;
    
    //Control Variable (Validations)
    bool isGrounded;
    bool isInvincible;
    bool isFacingright;

    bool hitSideRight;

    float shootTime;
    bool keyShootRelease;

    public int currentHealth;
    public int MaxHealth = 3;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box2d = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;

        //sprites face right by default
        isFacingright = true;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirectionInput();
        PlayerJumpInput();
        PlayerMovement();
    }

    void PlayAnimation(Animator anim, string stateName)
    {
        if (anim == null) return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName)) return;
        anim.Play(stateName);
    }

    void PlayerDirectionInput()
    {
        keyHorizontal = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            keyHorizontal = -1f;
        }
        else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            keyHorizontal = 1f;
        }
    }

    void PlayerJumpInput()
    {
        Keyboard keyboard = Keyboard.current;
        keyJump = keyboard != null && keyboard.spaceKey.wasPressedThisFrame;
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        Color raycastColor;
        RaycastHit2D raycastHit;
        float raycastDistance = 0.05f;
        int layerMask = 1 << LayerMask.NameToLayer("Ground");

        //goundCheck
        Vector3 box_origin = box2d.bounds.center;
        box_origin.y = box2d.bounds.min.y + (box2d.bounds.extents.y / 4f);
        Vector3 box_size = box2d.bounds.size;
        box_size.y = box2d.bounds.size.y / 4f;
        raycastHit = Physics2D.BoxCast(box_origin, box_size, 0f, Vector2.down, raycastDistance, layerMask);

        //player box colliding with ground layer
        if (raycastHit.collider != null)
        {
            isGrounded = true;
        }

        // draw debug lines
        raycastColor = (isGrounded) ? Color.green : Color.red;
        Debug.DrawRay(box_origin + new Vector3(box2d.bounds.extents.x, 0),
            Vector2.down * (box2d.bounds.extents.y / 4f + raycastDistance), raycastColor);

        Debug.DrawRay(box_origin - new Vector3(box2d.bounds.extents.x, 0),
            Vector2.down * (box2d.bounds.extents.y / 4f + raycastDistance), raycastColor);

        Debug.DrawRay(box_origin - new Vector3(box2d.bounds.extents.x, box2d.bounds.extents.y/4f +raycastDistance),
            Vector2.right * (box2d.bounds.extents.x *2), raycastColor);

    }

    void PlayerMovement()
    {
        if (keyHorizontal < 0)
        {
            if (isFacingright)
            {
                Flip();
            }
            rb2d.linearVelocity = new Vector2(-moveSpeed, rb2d.linearVelocity.y);
        }
        else if (keyHorizontal > 0)
        {
            if (!isFacingright)
            {
                Flip();
            }
            rb2d.linearVelocity = new Vector2(moveSpeed, rb2d.linearVelocity.y);
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0f, rb2d.linearVelocity.y);
        }

        if (keyJump && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
        }

        UpdatePlayerAnimation();
    }

    void UpdatePlayerAnimation()
    {
        if (!isGrounded)
        {
            PlayAnimation(animator, "Jump");
        }
        else if (keyHorizontal != 0f)
        {
            PlayAnimation(animator, "Run");
        }
        else
        {
            PlayAnimation(animator, "PlayerIdle");
        }
    }

    void Flip()
    {
        isFacingright = !isFacingright;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }


    public void HitSide(bool rightSide)
    {
        hitSideRight = rightSide;
    }

    public void Invincible(bool invincibility)
    {
        isInvincible = invincibility;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        if (UIHealthBar.instance != null)
        {
            UIHealthBar.instance.SetValue(currentHealth / (float)MaxHealth);
        }

        if (currentHealth <= 0)
        {
            Defeat();
        }
        else
        {
            StartDamageAnimation();
            UpdateLosaDamageState();
        }
    }

    void UpdateLosaDamageState()
    {
        if (losaAnimator == null) return;

        if (currentHealth == 2)
        {
            PlayAnimation(losaAnimator, "Cracking");
        }
        else if (currentHealth == 1)
        {
            PlayAnimation(losaAnimator, "Breaking");
        }
    }

    void StartDamageAnimation()
    {
        isInvincible = true;
        float hitForceX = 0.50f;
        float hitForceY = 1.50f;
        if (hitSideRight) hitForceX = -hitForceX;
        rb2d.AddForce(new Vector2(hitForceX, hitForceY), ForceMode2D.Impulse);
        StartCoroutine(DamageFlashRoutine());
    }

    IEnumerator DamageFlashRoutine()
    {
        float elapsed = 0f;
        bool showFlash = true;

        while (elapsed < damageStaggerDuration)
        {
            spriteRenderer.color = showFlash ? damageFlashColor : normalColor;
            showFlash = !showFlash;
            yield return new WaitForSeconds(damageFlashInterval);
            elapsed += damageFlashInterval;
        }

        spriteRenderer.color = normalColor;
        isInvincible = false;
    }

    void Defeat()
    {
        isInvincible = true;
        rb2d.linearVelocity = Vector2.zero;
        PlayAnimation(animator, "Death");
        box2d.size = new Vector2(box2d.size.x, deathColliderHeight);
        enabled = false;
    }

}
