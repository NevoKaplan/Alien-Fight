using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] LineRenderer circleRenderer;
    [SerializeField] int steps;
    [SerializeField] float radius;
    [SerializeField] Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        DrawCircle(steps, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawCircle(int steps, float radius) 
    {
        circleRenderer.positionCount = steps + 1;

        for (int currentStep = 0; currentStep <= steps; currentStep++) {
           
            float circumferenceProgress = (float)currentStep / steps;
            
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition + transform.position);
        }
    }
}
