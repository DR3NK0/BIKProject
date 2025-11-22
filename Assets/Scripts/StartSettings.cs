using UnityEngine;
using UnityEngine.UI;

public class StartSettings : MonoBehaviour
{
    int selectedLanguage = 0; // 0 - Macedonian, 1 - English, 2 - Shqip
    int Mute = 0; // 0 - false, 1 - true

    [SerializeField] GameObject MuteButton;
    [SerializeField] GameObject[] LanguageButtons;
    [SerializeField] GameObject[] LanguageGameObjects; // 0 - Macedonian, 1 - English, 2 - Shqip

    void Start()
    {
        if (PlayerPrefs.HasKey("Mute"))
            Mute = PlayerPrefs.GetInt("Mute");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Mute", Mute);

        if (Mute == 0)
        {
            MuteButton.transform.GetChild(0).gameObject.SetActive(true);
            MuteButton.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            MuteButton.transform.GetChild(0).gameObject.SetActive(false);
            MuteButton.transform.GetChild(1).gameObject.SetActive(true);
        }

        startLoadingLanguage();
    }

    public void startLoadingLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
            selectedLanguage = PlayerPrefs.GetInt("Language");
        else
        {
            selectedLanguage = 0;
            PlayerPrefs.SetInt("Language", 0);
        }

        switchLanguage();
        switchLanguageButtons();
    }

    public void switchLanguageButtons()
    {
        for (int i = 0; i < LanguageButtons.Length; i++)
            LanguageButtons[i].GetComponent<Button>().interactable = true;

        LanguageButtons[selectedLanguage].GetComponent<Button>().interactable = false;
    }

    public void switchLanguage()
    {
        for (int i = 0; i < LanguageGameObjects.Length; i++)
            LanguageGameObjects[i].SetActive(false);

        LanguageGameObjects[selectedLanguage].SetActive(true);
    }

    public void switchLanguage(int index)
    {
        selectedLanguage = index;
        PlayerPrefs.SetInt("Language", selectedLanguage);

        switchLanguageButtons();
        switchLanguageButtons();
    }

    public void toggleMute()
    {
        if (Mute == 0)
        {
            MuteButton.transform.GetChild(0).gameObject.SetActive(false);
            MuteButton.transform.GetChild(1).gameObject.SetActive(true);
            Mute = 1;
        }
        else
        {
            MuteButton.transform.GetChild(0).gameObject.SetActive(true);
            MuteButton.transform.GetChild(1).gameObject.SetActive(false);
            Mute = 0;
        }

        PlayerPrefs.SetInt("Mute", Mute);
    }

}
