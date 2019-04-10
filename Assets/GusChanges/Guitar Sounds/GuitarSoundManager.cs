using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarSoundManager : MonoBehaviour
{
    AudioClip greenAudio; 
    AudioClip redAudio;
    AudioClip yellowAudio;
    AudioClip blueAudio;

    void Start() {
        greenAudio = (AudioClip)Resources.Load("green");
        redAudio = (AudioClip)Resources.Load("red");
        yellowAudio = (AudioClip)Resources.Load("yellow");
        blueAudio = (AudioClip)Resources.Load("blue");
    }

    // QUESTION: DO YOU WANT NEW NOTES TO STOP THE SOUND OF THE OLD NOTE? CURRENTLY I THINK THEY DO
    void Update()
    {
        if (Input.GetButtonDown("Green"))
        {
            GetComponent<AudioSource>().PlayOneShot(greenAudio);
        }
        else if (Input.GetButtonDown("Red"))
        {
            Debug.Log("YESSS");
            GetComponent<AudioSource>().PlayOneShot(redAudio);
        }
        else if (Input.GetButtonDown("Yellow"))
        {
            GetComponent<AudioSource>().PlayOneShot(yellowAudio);
        }
        else if (Input.GetButtonDown("Blue"))
        {
            GetComponent<AudioSource>().PlayOneShot(blueAudio);
        }
    }
}
