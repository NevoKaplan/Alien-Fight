using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public int numOfHearts;
    [SerializeField] int health;
    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Sprite Heart;
    [SerializeField] float space;
    [SerializeField] GameObject[] hearts;
    Image[] copy;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject player;
    private PlayerMovement lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = player.GetComponent<PlayerMovement>();
        hearts = new GameObject[numOfHearts];
        copy = new Image[numOfHearts];
        for (int j = 0; j < numOfHearts; j++) {
            //GameObject tmp = new GameObject();
            Vector3 tmp = new Vector3(space * j, 0, 0);
            //hearts[j] = Instantiate(tmp, transform.position + tmp.transform.position, Quaternion.identity);
            hearts[j] = new GameObject();
            hearts[j].transform.SetParent(transform);
            hearts[j].AddComponent<Image>();
            hearts[j].GetComponent<RectTransform>().transform.localScale = new Vector3(0.4f, 0.4f, 1);
            hearts[j].transform.SetPositionAndRotation(this.transform.position + tmp, Quaternion.identity);
            copy[j] = hearts[j].GetComponent<Image>();
            
        }
    }

    public void Restart()
    {
        for (int i = 0; i < numOfHearts - 1; i++)
        {
            Destroy(copy[i]);
        }
        hearts = new GameObject[numOfHearts];
        copy = new Image[numOfHearts];
        for (int j = 0; j < numOfHearts; j++)
        {
            //GameObject tmp = new GameObject();
            Vector3 tmp = new Vector3(space * j, 0, 0);
            //hearts[j] = Instantiate(tmp, transform.position + tmp.transform.position, Quaternion.identity);
            hearts[j] = new GameObject();
            hearts[j].transform.SetParent(transform);
            hearts[j].AddComponent<Image>();
            hearts[j].GetComponent<RectTransform>().transform.localScale = new Vector3(0.4f, 0.4f, 1);
            hearts[j].transform.SetPositionAndRotation(this.transform.position + tmp, Quaternion.identity);
            copy[j] = hearts[j].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        health = lives.lives;
        for (int i = 0; i < numOfHearts; i++)
        {
            if (i < health)
            {
                copy[i].sprite = Heart;
            }
            else 
            { 
                copy[i].sprite = EmptyHeart;
            }
        }
        if (health <= 0) 
        {
            for (int i = 0; i < numOfHearts; i++)
            {
                Destroy(copy[i]);
            }
        }
    }

}
