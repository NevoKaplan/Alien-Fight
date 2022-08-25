using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveComplete : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button bunker, heart, dmg, speed;
    [SerializeField] GameObject heartContainer;
    [SerializeField] Text Stats, chooseText;
    private int dmglvl = 1, rpdlvl = 1, spdlvl = 1;
    private bool letMult;

    public void UpdateText()
    {
        if (gameManager != null)
        {
            Hearts hearts = heartContainer.GetComponent<Hearts>();
            Stats.text = "<color=green>Bunker lvl:</color> " + gameManager.bunkerNum + "\n<color=red>Heart lvl:</color> " + hearts.numOfHearts + "\nDamage lvl: " + dmglvl + "\n<color=#726F72>Speed lvl:</color> " + spdlvl + "\n<color=#1EB5DC>FireRate lvl:</color> " + rpdlvl;

        }
        if (gameManager.wave % 4 == 0)
        {
            letMult = true;
            chooseText.text = "Choose An Upgrade\n<b><i><color=yellow>Choose 1 Get 1 Free Round!</color></i></b>";
        }
        else { letMult = false;
            chooseText.text = "Choose An Upgrade";
        }
    }

    void Restart() 
    {
        AudioManager.playSound("UpgradeChosen");
        gameManager.Restart(); 
    }

    public void OnSpeed() 
    {
        if (gameManager != null)
        {
            gameManager.playerMovement.speed *= 1.33f;
            spdlvl++;
            if (letMult) {
                letMult = false;
                gameManager.playerMovement.speed *= 1.33f;
                spdlvl++;
            }
            gameManager.playerMovement.Restart();
            Restart();
        }
    }

    public void OnBunker()
    {
        if (gameManager != null)
        {
            gameManager.addBunker = 1;
            gameManager.bunkerNum++;
            if (letMult) 
            {
                gameManager.addBunker = 2;
                gameManager.bunkerNum++;
            }
            Restart();
        }
    }

    public void OnHeart()
    {
        if (gameManager != null)
        {
            
            Hearts hearts = heartContainer.GetComponent<Hearts>();
            hearts.numOfHearts++;
            if (letMult)
            {
                letMult = false;
                hearts.numOfHearts++;
            }
            hearts.Restart();
            Restart();
        }
    }

    public void OnDMG()
    {
        if (gameManager != null)
        {
            Weapon weapon = gameManager.player.GetComponent<Weapon>();
            weapon.laserDamage *= 1.5f;
            weapon.bulletDamage *= 1.5f;
            dmglvl++;
            if (letMult) {
                letMult = false;
                weapon.laserDamage *= 1.5f;
                weapon.bulletDamage *= 1.5f;
                dmglvl++;
            }
            Restart();
        }
    }

    public void OnRapid() 
    {
        if (gameManager != null) 
        {
            Weapon weapon = gameManager.player.GetComponent<Weapon>();
            weapon.bulletsPerSecond += 0.75f;
            weapon.cooldown *= 0.92f;
            rpdlvl++;
            if (letMult) {
                letMult = false;
                weapon.bulletsPerSecond += 0.75f;
                weapon.cooldown *= 0.92f;
                rpdlvl++;
            }
            Restart();
        }
    }
}
