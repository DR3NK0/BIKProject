using System.Linq;
using TMPro;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    int Level = 1;

    [SerializeField] GameObject ObjectiveObject;
    [SerializeField] TextMeshProUGUI ObjectiveText;

    [SerializeField] GameObject[] LevelDoors;

    void Start() => getObjectives();

    public void getObjectives()
    {
        setupObjective();

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

    public void setupObjective()
    {
        if(!PlayerPrefs.HasKey("Objective"))
            ObjectiveObject.SetActive(false);
        else
        {
            ObjectiveObject.SetActive(true);
            ObjectiveText.text = PlayerPrefs.GetString("Objective");
        }
    }

}
