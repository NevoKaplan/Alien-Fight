using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10, xOffset = 5, yOffset = 3, waitTime = 1, moveTime = 2;
    public Transform A, B, C, D;
    private Vector2 currentTarget;
    private float waitTimeReset, moveTimeReset;

    public enum Etat 
    {
        Move,
        Wait
    }

    public Etat etat = Etat.Move;


    // Start is called before the first frame update
    void Start()
    {
        moveTimeReset = moveTime;
        waitTimeReset = waitTime;
        currentTarget = B.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (etat) 
        {
            case Etat.Move:
                Movement();
                break;
            case Etat.Wait:
                Wait();
                break;
        
        }
    }

    void Movement() 
    {
        if (transform.position.x == A.position.x && transform.position.y >= A.position.y)
        {
            transform.position = A.position;
            currentTarget = B.position;
        }

        else if (transform.position.x >= B.position.x && transform.position.y == B.position.y)
        {
            transform.position = B.position;
            currentTarget = C.position;
        }

        else if (transform.position.x == C.position.x && transform.position.y <= C.position.y)
        {
            transform.position = C.position;
            currentTarget = D.position;
        }

        else if (transform.position.x <= D.position.x && transform.position.y == D.position.y)
        {
            transform.position = D.position;
            currentTarget = A.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        moveTime -= Time.deltaTime;

        if (moveTime <= 0)
        {
            etat = Etat.Wait;
            moveTime = moveTimeReset;
        }
    }

    void Wait() {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0) 
        {
            etat = Etat.Move;
            waitTime = waitTimeReset;
        }
    }

    void FixedUpdate()
    {
        
    }
}
