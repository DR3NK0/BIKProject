using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOne : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelOneObject;
    [SerializeField] GameObject[] DraggableObjects;

    [SerializeField] GameObject Background;
    [SerializeField] Sprite passwordFoundBackground;
    [SerializeField] Sprite finishBackground;

    [SerializeField] GameObject Tutorial;
    [SerializeField] GameObject FinishUI;

    public bool passwordFound = false;

    bool gameStarted = false;
    bool tutorialStarted = false;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelOneObject.GetComponent<CanvasGroup>().interactable)
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
        if(checkDraggableObjects() && gameStarted && passwordFound)
        {
            gameController.setGameFinished(true);
            changeToFinishBackground();
            FinishUI.SetActive(true);

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
        Tutorial.SetActive(false);

        LevelOneObject.GetComponent<CanvasGroup>().interactable = true;
        LevelOneObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameStarted = true;
    }

    public void increaseLevel() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

    public void changeToPasswordFoundBackground() => Background.GetComponent<Image>().sprite = passwordFoundBackground;
    public void changeToFinishBackground() => Background.GetComponent<Image>().sprite = finishBackground;

    bool checkDraggableObjects()
    {
        bool deactivated = true;

        foreach (GameObject go in DraggableObjects)
        {
            if (go.activeInHierarchy)
                deactivated = false;
        }

        return deactivated;
    }
}
