using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    bool _vidStarted;
    bool gameStarted = false;
    bool vidEnd = false;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        StartCoroutine(PlayVid());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator PlayVid()
    {
        
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared)
        {
            yield return  waitForSeconds;
        }
        //yield return new WaitForSeconds(2);
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        _vidStarted = true;
        yield return new WaitForSeconds(5);
        vidEnd = true;
        StartCoroutine(StartGameScene());
        

    }

    IEnumerator StartGameScene()
    {
        yield return new WaitForSeconds(1);        
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Game);
        
    }
}
