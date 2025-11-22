using UnityEngine;

public class Windows : MonoBehaviour
{
    [SerializeField] GamFour gameFour;
    [SerializeField] Animator windowsAnimator;

    public void runClose() => windowsAnimator.SetTrigger("Close");
    public void closeWindow()
    {
        this.gameObject.SetActive(false);

        gameFour.checkIfBeaten();
    }
}
