using UnityEngine.EventSystems;
using UnityEngine;

public class Key : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;

    public int keyIndex;
    public bool canBeDragged { get; set; } = true;

    RectTransform rectTransform;
    Vector2 originalPosition;

    [SerializeField] Animator Greshka;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canBeDragged) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canBeDragged) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canBeDragged) return;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<KeyPlaceholder>() == null)
        {
            goToOriginalPosition();
            Greshka.SetTrigger("Flash");
        }
    }

    public void changeItemLocation(Vector3 pos) => rectTransform.position = pos;
    public void goToOriginalPosition() => rectTransform.anchoredPosition = originalPosition;
}
