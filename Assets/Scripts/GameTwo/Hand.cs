using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameTwo gameTwo;
    public bool handFull { get; private set; } = false;
    public int handMailIndex = -1;
    public GameObject[] MailUI;

    bool Tutorial = false;

    public void getMailInHand(int index)
    {
        handFull = true;
        handMailIndex = index;

        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        this.gameObject.GetComponent<CanvasGroup>().interactable = true;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void removeMailInHand()
    {
        handFull = false;
        handMailIndex = -1;

        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        this.gameObject.GetComponent<CanvasGroup>().interactable = false;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!handFull) return;
        else
        {
            if (!Tutorial) { Tutorial = !Tutorial; gameTwo.changeTutorial(); }

            MailUI[handMailIndex].SetActive(true);
        }
    }

    public void closeMail()
    {
        for(int i = 0; i < MailUI.Length; i++)
            MailUI[i].SetActive(false);
    }
}
