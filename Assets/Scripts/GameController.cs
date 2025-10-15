using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameStarted { get; private set; } = false;
    public bool gameFinished { get; private set; } = false;

    [SerializeField] GameObject dialoguePanel;

    public void setGameStarted(bool state)
    {
        gameStarted = state;
        controldialoguePanel(!state);
    }

    public void setGameFinished(bool state) => gameFinished = state;
    public void controldialoguePanel(bool state) => dialoguePanel.SetActive(state);
}
