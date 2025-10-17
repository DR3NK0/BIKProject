using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadTextAssets : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    [SerializeField] Objectives o;

    int selectedLanguage = 0; // 1 - Macedonian, 2 - English, 3 - Shqip
    [SerializeField] GameObject[] LanguageButtons;

    void Start() => startLoadingLanguage();

    public void startLoadingLanguage()
    {
        StartCoroutine(loadLanguage());
        switchLanguageButtons();
    }

    IEnumerator loadLanguage()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Menu")) yield break;

        if (PlayerPrefs.HasKey("Language"))
            selectedLanguage = PlayerPrefs.GetInt("Language");
        else
        {
            selectedLanguage = 0;
            PlayerPrefs.SetInt("Language", 0);
        }

        string path = "";

        if(selectedLanguage == 0)
            path = System.IO.Path.Combine(Application.streamingAssetsPath, "macedonian.txt");
        else if(selectedLanguage == 1)
            path = System.IO.Path.Combine(Application.streamingAssetsPath, "english.txt");
        else if (selectedLanguage == 2)
            path = System.IO.Path.Combine(Application.streamingAssetsPath, "shqip.txt");

        if (path.Contains("://"))
        {
            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string content = request.downloadHandler.text;
                PlayerPrefs.DeleteKey("Content");
                PlayerPrefs.SetString("Content", content);
            }
        }
        else
        {
            string content = System.IO.File.ReadAllText(path);
            PlayerPrefs.DeleteKey("Content");
            PlayerPrefs.SetString("Content", content);
        }
    }

    public void switchLanguageButtons()
    {
        for (int i = 0; i < LanguageButtons.Length; i++)
            LanguageButtons[i].GetComponent<Button>().interactable = true;

        LanguageButtons[selectedLanguage].GetComponent<Button>().interactable = false;
    }

    public void switchLanguage(int index)
    {
        selectedLanguage = index;
        PlayerPrefs.SetInt("Language", selectedLanguage);
        StartCoroutine(loadLanguage());
        switchLanguageButtons();
        o.loadSentences();
    }
}
