using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bullet;
    public LineRenderer laser;
    public float laserDamage, bulletDamage;
    public float cooldown;
    float timer, timer2;
    private bool shootClicked;
    public float bulletsPerSecond;
    [SerializeField] PlayerMovement player;
    public float OGspeed;
    [SerializeField] Image coverImage;


    // Start is called before the first frame update
    void Start()
    {
        shootClicked = false;
        bulletsPerSecond = 4f;
        player = GetComponent<PlayerMovement>();
        OGspeed = player.speed;
        Debug.Log("OGSPEED: " + OGspeed);
        coverImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        if (timer <= 0)
        {
            coverImage.fillAmount = 0;
            if (Input.GetButtonDown("Fire2"))
            {
                AudioManager.playSound("LaserSound");
                StartCoroutine(shootLaser());
                timer = cooldown;
            }
        }
        else 
        {
            if (Input.GetButtonDown("Fire2"))
                AudioManager.playSound("CantFire");
            coverImage.fillAmount = timer / cooldown;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shootClicked = true;
            player.speed *= 0.6f;
            Debug.Log(player.speed);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            shootClicked = false;
            player.speed = OGspeed;
        }
        if (shootClicked && timer2 <= 0) {
            shootBullet();
            timer2 = 1f/bulletsPerSecond;
        }
    }

    void shootBullet() {
        GameObject bull = Instantiate(bullet, firePoint.position, new Quaternion());
        (bull.GetComponent<Bullet>()).damage = bulletDamage;
    }
    
    IEnumerator shootLaser() {
        RaycastHit2D[][] hitInfo = new RaycastHit2D[3][];
        Vector2 p0 = new Vector2(firePoint.position.x - 0.07f, firePoint.position.y);
        Vector2 p1 = new Vector2(firePoint.position.x + 0.07f, firePoint.position.y);
        hitInfo[0] = Physics2D.RaycastAll(p0, firePoint.up);
        hitInfo[1] = Physics2D.RaycastAll(p1, firePoint.up);
        hitInfo[2] = Physics2D.RaycastAll(firePoint.position, firePoint.up);
        for (int j = 0; j < hitInfo.Length; j++) {
            Debug.Log(hitInfo[j]);
            for (int i = 0; i < hitInfo[j].Length; i++)
            {
                if (hitInfo[j][i])
                {
                    Enemy enemy = hitInfo[j][i].transform.GetComponent<Enemy>();
                    if (enemy != null && enemy.health > 0)
                    {
                        if (enemy.name == "Enemy 2")
                        {
                            enemy.whenHit2();
                        }
                        enemy.TakeDamage(laserDamage);
                    }
                }
            }
        }
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, firePoint.position + firePoint.up * 7.5f);

        laser.enabled = true;
        yield return new WaitForSeconds(0.04f);
        laser.enabled = false;
    }

    private void OnBecameVisible()
    {
        laser.enabled = false;
        player.speed = OGspeed;
    }

    private void OnBecameInvisible()
    {
        shootClicked = false;
    }
}
