using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    List<string> oSentences = new List<string>();
    int Level = 0;

    [SerializeField] TextMeshProUGUI ObjectiveText;

    [SerializeField] GameObject[] LevelDoors;

    void Start() => getObjectives();

    public void getObjectives()
    {
        List<string> oSentences = ParseOLines(PlayerPrefs.GetString("Content"));

        if (PlayerPrefs.HasKey("Level"))
            Level = PlayerPrefs.GetInt("Level");
        else
        {
            Level = 1;
            PlayerPrefs.SetInt("Level", 1);
        }

        if (Level > 5)
        {
            for (int i = 0; i < LevelDoors.Length; i++)
                LevelDoors[i].GetComponent<Button>().interactable = true;

            ObjectiveText.text = "All levels cleared!";

            return;
        }

        ObjectiveText.text = oSentences[Level - 1];

        for (int i = 0; i < LevelDoors.Length; i++)
            LevelDoors[i].GetComponent<Button>().interactable = false;

        LevelDoors[Level - 1].GetComponent<Button>().interactable = true;
    }

    List<string> ParseOLines(string input)
    {
        List<string> result = new List<string>();

        string[] lines = input.Split('\n');

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(line) || line == "-")
                continue;

            if (line.StartsWith("[O]"))
            {
                string clean = line.Substring(3).Trim();
                result.Add(clean);
            }
        }

        return result;
    }
}
