using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conversation : MonoBehaviour
{
    public string playerSpeech;
    public string enemySpeech;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI enemyText;

    // Start is called before the first frame update
    void Start()
    {
        updatePlayerSpeech();
        updateEnemySpeech();
    }

    public void updatePlayerSpeech()
    {
        playerSpeech = playerText.text;
    }

    public void updateEnemySpeech()
    {
        enemySpeech = enemyText.text;
    }

    void setEnemySpeech()
    {
        enemyText.text = enemySpeech;
    }
}
