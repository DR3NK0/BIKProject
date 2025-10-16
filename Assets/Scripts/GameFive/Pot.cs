using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameFive gameFive;

    [SerializeField] TextMeshProUGUI vegetableToAdd;

    string[] toAddStrings = { "Now add Tomato!", "Now add Apple.", "Now Add Cucumber!"};

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
            droppedObject.transform.SetParent(transform, false);
            droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            item.DisableItem();

            if (vegetableIndexToAdd < gameFive.getVegetablesLenght())
            {
                vegetableIndexToAdd++;
                vegetableToAdd.text = toAddStrings[vegetableIndexToAdd - 1];
            }
            else
                vegetableToAdd.text = "";

            gameFive.checkIfBeaten();
        }
        else
            item.goToOriginalPosition();
    }
}
