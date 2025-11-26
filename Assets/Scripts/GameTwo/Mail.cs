using UnityEngine;
using UnityEngine.EventSystems;

public class Mail : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameTwo gameTwo;

    public int mailIndex;
    public int correctMailSpot;

    bool mailBoxEmpty = false;
    bool correctSportComplete = false;

    [SerializeField] Hand hand;

    [SerializeField] GameObject Completed;

    public bool Tutorial = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (correctSportComplete) return;

        if (!Tutorial) { Tutorial = !Tutorial; gameTwo.changeTutorial(); }

        if (!hand.handFull)
        {
            this.gameObject.transform.GetChild(mailIndex).gameObject.SetActive(false);
            mailBoxEmpty = true;
            hand.getMailInHand(mailIndex);
        }
        else
        {
            if (mailBoxEmpty)
            {
                mailIndex = hand.handMailIndex;
                hand.removeMailInHand();
            }
            else
            {
                for(int i = 0; i < 4; i++)
                    this.gameObject.transform.GetChild(i).gameObject.SetActive(false);

                int tmp = mailIndex;
                mailIndex = hand.handMailIndex;
                hand.handMailIndex = tmp;
            }

            mailBoxEmpty = false;

            this.gameObject.transform.GetChild(mailIndex).gameObject.SetActive(true);

            if (mailIndex == correctMailSpot)
            {
                correctSportComplete = true;
                Completed.SetActive(true);
            }

            hand.closeMail();

            gameTwo.checkIfBeaten();
        }
    }
}
