using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score;
    public int record { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        UIManager.instance.loseGameUi.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        UIManager.instance.loseGameUi.SetActive(true);
        SetRecord();
    }

    public void SetRecord()
    {
        if(score > record)
        {
            record = score;
        }
    }

    public void AddCoin()
    {
        score += 1;
        UIManager.instance.SetScoreInGame(score);
    }
}
