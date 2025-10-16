using UnityEngine;
using UnityEngine.EventSystems;

public class Vegetables : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;

    RectTransform rectTransform;

    Vector2 originalPosition;
    Transform originalParent;

    public int vegetableIndex;

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

        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<KeyPlaceholder>() == null)
            goToOriginalPosition();
    }

    public void DisableItem() => gameObject.SetActive(false);

    public void goToOriginalPosition() => rectTransform.anchoredPosition = originalPosition;
}
