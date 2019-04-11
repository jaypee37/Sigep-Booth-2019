using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    static public int difficulty;
    bool _playButton = false;
    AudioSource _audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        
        _audioSource = GetComponent<AudioSource>();
        Input.ResetInputAxes();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playButton)
        {
            

            if (Input.GetButtonDown("Green"))
            {
                _playButton = true;
                SceneHandler.instance.difficulty = SceneHandler.Difficulty.Easy;
                StartCoroutine(StartOpeningScene());
            }
            else if (Input.GetButtonDown("Red"))
            {
                _playButton = true;
                SceneHandler.instance.difficulty = SceneHandler.Difficulty.Medium;
                StartCoroutine(StartOpeningScene());
            }
            else if (Input.GetButtonDown("Yellow"))
            {
                _playButton = true;
                SceneHandler.instance.difficulty = SceneHandler.Difficulty.Hard;
                StartCoroutine(StartOpeningScene());
            }
 
            
        }
    }
    IEnumerator StartOpeningScene()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Opening);
        Destroy(SceneHandler.instance._audioSource.gameObject);
    }
   

}
