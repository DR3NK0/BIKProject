using UnityEngine;

public class GamFour : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelFourObject;
    [SerializeField] GameObject Tutorial;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject[] windows;

    bool gameStarted = false;
    bool tutorialStarted = false;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelFourObject.GetComponent<CanvasGroup>().interactable)
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
        if (checkIfAllClosed() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishUI.SetActive(true);

            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

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

    public void increaseLevel() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    public void finishTutorial()
    {
        tutorialStarted = true;
        Tutorial.SetActive(false);

        LevelFourObject.GetComponent<CanvasGroup>().interactable = true;
        LevelFourObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameStarted = true;
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
