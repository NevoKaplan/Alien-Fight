using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bibi : MonoBehaviour
{
    [SerializeField] Text text;
    
    public void OpenURL() 
    {
        Application.OpenURL("https://nevokaplan4.wixsite.com/bibis-adventure");
        text.text = "Check Your Browser";
    }

    void Update()
    {
        if (Input.anyKey) {
            text.text = "Click On Me\n<-";
        }
    }
}
