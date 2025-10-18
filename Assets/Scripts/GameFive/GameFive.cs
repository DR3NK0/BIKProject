using System.Collections;
using UnityEngine;

public class GameFive : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelFiveObject;
    [SerializeField] GameObject finishKey;
    [SerializeField] GameObject[] vegetables;

    bool gameStarted = false;

    void Update() => checkStart();
        
    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelFiveObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelFiveObject.SetActive(true);
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllClosed() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);

            LevelFiveObject.SetActive(false);

            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            if (!dialogueSM.checkIfDialogEnded())
            {
                gameController.controldialoguePanel(true);
                dialogueSM.startTyping();
            }
            else
                StartCoroutine(goToMenu());
        }
    }

    IEnumerator goToMenu()
    {
        yield return new WaitForSeconds(3);

        sceneController.loadScene("Menu");
    }

    bool checkIfAllClosed()
    {
        bool deactivated = true;

        foreach (GameObject go in vegetables)
        {
            if (go.activeInHierarchy)
                deactivated = false;
        }

        return deactivated;
    }

    public int getVegetablesLenght() => vegetables.Length;
}
