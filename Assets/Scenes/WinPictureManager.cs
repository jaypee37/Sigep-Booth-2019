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
        StartCoroutine(FadeScreen());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator FadeScreen()
    {

        float timeElapsed = 0.0f;
        while (timeElapsed < _fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float alpha = (timeElapsed / _fadeTime);
            rawImage.color = new Color(alpha, alpha, alpha, 255);
            
            yield return null;
        }
       
    }
    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }
}
