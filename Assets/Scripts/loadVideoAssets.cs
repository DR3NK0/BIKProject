using UnityEngine;
using UnityEngine.Video;

public class loadVideoAssets : MonoBehaviour
{
    [SerializeField] VideoPlayer videoController;

    void Start()
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, "TEST.mp4");
        videoController.url = url;
        videoController.Play();
    }
}
