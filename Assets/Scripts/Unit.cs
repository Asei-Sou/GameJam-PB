using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isEnemy;

    public int maxHealth;
    private int health;

    public int attack;
    public int damage;
    public int defence;
    public int diceSize;

    public Combat combat;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            isEnemy = false;
        }
        else
        {
            isEnemy = true;
        }

        health = maxHealth;
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }
}
