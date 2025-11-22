using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPlaceholder : MonoBehaviour, IDropHandler
{
    [SerializeField] GameTree gameTree;

    [Space]
    public int keyIndex;

    [Space]

    [SerializeField] Animator Greshka;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject == null) return;

        Key item = droppedObject.GetComponent<Key>();

        if (item == null) return;

        if (item.keyIndex == this.keyIndex)
        {
            droppedObject.transform.SetParent(transform, false);
            droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            droppedObject.transform.eulerAngles = Vector3.zero;

            item.changeItemLocation(this.gameObject.GetComponent<RectTransform>().position);

            item.canBeDragged = false;

            gameTree.checkIfBeaten();
        }
        else
        {
            item.goToOriginalPosition();
            Greshka.SetTrigger("Flash");
        }
    }
}
