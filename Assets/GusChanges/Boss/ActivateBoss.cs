using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    public BossDeLaCruzMvt bossMove;
    public GameObject bossScripts;
    public BossKillPlayer bossKill;

    public void Activate() {
        bossScripts.SetActive(true);
        bossMove.enabled = true;
        bossKill.enabled = true;
    }
}
