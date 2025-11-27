using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    int Level = 1;

    [SerializeField] TextMeshProUGUI ObjectiveText;

    [SerializeField] GameObject[] LevelDoors;

    void Start() => getObjectives();

    public void getObjectives()
    {
        getFirstObjective();

        if (PlayerPrefs.HasKey("Level"))
            Level = PlayerPrefs.GetInt("Level");
        else
        {
            Level = 1;
            PlayerPrefs.SetInt("Level", 1);
        }

        for (int i = 0; i < LevelDoors.Length; i++)
            LevelDoors[i].SetActive(false);

        if (Level >= 5)
        {
            for (int i = 0; i < LevelDoors.Length; i++)
                LevelDoors[4].SetActive(true);

            return;
        }

        LevelDoors[Level - 1].SetActive(true);
    }

    public void getFirstObjective()
    {
        string[] lines = PlayerPrefs.GetString("Content").Split('\n');
        string line = lines.FirstOrDefault(l => l.StartsWith("[O]"));

        if (line == null)
        {
            Debug.LogError("Missing [O] line in language file.");
            return;
        }

        string firstO = line.Substring(3).Trim();
        PlayerPrefs.SetString("Objective", firstO);
    }

    public void switchLanguageObjective()
    {
        string[] lines = PlayerPrefs.GetString("Content").Split('\n');
        string line = lines.FirstOrDefault(l => l.StartsWith("[O]"));

        if (line == null)
        {
            Debug.LogError("Missing [O] line in language file.");
            return;
        }

        string firstO = line.Substring(3).Trim();
        PlayerPrefs.SetString("Objective", firstO);
    }
    public void setLevel(int l)
    {
        Level = l;
        PlayerPrefs.SetInt("Level", Level);
    }
}
