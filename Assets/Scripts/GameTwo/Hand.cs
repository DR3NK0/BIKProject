using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] GameTwo gameTwo;

    public bool handFull { get; private set; } = false;
    public int handMailIndex = -1;
    public GameObject mailGameObject { get; private set; } = null;

    [SerializeField] GameObject Placeholder; 

    public void getMailInHand(int index,GameObject mail)
    {
        handFull = true;
        handMailIndex = index;
        Placeholder.GetComponent<RectTransform>().position = mail.GetComponent<RectTransform>().position;
        mail.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        mailGameObject = mail;
    }

    public void switchMailInHand(int index, GameObject mail)
    {
        if(index == 0)
        {
            handFull = false;
            mailGameObject.GetComponent<RectTransform>().position = Placeholder.GetComponent<RectTransform>().position;
            Placeholder.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
            mailGameObject = null;
            handMailIndex = -1;

            gameTwo.checkIfBeaten();
        }
        else
        {
            mailGameObject.GetComponent<RectTransform>().position = mail.GetComponent<RectTransform>().position;
            mail.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;

            int mailIndex = mail.GetComponent<Mail>().mailBelongsTo;

            mail.GetComponent<Mail>().mailBelongsTo = mailGameObject.GetComponent<Mail>().mailBelongsTo;
            mailGameObject.GetComponent<Mail>().mailBelongsTo = mailIndex;
            mailGameObject = mail;
            handMailIndex = index;
        }
    }
}
