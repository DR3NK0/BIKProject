using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueEntry
{
    public string type;
    public string text;
    public List<DialogueAnswer> answers;
}

[System.Serializable]
public class DialogueAnswer
{
    public string type;
    public string option;
    public string response;
}
public class dialogueSystem : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] SceneController sceneController;

    [Space]

    [SerializeField] GameObject ObjectiveObject;
    [SerializeField] TextMeshProUGUI objectiveText;

    [SerializeField] TextMeshProUGUI TextArea;

    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject[] Options;
    [SerializeField] TextMeshProUGUI QuestionField;

    [SerializeField] GameObject ContinueButton;

    [SerializeField] float wordSpeed;

    int chatIndex = 0;
    string dialogue;
    int selected = -1;

    [Space]

    [SerializeField] int currentLevel = 0;

    Coroutine typingCoroutine = null;

    public List<List<DialogueEntry>> levels = new();

    void Start()
    {
        if (!PlayerPrefs.HasKey("Objective"))
            ObjectiveObject.SetActive(false);
        else
        {
            ObjectiveObject.SetActive(true);
            objectiveText.text = PlayerPrefs.GetString("Objective");
        }

        ParseText(PlayerPrefs.GetString("Content")); 
        startTyping();
    }

    public void startTyping()
    {
        TextArea.text = "";

        if (checkReturnObjective())
            checkIfObjective();
        else
        {
            chatIndex++;
            checkIfObjective();
        }

        if (checkIfDialogEnded()) return;

        dialogue = levels[currentLevel][chatIndex].text.ToString();
        typingCoroutine = StartCoroutine(Typing());
    }

    void ParseText(string text)
    {
        string[] lines = text.Split('\n');
        List<DialogueEntry> currentLevel = new();
        DialogueEntry currentQuestion = null;

        foreach (var rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line)) continue;

            if (line == "-")
            {
                levels.Add(currentLevel);
                currentLevel = new();
                currentQuestion = null;
                continue;
            }

            if (line.StartsWith("[O]"))
            {
                currentLevel.Add(new DialogueEntry
                {
                    type = "O",
                    text = line.Substring(3).Trim()
                });
                currentQuestion = null;
            }
            else if (line.StartsWith("[D]"))
            {
                currentLevel.Add(new DialogueEntry
                {
                    type = "D",
                    text = line.Substring(3).Trim()
                });
                currentQuestion = null;
            }
            else if (line.StartsWith("[P]"))
            {
                currentLevel.Add(new DialogueEntry
                {
                    type = "P",
                    text = line.Substring(3).Trim()
                });
                currentQuestion = null;
            }
            else if (line.StartsWith("[Q]"))
            {
                currentQuestion = new DialogueEntry
                {
                    type = "Q",
                    text = line.Substring(3).Trim(),
                    answers = new List<DialogueAnswer>()
                };
                currentLevel.Add(currentQuestion);
            }
            else if (line.StartsWith("[F]") || line.StartsWith("[T]"))
            {
                if (currentQuestion == null) continue;

                string type = line[1].ToString();
                string[] parts = line.Substring(3).Trim().Split("->");

                DialogueAnswer answer = new DialogueAnswer
                {
                    type = type,
                    option = parts[0].Trim(),
                    response = parts.Length > 1 ? parts[1].Trim() : ""
                };

                currentQuestion.answers.Add(answer);
            }
        }

        if (currentLevel.Count > 0)
            levels.Add(currentLevel);

    }

    public void continueClicked()
    {
        ContinueButton.SetActive(false);

        checkText();
    }

    public void optionClicked(int selected)
    {
        OptionsPanel.SetActive(false);
        TextArea.gameObject.SetActive(true);
        TextArea.text = "";
        dialogue = levels[currentLevel][chatIndex].answers[selected].response;
        this.selected = selected;

        typingCoroutine = StartCoroutine(Typing());
    }

    public void checkText()
    {
        if(levels[currentLevel][chatIndex].type == "Q")
        {
            if(selected == -1 || levels[currentLevel][chatIndex].answers[selected].type == "F")
            {
                OptionsPanel.SetActive(true);
                TextArea.gameObject.SetActive(false);
                QuestionField.text = levels[currentLevel][chatIndex].text;
                Options[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[currentLevel][chatIndex].answers[0].option;
                Options[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[currentLevel][chatIndex].answers[1].option;
                Options[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[currentLevel][chatIndex].answers[2].option;
            }
            else
            {
                if (checkIfDialogEnded())
                {
                    if (!gameController.gameStarted && !gameController.gameFinished)
                        gameController.setGameStarted(true);
                    else
                        sceneController.loadScene("Menu");

                    return;
                }

                chatIndex++;

                checkIfObjective();

                if (checkIfDialogEnded()) return;

                TextArea.text = "";
                dialogue = levels[currentLevel][chatIndex].text;
                selected = -1;
                typingCoroutine = StartCoroutine(Typing());
            }
        }
        else if(levels[currentLevel][chatIndex].type == "D")
        {
            if (checkIfDialogEnded())
            {
                if (!gameController.gameStarted && !gameController.gameFinished)
                    gameController.setGameStarted(true);
                else
                    sceneController.loadScene("Menu");

                return;
            }

            chatIndex++;

            checkIfObjective();

            if(checkIfDialogEnded()) return;

            TextArea.text = "";
            dialogue = levels[currentLevel][chatIndex].text;
            typingCoroutine = StartCoroutine(Typing());
        }
        else if (levels[currentLevel][chatIndex].type == "P")
        {
            if (!gameController.gameStarted && !gameController.gameFinished)
                gameController.setGameStarted(true);
            else
            {
                chatIndex++;

                checkIfObjective();

                if (checkIfDialogEnded()) return;

                TextArea.text = "";
                dialogue = levels[currentLevel][chatIndex].text;
                typingCoroutine = StartCoroutine(Typing());
            }
        }
    }

    public bool checkIfDialogEnded()
    {
        if (chatIndex == levels[currentLevel].Count - 1)
        {
            return true;
        }
        else
            return false;
    }

    public void checkIfObjective()
    {
        if (levels[currentLevel][chatIndex].type == "O")
        {
            objectiveText.text = levels[currentLevel][chatIndex].text;
            PlayerPrefs.SetString("Objective", objectiveText.text);

            if(!ObjectiveObject.activeInHierarchy)
                ObjectiveObject.SetActive(true);

            if (checkIfDialogEnded())
            {
                if (!gameController.gameStarted && !gameController.gameFinished)
                    gameController.setGameStarted(true);
                else
                    sceneController.loadScene("Menu");

                return;
            }

            chatIndex++;

            TextArea.text = "";
            dialogue = levels[currentLevel][chatIndex].text;
            typingCoroutine = StartCoroutine(Typing());
        }
    }
    public bool checkReturnObjective() => levels[currentLevel][chatIndex].type == "O";

    IEnumerator Typing()
    {
        char[] chars = dialogue.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '<')
            {
                int endIndex = dialogue.IndexOf('>', i);

                if (endIndex != -1)
                {
                    string tag = dialogue.Substring(i, endIndex - i + 1);
                    TextArea.text += tag;

                    i = endIndex;
                    continue;
                }
            }

            TextArea.text += chars[i];
            yield return new WaitForSeconds(wordSpeed);
        }

        ContinueButton.SetActive(true);
        typingCoroutine = null;
    }
}
