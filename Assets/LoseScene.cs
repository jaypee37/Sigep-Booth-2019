using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    public Canvas StartScreen;
    void Awake()
    {
        
    }

    IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(6);
        //SceneHandler.instance.SetFadedAndCanvas(false,StartScreen);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( WaitForSceneSwitch());
    }
}
