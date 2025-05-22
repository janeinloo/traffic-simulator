using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string url = "https://www.youtube.com/watch?v=dEbP5gLhVfo&ab_channel=simoatso";

    public void OpenYoutubeVideo()
    {
        Application.OpenURL(url);
    }
}