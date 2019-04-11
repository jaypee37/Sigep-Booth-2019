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
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && !_playButton)
        {
            SceneHandler.instance.difficulty = SceneHandler.Difficulty.Medium;
            _playButton = true;
            StartCoroutine(StartOpeningScene());
            
        }
    }
    IEnumerator StartOpeningScene()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(2.0f);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Opening);
    }

}
