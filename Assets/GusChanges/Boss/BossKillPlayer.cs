using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossKillPlayer : MonoBehaviour
{
    bool gameEnded = false;
    public Canvas loseScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !gameEnded)
        {
            gameEnded = false;
            Debug.Log("GAME OVER MAN!");
            SceneHandler.instance.SetFadedAndCanvas(false, loseScreen);
            SceneHandler.instance.ChangeScene(SceneHandler.Scene.Loss);
        }
    }
}
