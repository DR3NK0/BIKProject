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

        if (Level > 5)
        {
            for (int i = 0; i < LevelDoors.Length; i++)
                LevelDoors[i].GetComponent<Button>().interactable = true;

            return;
        }

        for (int i = 0; i < LevelDoors.Length; i++)
            LevelDoors[i].GetComponent<Button>().interactable = false;

        LevelDoors[Level - 1].GetComponent<Button>().interactable = true;
    }

    public void getFirstObjective()
    {
        if (!PlayerPrefs.HasKey("Objective"))
        {
            string firstO = PlayerPrefs.GetString("Content").Split('\n').First(l => l.StartsWith("[O]")).Substring(3).Trim();
            PlayerPrefs.SetString("Objective", firstO);
        }

        ObjectiveText.text = PlayerPrefs.GetString("Objective");
    }

    public void switchLanguageObjective()
    {
        string firstO = PlayerPrefs.GetString("Content").Split('\n').First(l => l.StartsWith("[O]")).Substring(3).Trim();
        PlayerPrefs.SetString("Objective", firstO);
        ObjectiveText.text = PlayerPrefs.GetString("Objective");
    }
    public void setLevel(int l)
    {
        Level = l;
        PlayerPrefs.SetInt("Level", Level);
    }
}
