 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField] float speedMin, speedMax;
    private Rigidbody2D rb;
    public int wave;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedMin += (wave * 1.4f);
        if (speedMin < speedMax) { rb.velocity = transform.up * -Random.Range(speedMin, speedMax); }
        else { rb.velocity = transform.up * -speedMax; }
        
        StartCoroutine(DestroySelfAfterSeconds(destroyTime: 6f));
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        Bunker bunker = hitInfo.GetComponent<Bunker>();
        if (player != null)
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
        else if (bunker != null)
        {
            bunker.Destroy();
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelfAfterSeconds(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
