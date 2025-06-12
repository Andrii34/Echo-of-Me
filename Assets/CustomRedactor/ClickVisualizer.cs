using UnityEngine;
using UnityEngine.UI;

public class ClickVisualizer : MonoBehaviour
{
    [Header("Настройки трансформации")]
    public RectTransform targetImage;
    public Canvas canvas;

    [Header("Настройки масштаба")]
    [Range(0.5f, 1f)]
    public float pressedScale = 0.9f;
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<RectTransform>();

        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();

        originalScale = targetImage.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        FollowCursor();
        HandleInput();
        SmoothScale();
    }

    private void FollowCursor()
    {
        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        targetImage.localPosition = localPoint;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
            targetScale = originalScale * pressedScale;
        else
            targetScale = originalScale;
    }

    private void SmoothScale()
    {
        targetImage.localScale = Vector3.Lerp(targetImage.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }
}

