using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Combat : MonoBehaviour
{
    public Unit hero;
    public Unit enemy;

    public Unit[] allies;

    public TextMeshProUGUI heroUI;
    public TextMeshProUGUI enemyUI;

    public TextMeshProUGUI[] allyUI;

    public int heroHP;
    public int enemyHP;
    public int[] allyHP;

    public bool playerAttacking;
    public bool enemyAttacking;
    public bool[] allyAttacking;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        heroHP = hero.maxHealth;
        enemyHP = enemy.maxHealth;

        int index = 0;
        foreach (Unit ally in allies)
        {
            if(ally.gameObject.activeSelf)
            {
                allyHP[index] = ally.maxHealth;
                allyAttacking[index] = true;
            }
            else
            {
                allyUI[index].gameObject.SetActive(false);
                allyAttacking[index] = false;
            }
            index++;
        }

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
        
        int index = 0;
        foreach (Unit ally in allies)
        {
            allyHP[index] = ally.getHealth();
            index++;
        }
    }

    void setUnitsHealth()
    {
        hero.setHealth(heroHP);
        enemy.setHealth(enemyHP);

        int index = 0;
        foreach (Unit ally in allies)
        {
            ally.setHealth(allyHP[index]);
            index++;
        }
    }

    void updateHealth()
    {
        heroUI.text = heroHP.ToString();
        enemyUI.text = enemyHP.ToString();

        int index = 0;
        foreach (TextMeshProUGUI ally in allyUI)
        {
            ally.text = allyHP[index].ToString();
            index++;
        }
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

    int enemyAttack()
    {
        Unit target = hero;
        int pointer = 0;
        if (allyAttacking[2])
        {
            target = allies[2];
            pointer = 3;
        }
        else if (allyAttacking[1])
        {
            target = allies[1];
            pointer = 2;
        }
        else if (allyAttacking[0])
        {
            target = allies[0];
            pointer = 1;
        }

        bool hit = rollToHit(enemy.attack, target.defence);
        if (hit)
        {
            int damage = rollDamage(enemy.damage, enemy.diceSize);
            
            switch(pointer)
            {
                case 0:
                    heroHP -= damage; break;
                case 1:
                    allyHP[0] -= damage; break;
                case 2:
                    allyHP[1] -= damage; break;
                case 3:
                    allyHP[2] -= damage; break;
                default:
                    heroHP -= damage; break;

            }
            Debug.Log("Enemy Hit! Dealt " + damage + " damage.");
        }
        else
        {
            Debug.Log("Enemy Missed!");
        }

        return pointer;
    }

    void allyAttack(Unit ally)
    {
        bool hit = rollToHit(ally.attack, enemy.defence);
        if (hit)
        {
            int damage = rollDamage(ally.damage, ally.diceSize);
            enemyHP -= damage;
            Debug.Log("Ally Hit! Dealt " + damage + " damage.");
        }
        else
        {
            Debug.Log("Ally Missed!");
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

            int index = 0;

            foreach(Unit ally in allies)
            {
                if (allyAttacking[index])
                {
                    allyAttack(ally);

                    if (enemyHP <= 0)
                    {
                        enemyHP = 0;
                        aWinnerIsYou("Player");
                        setUnitsHealth();
                        updateHealth();
                        return;
                    }
                }
                index++;
            }

            if (enemyAttacking)
            {
                int target = enemyAttack();

                switch (target)
                {
                    case 1:
                        if (allyHP[0] <= 0)
                        {
                            allyHP[0] = 0;
                            allyAttacking[0] = false;
                            allies[0].gameObject.SetActive(false);
                            allyUI[0].gameObject.SetActive(false);
                            setUnitsHealth();
                            updateHealth();
                            return;
                        }
                        break;
                    case 2:
                        if (allyHP[1] <= 0)
                        {
                            allyHP[1] = 0;
                            allyAttacking[1] = false;
                            allies[1].gameObject.SetActive(false);
                            allyUI[1].gameObject.SetActive(false);
                            setUnitsHealth();
                            updateHealth();
                            return;
                        }
                        break;
                    case 3:
                        if (allyHP[2] <= 0)
                        {
                            allyHP[2] = 0;
                            allyAttacking[2] = false;
                            allies[2].gameObject.SetActive(false);
                            allyUI[2].gameObject.SetActive(false);
                            setUnitsHealth();
                            updateHealth();
                            return;
                        }
                        break;

                    case 0:
                    default:
                        if (heroHP <= 0)
                        {
                            heroHP = 0;
                            aWinnerIsYou("Enemy");
                            setUnitsHealth();
                            updateHealth();
                            return;
                        }
                        break;

                }

                

            }

            setUnitsHealth();
            updateHealth();
        }
        
    }
}
