using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WinPictureManager : MonoBehaviour
{
    public Image rawImage;
    bool gameStarted = false;
    float _fadeTime = 2.0f;
    bool _faded = true;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(ReturnToMenu());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(4);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }
}
