using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    #region Serialize fields

    [SerializeField]
    TextMeshProUGUI _playButtonText;

    [SerializeField, Range(0.0f, 10f)]
    float _fadeTime = 1f;

    #endregion

    #region Private fields

    bool _gameStarted;
    bool _fadingIn;
    float _textVisibility;
    float _timeElapsed;
    bool _playButton;

    AudioSource _audioSource;

    #endregion

    #region Unity events

    private void Awake() {
        _fadingIn = false;
        _gameStarted = false;
        _textVisibility = 1.0f;
        _playButton = false;

        _audioSource = GetComponent<AudioSource>();

        StartCoroutine("FadeText");
    }

    private void Update() {
        if (Input.GetButtonDown("Submit") && !_playButton)
        {
            _playButton = true;
            StartCoroutine("StartGame");
        }
    }

    #endregion

    #region Private methods

    #endregion

    #region Coroutines

    IEnumerator FadeText() {
        while (!_gameStarted) {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _fadeTime) {
                _timeElapsed = 0f;
                _fadingIn = !_fadingIn;
            }

            _textVisibility = (_fadingIn) ? (_timeElapsed / _fadeTime) : 1 - (_timeElapsed / _fadeTime);
            _playButtonText.color = new Color(_textVisibility, _textVisibility, _textVisibility);
            yield return null;
        }

        _playButtonText.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        _playButtonText.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        _playButtonText.color = Color.yellow;
    }

    IEnumerator StartGame() {
        _gameStarted = true;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);

        Destroy(_audioSource.gameObject);
        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Difficulty);
    }

    #endregion
}
