using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementType2 : MonoBehaviour
{
    private Vector2 currentTarget;
    public Vector2[] targets;
    public float speed = 1.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = targets[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.x >= targets[0].x)
        {
            currentTarget = targets[1];
        }

        else if (this.transform.localPosition.x <= targets[1].x) 
        {
            currentTarget = targets[0];
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, currentTarget, speed * Time.deltaTime);
    }

    public void Movement()
    {
        float x = Random.Range(-0.5f, 0.5f), y = Random.Range(-0.5f, 0.5f);

        if (transform.localPosition.x >= 1) 
        {
            transform.InverseTransformDirection(new Vector3(1, transform.position.y, 0));
            x = Random.Range(-1f, 0);
        }
        else if (transform.localPosition.x <= -1)
        {
            transform.InverseTransformDirection(new Vector3(-1, transform.position.y, 0));
            x = Random.Range(0f, 1f);
        }

        if (transform.localPosition.y >= 0)
        {
            transform.InverseTransformDirection(new Vector3(transform.position.x, 0, 0));
            y = Random.Range(-1f, 0);
        }

        else if (transform.localPosition.y <= -1)
        {
            transform.InverseTransformDirection(new Vector3(transform.position.x, -1, 0));
            y = Random.Range(0f, 1f);
        }
        transform.localPosition = new Vector3(x, y, 0);
        currentTarget.Set(currentTarget.x, transform.localPosition.y);
        for (int i = 0; i < targets.Length; i++) {
            targets[i].Set(targets[i].x, transform.localPosition.y);
        }
    }

    
}
