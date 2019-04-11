using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Canvas timer;
    Image timerImage;
    TextMeshProUGUI numberText;
    bool numberFadeIn = false;
    bool numberFadeOut = false;
    float _timeElapsed;
    public bool fading = false;
    int numberTextIndex;
    float staffFadeTime = 1.2f;
    float staffFadeTimeElapsed;
    int MaxNumberIndex;
    
    void SetNoteTransforms()
    {

    }
    void SetNumberText(SceneHandler.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case SceneHandler.Difficulty.Easy:
                MaxNumberIndex = 10;
                break;
            case SceneHandler.Difficulty.Medium:
                MaxNumberIndex = 6;
                break;
            case SceneHandler.Difficulty.Hard:
                MaxNumberIndex = 4;
                break;
        }
    }
    void Start()
    {
        SetNumberText(SceneHandler.instance.difficulty);
        image = GetComponent<Image>();
        Color curColor = this.image.color;
        curColor.a = 0;
        this.image.color = curColor;
        
        //
        timerImage = timer.GetComponent<Image>();
        Color timerColor = timerImage.color;
        timerColor.a = 0;
        timerImage.color = timerColor;

        numberText = timer.GetComponentInChildren<TextMeshProUGUI>();
        numberText.text = MaxNumberIndex.ToString();
        Color numColor = numberText.color;
        numColor.a = 0;
        numberText.color = numColor;

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

 
    IEnumerator WaitForFadeTimerNumbers()
    {
        numberFadeIn = true;
        fading = true;
        numberText.text = MaxNumberIndex.ToString();
        numberTextIndex = MaxNumberIndex;

        print("open up timer");
        timerImage = timer.GetComponent<Image>();
        Color timerColor = timerImage.color;
        timerColor.a = 1;
        timerImage.color = timerColor;

        yield return new WaitForSeconds(0);
        


    }

    // Update is called once per frame
    void Update()
    {
        
        if (fadeIn)
        {
            

            staffFadeTimeElapsed += Time.deltaTime;
            if (staffFadeTimeElapsed >= staffFadeTime)
            {
                fadeIn = false;
                fadeOut = false;
                fadeInFinished = true;
               
                
            }

            Color curColor = this.image.color;
            curColor.a = (staffFadeTimeElapsed / staffFadeTime);
            this.image.color = curColor;

            Color timerColor = timerImage.color;
            timerColor.a = (staffFadeTimeElapsed / staffFadeTime);
            timerImage.color = timerColor;

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
        if(fading)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= .5f)
            {
                if (!numberFadeIn && numberTextIndex == 0)
                {
                    fading = false;
                    numberTextIndex = MaxNumberIndex;
                    Color timerColor = numberText.color;
                    timerColor.a = 0;
                    numberText.color = timerColor;

                }
                else
                {
                    _timeElapsed = 0f;
                    if (!numberFadeIn)
                    {
                        numberTextIndex -= 1;
                        numberText.text = numberTextIndex.ToString();
                    }

                    numberFadeIn = !numberFadeIn;
                }
                
            }
            if(fading)
            {
                float _textVisibility = (numberFadeIn) ? (_timeElapsed / .5f) : 1 - (_timeElapsed / .5f);
                numberText.color = new Color(_textVisibility, _textVisibility, _textVisibility);
            }

            
        }
     

    }

    public void StopTimer()
    {
        StopAllCoroutines();
        fading = false;
        numberTextIndex = MaxNumberIndex;
        Color timerColor = numberText.color;
        timerColor.a = 0;
        numberText.color = timerColor;

        timerColor = timerImage.color;
        timerColor.a = 0;
        timerImage.color = timerColor;
        
    }
    public void FadeTimerNumbersInandOut()
    {
        StartCoroutine(WaitForFadeTimerNumbers());
    }
    public void FadeStaff(int i, string[] notes)
    {
        staffFadeTimeElapsed = 0;
        fadeInFinished = false;
        if (i == 1)
        {
            fadeIn = true;
        }
        else if (i == 2)
        {

            fadeOut = true;

        }
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
                Notes[i].color = Color.white;
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
    public void GrayOutNote(int i)
    {
        Notes[i].color = Color.gray;
    }
    
}
