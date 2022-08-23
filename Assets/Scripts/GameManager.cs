using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyArr;
    public int rows = 4, cols = 13;
    public float rowSpace = 2, colSpace = 2;
    public int amountKilled { get; private set; }
    private int maxAmount => rows * cols;
    public GameObject Missile;
    public GameObject Bunker;
    [SerializeField] Text score, waveText;
    private float times => 1000f / maxAmount;
    public int wave, bunkerNum;
    [SerializeField] GameObject WaveComplete, LostScreen, HeartContainer, laserIcon;
    public GameObject player;
    public PlayerMovement playerMovement;
    private Hearts hearts;
    private List<GameObject> bunkers;
    private Bunker[][] bunk;
    public int addBunker;
    private bool wClicked;
    [SerializeField] Text wText;



    // Start is called before the first frame update
    void Start()
    {
        addBunker = 0;
        playerMovement = player.GetComponent<PlayerMovement>();
        hearts = HeartContainer.GetComponent<Hearts>();
        Restart();
        score.text = "0";
        bunkers = new List<GameObject>();
        bunk = new Bunker[bunkerNum][];
        for (int i = 0; i < bunkerNum; i++) {
            bunkers.Add(Instantiate(Bunker, this.transform));
            Vector2 po = new Vector2(-8 + ((15.25f / (bunkerNum-1)) * i), -2.62f);
            bunkers[i].transform.position = po;
            bunk[i] = bunkers[i].GetComponentsInChildren<Bunker>();
        }
        InvokeRepeating("missileAttack", 3f, 1.25f);
    }

    void Update()
    {
        if (playerMovement.lives <= 0) {
            LostScreen lostScreen = LostScreen.GetComponent<LostScreen>();
            lostScreen.gameScore = int.Parse(score.text);
            laserIcon.SetActive(false);
            LostScreen.SetActive(true); 
            Destroy(gameObject); }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(0);
        }
        if (!wClicked && Input.GetKeyDown(KeyCode.W))
        {
            wClicked = true;
            wText.text = "W";
        }
    }

    public void Restart()
    {
        WaveComplete.SetActive(false);
        wave++;
        if (wave > 1)
        {
            for (int i = 0; i < bunk.Length; i++)
            {
                for (int j = 0; j < bunk[0].Length; j++)
                {
                    if (!bunk[i][j].isActiveAndEnabled && Random.Range(0, 2) == 1)
                    {
                        bunk[i][j].Revive();
                    }
                }
                bunkers[i].SetActive(true);
            }
        }

        if (addBunker > 0)
        {
            
            Vector2 po;
            Bunker[][] bunk2 = new Bunker[bunk.Length][];
            for (int i = 0; i < bunk.Length; i++) {
                bunk2[i] = bunk[i];
            }
            bunk = new Bunker[bunkerNum][];
            for (int i = 0; i < bunk2.Length; i++) {
                bunk[i] = bunk2[i];
            }
            for (int i = 0; i < bunkers.Count; i++)
            {
                po = new Vector2(-8f + ((15.25f / (bunkerNum-1)) * i), -2.62f);
                bunkers[i].transform.position = po;
            }
            for (int i = addBunker; i > 0; i--)
            {
                bunkers.Add(Instantiate(Bunker, this.transform));
                po = new Vector2(-8f + ((15.25f / (bunkerNum-1)) * (bunkerNum-i)), -2.62f);
                bunkers[bunkerNum -i].transform.position = po;
                bunk[bunkerNum -i] = bunkers[bunkerNum -i].GetComponentsInChildren<Bunker>();
            }
            addBunker = 0;
        }
        laserIcon.SetActive(true);
        player.SetActive(true);
        player.transform.position = new Vector2(0, -4);
        playerMovement.lives = hearts.numOfHearts;
        waveText.text = "Wave: " + wave;
        for (int row = 0; row < this.rows; row++)
        {
            Vector2 pos = new Vector2(0, row * rowSpace);
            for (int col = 0; col < this.cols; col++)
            {
                GameObject enemy = Instantiate(this.enemyArr[row], this.transform);
                pos.Set(col * colSpace, pos.y);
                enemy.transform.localPosition = pos;
                Enemy enemy2 = enemy.GetComponentInChildren<Enemy>();
                enemy2.killed += EnemyKilled;
            }
        }
        amountKilled = 0;
    }

    void missileAttack() 
    {
        foreach (Transform enemy in this.transform)
        {
            if (enemy.name != "Bunker(Clone)" && enemy.name != "Missile(Clone)")
            {
                if (Random.value * Random.Range(1f / (wave * 0.5f), 1.5f) < 1.0f / (maxAmount - amountKilled))
                {
                    Transform enemy2 = enemy.GetChild(0);
                    Missle missile = Instantiate(Missile, new Vector2(enemy2.position.x, (enemy2.position.y - 0.8f)), Quaternion.identity).GetComponent<Missle>();
                    missile.wave = wave;
                    missile.transform.SetParent(this.transform);
                }
            }
        }
    }

    void EnemyKilled() 
    {
        amountKilled++;
        score.text = (1000 * (wave-1) + (amountKilled * times)).ToString("0");
        if (amountKilled == maxAmount) {
            Enemy[] enemy = GetComponentsInChildren<Enemy>();
            for (int i = 0; i < enemy.Length; i++) {
                if (enemy[i] != null) {
                    Destroy(enemy[i].gameObject);
                }
            }
            laserIcon.SetActive(false);
            for (int i = 0; i < bunkers.Count; i++) {
                bunkers[i].SetActive(false);
            }
            player.SetActive(false);
            WaveComplete.SetActive(true);
        }
    }
}
