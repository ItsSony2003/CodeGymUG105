using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        UIManager.instance.ChangeMap("Play");
        Time.timeScale = 1f;
    }

    public void OnClickQuitGame()
    {
        UIManager.instance.QuitGame();
    }

    public void OnClickSelectCharacter()
    {
        Debug.Log("Dang Phat trien tinh nang");
    }
}
