using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Player player;

    //ui child
    public GameObject mainMenuUi;
    public GameObject inGameUi;
    public GameObject loseGameUi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    public void TimeGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeMap(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void SetScoreInGame(int numScore)
    {
        UiInGame UiInGameComp = inGameUi.GetComponent<UiInGame>();
        UiInGameComp.SetScore(numScore);
    }


    //UI skill
    public void SlotSkill_1()
    {
        player.skillManager.UseSkill(0);
    }

    public void SlotSkill_2()
    {
        player.skillManager.UseSkill(1);
    }

    public void SlotSkill_3()
    {
        player.skillManager.UseSkill(2);
    }

    public void SlotSkill_4()
    {
        player.skillManager.UseSkill(3);
    }
}
