using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Destroy()
    {
        this.gameObject.SetActive(false);
    }

    public void Revive() 
    {
        this.gameObject.SetActive(true);
    }
}
