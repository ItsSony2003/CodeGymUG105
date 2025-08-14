using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickPauseGame()
    {
        UIManager.instance.TimeGame(true);
    }

    public void OnClickResume()
    {
        UIManager.instance.TimeGame(false);
    }

    public void OnClickReturnMainMenu()
    {
        UIManager.instance.ChangeMap("MainMenu");
    }

    public void SetScore(int num)
    {
        score.text = num.ToString();
    }
}
