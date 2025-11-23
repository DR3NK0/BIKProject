using System.Collections;
using UnityEngine;

public class GameTwo : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelTwoObject;
    [SerializeField] GameObject finishKey;
    [SerializeField] GameObject[] MailGameObjects;

    bool gameStarted = false;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelTwoObject.GetComponent<CanvasGroup>().interactable)
        {
            LevelTwoObject.GetComponent<CanvasGroup>().interactable = true;
            LevelTwoObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gameStarted = true;
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllCorrect() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);

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

    bool checkIfAllCorrect()
    {
        bool allCorrect = true;

        for(int i = 0;i< MailGameObjects.Length; i++)
        {
            if (MailGameObjects[i].GetComponent<Mail>().correctMailSpot != MailGameObjects[i].GetComponent<Mail>().mailIndex)
                allCorrect = false;
        }

        return allCorrect;
    }
}
