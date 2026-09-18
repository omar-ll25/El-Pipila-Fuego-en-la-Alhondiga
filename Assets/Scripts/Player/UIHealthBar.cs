using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image[] hearts;

    public static UIHealthBar instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void SetValue(float value)
    {
        int activeHearts = Mathf.RoundToInt(value * hearts.Length);
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < activeHearts);
        }
    }

}
