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

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelOneObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelOneObject.SetActive(true);
        }
    }

    public void checkIfBeaten()
    {
        if(checkDraggableObjects() && gameStarted)
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
