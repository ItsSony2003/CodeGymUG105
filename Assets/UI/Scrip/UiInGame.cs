using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
