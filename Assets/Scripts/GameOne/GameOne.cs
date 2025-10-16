using System.Collections;
using UnityEngine;

public class GameOne : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelOneObject;
    [SerializeField] GameObject[] DraggableObjects;
    [SerializeField] GameObject finishKey;

    bool gameStarted = false;

    void Update()
    {
        checkStart();
        checkFinish();
    }

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelOneObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelOneObject.SetActive(true);
        }
    }

    void checkFinish()
    {
        if(gameStarted && gameController.gameFinished && dialogueSM.checkIfDialogEnded())
        {
            gameStarted = false;
            StartCoroutine(goToMenu());
        }
    }

    public void checkIfBeaten()
    {
        if(checkDraggableObjects() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);

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
