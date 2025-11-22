using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator transition;

    AsyncOperation operation;

    public void loadScene(string scene) => StartCoroutine(loadSceneCoroutine(scene));
    IEnumerator loadSceneCoroutine(string scene)
    {
        transition.SetTrigger("Switch");

        yield return new WaitForSeconds(0.5f);

        operation = SceneManager.LoadSceneAsync(scene);
        yield return new WaitUntil(() => operation.isDone);
        operation.allowSceneActivation = true;
    }
}
