using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

// Forces a 16:9 viewport and fills the rest of the screen with black bars.
// Overlay canvases are moved onto a UI camera so they stay inside the 16:9 area.
public class ScreenLetterbox : MonoBehaviour
{
    const float TargetAspect = 16f / 9f;
    const float CanvasSweepInterval = 0.5f;

    Camera backgroundCam;
    Camera uiCam;
    int uiLayer;
    float nextSweep;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        var go = new GameObject("ScreenLetterbox");
        DontDestroyOnLoad(go);
        go.AddComponent<ScreenLetterbox>();
    }

    void Awake()
    {
        uiLayer = LayerMask.NameToLayer("UI");

        backgroundCam = CreateCamera("LetterboxBackground", -100, CameraClearFlags.SolidColor, 0);
        backgroundCam.backgroundColor = Color.black;

        // URP: the UI camera must be an Overlay camera stacked on the scene's base camera.
        uiCam = CreateCamera("LetterboxUI", 100, CameraClearFlags.Depth, 1 << uiLayer);
        uiCam.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;

        SceneManager.sceneLoaded += (_, __) => SweepCanvases();
    }

    Camera FindSceneBaseCamera()
    {
        Camera best = null;
        foreach (Camera cam in Camera.allCameras)
        {
            if (cam == backgroundCam || cam == uiCam) continue;
            if (cam.GetUniversalAdditionalCameraData().renderType != CameraRenderType.Base) continue;
            if (best == null || cam.depth > best.depth) best = cam;
        }
        return best;
    }

    Camera CreateCamera(string camName, float depth, CameraClearFlags clearFlags, int mask)
    {
        var go = new GameObject(camName);
        go.transform.SetParent(transform);
        var cam = go.AddComponent<Camera>();
        cam.orthographic = true;
        cam.clearFlags = clearFlags;
        cam.cullingMask = mask;
        cam.depth = depth;
        return cam;
    }

    void LateUpdate()
    {
        Rect rect = ComputeViewport();

        foreach (Camera cam in Camera.allCameras)
        {
            if (cam == backgroundCam) continue;
            cam.rect = rect;
            if (cam != uiCam)
                cam.cullingMask &= ~(1 << uiLayer);
        }

        Camera sceneCam = FindSceneBaseCamera();
        if (sceneCam != null)
        {
            var stack = sceneCam.GetUniversalAdditionalCameraData().cameraStack;
            if (!stack.Contains(uiCam))
                stack.Add(uiCam);
        }

        if (Time.unscaledTime >= nextSweep)
        {
            nextSweep = Time.unscaledTime + CanvasSweepInterval;
            SweepCanvases();
        }
    }

    static Rect ComputeViewport()
    {
        float aspect = (float)Screen.width / Screen.height;

        if (aspect > TargetAspect)
        {
            float w = TargetAspect / aspect;
            return new Rect((1f - w) / 2f, 0f, w, 1f);
        }

        float h = aspect / TargetAspect;
        return new Rect(0f, (1f - h) / 2f, 1f, h);
    }

    void SweepCanvases()
    {
        foreach (Canvas canvas in FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (!canvas.isRootCanvas || canvas.renderMode != RenderMode.ScreenSpaceOverlay) continue;
            if (canvas.gameObject.layer != uiLayer) continue;

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCam;
            canvas.planeDistance = 1f;
        }
    }
}
