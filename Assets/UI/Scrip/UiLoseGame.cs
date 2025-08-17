using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLoseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickReturnMainMenu()
    {
        UIManager.instance.ChangeMap("MainMenu");
    }

    public void OnClickRestart()
    {
        GameManager.instance.ResetGame();
    }
}
