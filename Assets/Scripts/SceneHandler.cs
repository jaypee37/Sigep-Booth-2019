using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Enums

    public enum Scene {
        Start,
        Difficulty,
        Opening,
        Game,
        Win,
        Loss
        
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
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

    public Difficulty difficulty;

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
            case Scene.Difficulty:
                SceneManager.LoadScene("Difficulty", LoadSceneMode.Single);
                break;
            case Scene.Opening:
                SceneManager.LoadScene("Opening Scene", LoadSceneMode.Single);
                break;
            case Scene.Game:
                SceneManager.LoadScene("JayPee Scene", LoadSceneMode.Single);
                break;
            case Scene.Win:
                SceneManager.LoadScene("Win", LoadSceneMode.Single);
                break;
            case Scene.Loss:
                SceneManager.LoadScene("Lose", LoadSceneMode.Single);
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

    public void SetFadedAndCanvas(bool state, Canvas canvas)
    {
        _faded = state;
        _fadeCanvas = canvas;
    }

    #endregion
}
