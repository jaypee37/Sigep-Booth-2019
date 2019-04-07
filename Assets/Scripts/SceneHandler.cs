﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Enums

    public enum Scene {
        Start,
        Game,
        Win,
        Loss
    }

    #endregion

    #region Instance 

    public static SceneHandler instance = null;

    #endregion

    #region Serialize fields

    [SerializeField]
    Canvas _fadeCanvas;

    [SerializeField, Range(0f, 10f)]
    float _fadeTime = 1.0f;

    #endregion

    #region Private fields

    bool _faded;
    Image _fadeImage;

    #endregion

    #region Unity events

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(_fadeCanvas.gameObject);
        _fadeImage = _fadeCanvas.GetComponentInChildren<Image>();
        _faded = false;
    }

    #endregion

    #region Coroutine

    IEnumerator ChangeSceneHelper(Scene newScene) {
        StartCoroutine("FadeScreen");
        yield return new WaitForSeconds(_fadeTime);

        switch (newScene) {
            case Scene.Start:
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                break;
            case Scene.Game:
                SceneManager.LoadScene("JayPee Scene", LoadSceneMode.Single);
                break;
            case Scene.Win:
                break;
            case Scene.Loss:
                break;
        }

        yield return new WaitForSeconds(_fadeTime);
        StartCoroutine("FadeScreen");
    }

    IEnumerator FadeScreen() {
        _faded = !_faded;
        float timeElapsed = 0.0f;
        while (timeElapsed < _fadeTime) {
            timeElapsed += Time.deltaTime;
            float alpha = (_faded) ? (timeElapsed / _fadeTime) : 1.0f - (timeElapsed / _fadeTime);
            _fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
        float endAlpha = (_faded) ? 1.0f : 0.0f;
        _fadeImage.color = new Color(0.0f, 0.0f, 0.0f, endAlpha);
    }

    #endregion

    #region Public methods

    public void ChangeScene(Scene newScene) {
        StartCoroutine(ChangeSceneHelper(newScene));
    }

    public float GetFadeTime() {
        return _fadeTime;
    }

    #endregion
}
