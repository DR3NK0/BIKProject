using UnityEngine;
using UnityEngine.EventSystems;

public class Mail : MonoBehaviour, IPointerClickHandler
{
    public int mailIndex;
    public int mailBelongsTo;

    [SerializeField] Hand hand;
    [SerializeField] GameObject mailUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hand.handFull)
            hand.getMailInHand(mailIndex, this.gameObject);
        else
        {
            if (hand.mailGameObject == null) return;

            if(hand.handMailIndex == mailIndex)
            {
                if(mailUI != null)
                    mailUI.SetActive(true);
            }
            else
                hand.switchMailInHand(mailIndex, this.gameObject);
        }
    }

    public void closeMailUI()
    {
        mailUI.SetActive(false);
    }
}
