using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class loadTextAssets : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    [SerializeField] Objectives o;

    int selectedLanguage = 0; // 0 - Macedonian, 1 - English, 2 - Shqip
    [SerializeField] GameObject[] LanguageGameObjects; // 0 - Macedonian, 1 - English, 2 - Shqip

    void Start() => startLoadingLanguage();

    public void startLoadingLanguage()
    {
        StartCoroutine(loadLanguage());
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

            for (int i = 0; i < LanguageGameObjects.Length; i++)
                LanguageGameObjects[i].SetActive(false);

            LanguageGameObjects[selectedLanguage].SetActive(true);
        }

        string path = "";

        if (selectedLanguage == 0)
            path = Path.Combine(Application.streamingAssetsPath, "macedonian.txt");
        else if (selectedLanguage == 1)
            path = Path.Combine(Application.streamingAssetsPath, "english.txt");
        else if (selectedLanguage == 2)
            path = Path.Combine(Application.streamingAssetsPath, "shqip.txt");

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
}
