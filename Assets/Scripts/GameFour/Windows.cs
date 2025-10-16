using UnityEngine;

public class Windows : MonoBehaviour
{
    [SerializeField] GamFour gameFour;

    public void closeWindow()
    {
        this.gameObject.SetActive(false);

        gameFour.checkIfBeaten();
    }
}
