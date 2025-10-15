using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    AsyncOperation operation;

    public void loadScene(string scene) => StartCoroutine(loadSceneCoroutine(scene));
    IEnumerator loadSceneCoroutine(string scene)
    {
        operation = SceneManager.LoadSceneAsync(scene);
        yield return new WaitUntil(() => operation.isDone);
        operation.allowSceneActivation = true;
    }
}
