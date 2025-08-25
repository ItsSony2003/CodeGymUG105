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
    }

    public void OnClickQuitGame()
    {
        UIManager.instance.QuitGame();
    }

    public void OnClickSelectCharacter()
    {
        Debug.Log("Dang Phat trien tinh nang");
    }
    public void OnClickOpenSettingUi()
    {
        UIManager.instance.settingUi.SetActive(true);
        
    }
    public void OnClickCloseSettingUi()
    {
        UIManager.instance.settingUi.SetActive(false);
    }
    

}
