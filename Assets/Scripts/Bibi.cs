using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bibi : MonoBehaviour
{
    
    [SerializeField] Sprite happy, sad;
    [SerializeField] Text text;
    private Image image;
    
    public void OpenURL() 
    {
        Application.OpenURL("https://nevokaplan4.wixsite.com/bibis-adventure");
        text.text = "Check Your Browser";
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ImageChange()
    {
        image.sprite = happy;
    }

    public void ImageChangeBack()
    {
        image.sprite = sad;
        text.text = "Click On Me\n<-";
    }
}
