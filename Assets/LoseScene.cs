using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(5);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Start);
    }

    // Start is called before the first frame update
    void Start()
    {
        WaitForSceneSwitch();
    }
}
