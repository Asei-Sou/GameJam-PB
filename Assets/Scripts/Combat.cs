using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Combat : MonoBehaviour
{
    public Unit hero;
    public Unit enemy;

    public TextMeshProUGUI heroUI;
    public TextMeshProUGUI enemyUI;

    private int heroHP;
    private int enemyHP;

    public bool playerAttacking;
    public bool enemyAttacking;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        heroHP = hero.maxHealth;
        enemyHP = enemy.maxHealth;

        playerAttacking = true;
        enemyAttacking = true;

        updateHealth();
    }

    public void enablePlayerAttacking()
    {
        playerAttacking = true;
    }

    public void disablePlayerAttacking()
    {
        playerAttacking = false;
    }

    public void enableEnemyAttacking()
    {
        enemyAttacking = true;
    }

    public void disableEnemyAttacking()
    {
        enemyAttacking = false;
    }

    void getUnitsHealth()
    {
        heroHP = hero.getHealth();
        enemyHP = enemy.getHealth();
    }

    void setUnitsHealth()
    {
        hero.setHealth(heroHP);
        enemy.setHealth(enemyHP);
    }

    void updateHealth()
    {
        heroUI.text = heroHP.ToString();
        enemyUI.text = enemyHP.ToString();
    }

    void aWinnerIsYou(string winner)
    {
        Debug.Log(winner + " wins!");
        gameOver = true;
    }

    bool rollToHit(int modifier, int defence)
    {
        int roll = Random.Range(1, 21); // 1 to 20
        if (roll + modifier >= defence)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int rollDamage(int modifier, int diceSize)
    {
        int roll = Random.Range(1, diceSize+1);
        return roll + modifier;
    }

    void playerAttack()
    {
        bool hit = rollToHit(hero.attack, enemy.defence);
        if (hit)
        {
            int damage = rollDamage(hero.damage, hero.diceSize);
            enemyHP -= damage;
            Debug.Log("Player Hit! Dealt " + damage + " damage.");
        }
        else
        {
            Debug.Log("Player Missed!");
        }
    }

    void enemyAttack()
    {
        bool hit = rollToHit(enemy.attack, hero.defence);
        if (hit)
        {
            int damage = rollDamage(enemy.damage, enemy.diceSize);
            heroHP -= damage;
            Debug.Log("Enemy Hit! Dealt " + damage + " damage.");
        }
        else
        {
            Debug.Log("Enemy Missed!");
        }
    }

    public void combat()
    {
        if (!gameOver)
        {
            getUnitsHealth();

            if (playerAttacking)
            {
                playerAttack();

                if (enemyHP <= 0)
                {
                    enemyHP = 0;
                    aWinnerIsYou("Player");
                    setUnitsHealth();
                    updateHealth();
                    return;
                }
            }

            if (enemyAttacking)
            {
                enemyAttack();

                if (heroHP <= 0)
                {
                    heroHP = 0;
                    aWinnerIsYou("Enemy");
                    setUnitsHealth();
                    updateHealth();
                    return;
                }

            }

            setUnitsHealth();
            updateHealth();
        }
        
    }
}
