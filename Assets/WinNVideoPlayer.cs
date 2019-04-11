using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WinNVideoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public RawImage fadeCanvas;
    public VideoPlayer videoPlayer;
    bool _vidStarted;
    bool gameStarted = false;
    bool vidEnd = false;
    float _fadeTime = 5f;
    bool _faded = true;
    public Canvas winScreen;
    
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(PlayVid());
    }
    void Start()
    {
        StartCoroutine(FadeInScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vidEnd && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(ReturnToMenu());
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
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        _vidStarted = true;
        //yield return new WaitWhile(() => videoPlayer.isPlaying);
        yield return new WaitForSeconds(23f);
        StartCoroutine(FadeOutScreen());
        


    }
    IEnumerator FadeInScreen()
    {
        
        float timeElapsed = 0.0f;
        while (timeElapsed < 5f)
        {
            timeElapsed += Time.deltaTime;
            float alpha =(timeElapsed / 2f);
            float color = Mathf.Lerp(0, 255, 0.05f);
            rawImage.color = new Color(color, color, color, 255);

            yield return null;
        }
        
    }

    IEnumerator FadeOutScreen()
    {
        _faded = !_faded;
        float timeElapsed = 0.0f;
        while (timeElapsed < _fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float alpha = 1.0f - (timeElapsed / _fadeTime);
            rawImage.color = new Color(alpha, alpha,alpha, 255);
            videoPlayer.SetDirectAudioVolume(0, alpha);
            yield return null;
        }
        StartCoroutine(ReturnToMenu());

    }
    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1);
        SceneHandler.instance.SetFadedAndCanvas(false, winScreen);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.WinPicture);
        //SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }
}
