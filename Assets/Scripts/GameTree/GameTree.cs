using System.Collections;
using UnityEngine;

public class GameTree : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] dialogueSystem dialogueSM;
    [SerializeField] SceneController sceneController;

    [SerializeField] GameObject LevelTreeObject;
    [SerializeField] GameObject finishKey;
    [SerializeField] GameObject[] keys;

    bool gameStarted = false;

    void Update() => checkStart();

    void checkStart()
    {
        if (!gameStarted && gameController.gameStarted && !gameController.gameFinished && !LevelTreeObject.activeInHierarchy)
        {
            gameStarted = true;
            LevelTreeObject.SetActive(true);
        }
    }

    public void checkIfBeaten()
    {
        if (checkIfAllStacked() && gameStarted)
        {
            gameController.setGameFinished(true);
            finishKey.SetActive(true);

            LevelTreeObject.SetActive(false);

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
