using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public Rigidbody2D rb;
    public float speed = 40f;
    private float horizontal = 0f;
    public int lives;
    private bool isInvincible;
    [SerializeField] private float invincibilityDurationSeconds = 1.5f, invincibilityDeltaTime = 0.15f;
    [SerializeField] private GameObject player;
    private Vector3 largeScale;
    private Animator anim;
    Weapon weapon;
    AudioManager audioManager;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine("Spawn");
        largeScale = new Vector3(0.36f, 0.36f, 1);
        anim = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();
    }

   

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Horizontal", horizontal);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime, 0);
    }

    public void TakeDamage() {
        if (isInvincible) { return; }
        this.lives--;
        
        if (lives > 0)
        {
            audioManager.play("PlayerHit");
            StartCoroutine("Spawn");
        }
        else { Debug.Log("Player Destroyed");  Destroy(gameObject);}
    }

    private IEnumerator Spawn()
    {
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {

            if (player.transform.localScale == largeScale)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(largeScale);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        ScaleModelTo(largeScale);
        isInvincible = false;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        player.transform.localScale = scale;
    }

    public void Restart()
    {
        weapon.OGspeed = speed;
    }

    private void OnBecameVisible()
    {
       StartCoroutine("Spawn");
    }

}
