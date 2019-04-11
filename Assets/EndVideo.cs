using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource _audioSource;
    bool _vidStarted;
    bool gameStarted = false;
    bool vidEnd = false;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(PlayVid());
    }

    // Update is called once per frame
    void Update()
    {
        if (vidEnd && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(StartMenuScene());
        }
    }
    IEnumerator PlayVid()
    {

        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
        }
        //yield return new WaitForSeconds(2);
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        _vidStarted = true;
        yield return new WaitWhile(() => videoPlayer.isPlaying);
        vidEnd = true;
    }

    IEnumerator StartMenuScene()
    {
        yield return new WaitForSeconds(1);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }
}

