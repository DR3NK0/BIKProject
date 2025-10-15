using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] GameOne gameOne;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            DraggableObject item = droppedObject.GetComponent<DraggableObject>();
            if (item != null)
            {
                droppedObject.transform.SetParent(transform, false);
                droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                item.DisableItem();

                gameOne.checkIfBeaten();
            }
        }
    }
}
