using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFader : MonoBehaviour
{
    public NoteStaff staff;

    //Will cause a problem if someone can do the entire combo really quickly
    IEnumerator SecondaryEnsurance()
    {
        yield return new WaitForSeconds(2);
        staff.fadeInFinished = true;
    }

    //Sometimes fadeInFinished doesn't get turned to true
    public void fade(int i, string[] sequence) {
        staff.Fade(i, sequence);
        //MAYBE REMOVE: BUT ELIMINATES BUG:
        SecondaryEnsurance();
    }
}
