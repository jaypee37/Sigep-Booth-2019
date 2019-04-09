using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteStaff : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private float targetAlpha;
    float startTime;
    // Use this for initialization
    // Start is called before the first frame update
    bool fadeOut = false;
    bool fadeIn = false;
    public Image[] Notes;
    public bool fadeInFinished = false;
    public Sprite[] NoteImages;
    public bool setNotes = false;
    public bool notesFade = false;
    
    void Start()
    {
        image = GetComponent<Image>();
        Color curColor = this.image.color;
        curColor.a = 0;
        this.image.color = curColor;
        FadeOutNotes();
        fadeInFinished = false;
    }
    IEnumerator WaitForNotes(int i,string[] notes)
    {
        if(i ==1)
        {
            SetDisplayNotes(notes);
            setNotes = true;
        }
        else
        {
            FadeOutNotes();
        }
        yield return new WaitUntil(() => Notes[0].color.a > .9f);
        setNotes = false;
        notesFade = true;
       
    }

    IEnumerator WaitForFade(int i, string[] notes)
    {
        
        if(i == 1)
        {
            fadeIn = true;
        }
        else if(i == 2)
        {
            
            fadeOut = true;
            
        }
       
        yield return new WaitUntil(() => image.color.a > .95f);
        fadeOut = false;
        fadeIn = false;
        fadeInFinished = true;

        print("done fading");


    }

    // Update is called once per frame
    void Update()
    {
        
        if (fadeIn)
        {
            Color curColor = this.image.color;

            curColor.a = Mathf.Lerp(curColor.a,1, 0.05f);
            this.image.color = curColor;
           

        }
       
        else if (fadeOut)
        {

            Color curColor = this.image.color;
           
            curColor.a = 0;
            this.image.color = curColor;
            FadeOutNotes();
        }
        if(setNotes)
        {
            FadeInNotes();
          
        }

    }
    public void Fade(int i,string[] notes)
    {
        fadeInFinished = false;
        StartCoroutine(WaitForFade(i,notes));
    }
    public void FadeNotes(int i,string[] notes)
    {
        notesFade = false;
        StartCoroutine(WaitForNotes(i, notes));
    }

    public void SetDisplayNotes(string[] notes)
    {
        if(notes != null)
        {
            for (int i = 0; i < 4; i++)
            {
                Notes[i].sprite = ChooseColor(notes[i]);
            }
        }
        
    }

    Sprite ChooseColor(string color)
    {
        Sprite retVal = null;
        switch(color)
        {
            case "Blue":                
                retVal =  NoteImages[0];
                break;
            case "Red":
                retVal = NoteImages[1];
                break;
            case "Yellow":
                retVal = NoteImages[2];
                break;
            case "Green":
                retVal = NoteImages[3];
                break;
        }
        return retVal;
    }

    public void FadeOutNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            Image note = Notes[i];
            Color whiteOut = note.color;
            whiteOut.a = 0;
            note.color = whiteOut;
        }
    }
    void FadeInNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            Image note = Notes[i];
            Color curColor = note.color;
            curColor.a = Mathf.Lerp(curColor.a, 1, 0.1f);
            note.color = curColor;
           
        }
    }
    
}
