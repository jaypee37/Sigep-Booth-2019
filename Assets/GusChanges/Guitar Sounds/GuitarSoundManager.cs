using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarSoundManager : MonoBehaviour
{
    AudioClip greenAudio; 
    AudioClip redAudio;
    AudioClip yellowAudio;
    AudioClip blueAudio;
    AudioClip DeLaVoice;
    public AudioSource _audioSource;

    void Start() {
        greenAudio = (AudioClip)Resources.Load("Green");
        redAudio = (AudioClip)Resources.Load("Red");
        yellowAudio = (AudioClip)Resources.Load("Yellow");
        blueAudio = (AudioClip)Resources.Load("Blue");
        DeLaVoice = (AudioClip)Resources.Load("DeLaVoice");
    }

    
    void Update()
    {
        
    }

    public void playGreen()
    {
        GetComponent<AudioSource>().PlayOneShot(greenAudio);
    }
    public void playRed()
    {
        GetComponent<AudioSource>().PlayOneShot(redAudio);
    }
    public void playYellow()
    {
        GetComponent<AudioSource>().PlayOneShot(yellowAudio);
    }
    public void playBlue()
    {
        GetComponent<AudioSource>().PlayOneShot(blueAudio);
    }
    public void playDeLaVoice()
    {
        _audioSource.Play();
    }
    public void PlayStrum(string color)
    {
        switch(color)
        {
            case "Green":
                playGreen();
                break;
            case "Red":
                playRed();
                break;
            case "Yellow":
                playYellow();
                break;
            case "Blue":
                playBlue();
                break;
        }
    }
}
