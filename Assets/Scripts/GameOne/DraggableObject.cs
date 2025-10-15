using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;

    RectTransform rectTransform;

    Vector2 originalPosition;
    Transform originalParent;

    void Start() => rectTransform = GetComponent<RectTransform>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == originalParent)
            rectTransform.anchoredPosition = originalPosition;
    }

    public void DisableItem() => gameObject.SetActive(false);
}
