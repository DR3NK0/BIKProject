using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameFive gameFive;

    [SerializeField] TextMeshProUGUI vegetableToAdd;

    string[] toAddStrings = { "Додади Масло.", "Сега додади домати.", "Додади ги пиперките", "И на крај, кромидот." };

    int vegetableIndexToAdd = 1;


    void Start() => vegetableToAdd.text = toAddStrings[vegetableIndexToAdd - 1];

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject == null) return;

        Vegetables item = droppedObject.GetComponent<Vegetables>();

        if (item == null) return;

        if (item.vegetableIndex == this.vegetableIndexToAdd)
        {
            item.DisableItem();

            if (vegetableIndexToAdd < gameFive.getVegetablesLenght())
            {
                this.transform.GetChild(vegetableIndexToAdd - 1).gameObject.SetActive(true);
                vegetableIndexToAdd++;
                vegetableToAdd.text = toAddStrings[vegetableIndexToAdd - 1];
            }
            else
            {
                for (int i = 0; i < this.transform.childCount - 1; i++)
                    this.transform.GetChild(i).gameObject.SetActive(false);

                this.transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);

                vegetableToAdd.text = "";
            }

            gameFive.checkIfBeaten();
        }
        else
            item.goToOriginalPosition();
    }
}
