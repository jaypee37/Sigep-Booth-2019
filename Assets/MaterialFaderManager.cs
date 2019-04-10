using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFaderManager : MonoBehaviour
{
    public GameObject[] materialsToFade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeOutMaterials()
    {
        foreach(GameObject g in materialsToFade)
        {
            g.SetActive(false);
        }
    }
    
    public void FadeInMaterials()
    {
        foreach (GameObject g in materialsToFade)
        {
            g.SetActive(true);
        }
    }
}
