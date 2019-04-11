using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossKillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("GAME OVER MAN!");
            //SceneHandler.instance.SetFadedAndCanvas(true, loseScreen);
            //SceneHandler.instance.ChangeScene(SceneHandler.Scene.Lose);
        }
    }
}
