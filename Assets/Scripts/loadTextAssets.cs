using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class loadTextAssets : MonoBehaviour
{
    [SerializeField] SceneController sceneController;

    IEnumerator Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Menu")) yield break;

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "macedonian.txt");

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
