using UnityEngine;

public class GameTree : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelTreeObject;
    [SerializeField] GameObject Tutorial;
    [SerializeField] GameObject finishKey;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject[] keys;

    bool gameStarted = false;
    bool tutorialStarted = false;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelTreeObject.GetComponent<CanvasGroup>().interactable)
        {
            if (!tutorialStarted)
            {
                if (!Tutorial.activeInHierarchy)
                    Tutorial.SetActive(true);
            }
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllStacked() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);
            finishKey.GetComponent<Animator>().SetTrigger("Flash");
            finishUI.SetActive(true);

        }
    }

    /*
    public void continueDialogue()
    {
        if (!dialogueSM.checkIfDialogEnded())
        {
            gameController.controldialoguePanel(true);
            dialogueSM.startTyping();
        }
        else
            sceneController.loadScene("Menu");
    }
    */
    public void increaseLevel() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

    public void finishTutorial()
    {
        tutorialStarted = true;
        Tutorial.SetActive(false);

        LevelTreeObject.GetComponent<CanvasGroup>().interactable = true;
        LevelTreeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameStarted = true;
    }

    bool checkIfAllStacked()
    {
        bool allCorrect = true;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].GetComponent<Key>().canBeDragged)
                allCorrect = false;
        }

        return allCorrect;
    }
}
