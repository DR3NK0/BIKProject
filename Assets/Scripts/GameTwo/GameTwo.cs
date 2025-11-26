using System.Collections;
using UnityEngine;

public class GameTwo : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelTwoObject;
    [SerializeField] GameObject HandArtGameobject;
    [SerializeField] GameObject[] Tutorials;
    [SerializeField] GameObject[] TutorialActivations;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject[] MailGameObjects;

    bool gameStarted = false;
    bool tutorialStarted = false;
    int tutorialIndex = 0;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelTwoObject.GetComponent<CanvasGroup>().interactable)
        {
            if (!tutorialStarted)
            {
                if (!HandArtGameobject.activeInHierarchy)
                {
                    HandArtGameobject.SetActive(true);

                    if (!Tutorials[tutorialIndex].activeInHierarchy)
                    {
                        Tutorials[tutorialIndex].SetActive(true);
                        TutorialActivations[tutorialIndex].GetComponent<CanvasGroup>().ignoreParentGroups = true;
                    }
                }
            }
        }
    }

    public void changeTutorial()
    {
        if (tutorialIndex >= Tutorials.Length) return;
        Tutorials[tutorialIndex].SetActive(false);

        tutorialIndex++;

        if(tutorialIndex == 1)
        {
            TutorialActivations[tutorialIndex].GetComponent<CanvasGroup>().ignoreParentGroups = true;
            Tutorials[tutorialIndex].SetActive(true);
        }
        else if(tutorialIndex == 2)
        {
            TutorialActivations[tutorialIndex].GetComponent<CanvasGroup>().ignoreParentGroups = true;
            Tutorials[tutorialIndex].SetActive(true);
        }
        else if(tutorialIndex == 3)
        {
            Tutorials[tutorialIndex].SetActive(true);
            finishTutorial();
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllCorrect() && gameStarted)
        {
            gameController.setGameFinished(true);

            for (int i = 0; i < Tutorials.Length; i++)
                Tutorials[i].SetActive(false);

            finishUI.SetActive(true);

            /*
            if (!dialogueSM.checkIfDialogEnded())
            {
                gameController.controldialoguePanel(true);
                dialogueSM.startTyping();
            }
            else
                StartCoroutine(goToMenu());
            */
        }
    }

    public void finishTutorial()
    {
        tutorialStarted = true;

        LevelTwoObject.GetComponent<CanvasGroup>().interactable = true;
        LevelTwoObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameStarted = true;
    }

    public void increaseLevel() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

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
