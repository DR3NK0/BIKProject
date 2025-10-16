using System.Collections;
using UnityEngine;

public class GamFour : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelFourObject;
    [SerializeField] GameObject finishKey;
    [SerializeField] GameObject[] windows;

    bool gameStarted = false;

    void Update()
    {
        checkStart();
        checkFinish();
    }

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelFourObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelFourObject.SetActive(true);
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

            LevelFourObject.SetActive(false);

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
        bool allCorrect = true;

        for (int i = 0; i < windows.Length; i++)
        {
            if (windows[i].activeInHierarchy)
                allCorrect = false;
        }

        return allCorrect;
    }
}
