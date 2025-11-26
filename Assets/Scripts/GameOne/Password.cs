using UnityEngine;
using UnityEngine.EventSystems;

public class Password : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameOne gameOne;
    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
        gameOne.passwordFound = true;
        gameOne.changeToPasswordFoundBackground();
        gameOne.checkIfBeaten();
    }
}
