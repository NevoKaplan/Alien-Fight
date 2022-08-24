    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public float health = 100;
    private EnemyMovementType2 enemy;
    public System.Action killed;
    private GameManager gameManager;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = GetComponentInParent<GameManager>();
        enemy = GetComponent<EnemyMovementType2>();
        if (enemy != null)
        {
            health = gameManager.wave * 20;
        }
        else 
        {
            health = gameManager.wave * 15;
        }
    }



    public void TakeDamage(float damage) {
        this.health -= damage;
        if (this.health <= 0) {
            Die();
        }
    }

    public void whenHit2() 
    {
        enemy.Movement();
    }

    void Die() {
        if (enemy != null)
            audioManager.play("PurpleDeath");
        else
            audioManager.play("RedDeath");
        Destroy(transform.parent.gameObject);
        Destroy(Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity)as GameObject, 2);
        Destroy(gameObject);
        this.killed.Invoke();
    }
}
