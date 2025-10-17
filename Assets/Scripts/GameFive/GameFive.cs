using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
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

    void Update()
    {
        checkStart();
        checkFinish();
    }

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelFiveObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelFiveObject.SetActive(true);
        }
    }

    void checkFinish()
    {
        if (gameStarted && gameController.gameFinished && dialogueSM.checkIfDialogEnded())
        {
            gameStarted = false;
            StartCoroutine(goToMenu());
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllClosed() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);

            LevelFiveObject.SetActive(false);

            if (!dialogueSM.checkIfDialogEnded())
            {
                gameController.controldialoguePanel(true);
                dialogueSM.startTyping();
            }
        }
    }

    IEnumerator goToMenu()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

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
