using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody2D rb;
    public float damage;
    


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.playSound("BulletSound");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        StartCoroutine(DestroySelfAfterSeconds( destroyTime: 2f));
    }
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null && enemy.health > 0)
        {
            enemy.TakeDamage(damage);
            if (enemy.name == "Enemy 2")
            {
                enemy.whenHit2();
            }
        }
        if (hitInfo.name != "Bullet(Clone)" && hitInfo.name != "Missile(Clone)")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelfAfterSeconds(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
