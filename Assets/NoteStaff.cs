﻿using System.Collections;
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
    void Start()
    {
        image = GetComponent<Image>();
        Color curColor = this.image.color;
        curColor.a = 0;
        this.image.color = curColor;
    }

    IEnumerator FadeIn()
    {
        fadeOut = true;
        yield return new WaitForSeconds(5.5f);
        fadeOut = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut)
        {
            Color curColor = this.image.color;
            print(curColor.a);
            curColor.a = Mathf.Lerp(curColor.a,1, 0.01f);
            this.image.color = curColor;
        }

    }
    public void Fade()
    {
        StartCoroutine(FadeIn());
    }

}