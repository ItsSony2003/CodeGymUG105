using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    //Ground Theme
    public List<GroundTheme> listGTheme = new List<GroundTheme>();

    public GroundTheme currentGTheme;

    private int amountPrefabOfTheme;

    public static GroundManager Instance;

    public ObjectPool groundPool;

    private GameObject groundHeihest;

    public int limitRecenter = 100;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Awake()
    {
        LoadThemeFromResource();
    }

    void LoadThemeFromResource()
    {
        GroundTheme grassTheme = Resources.Load<GroundTheme>("SO_GroundTheme/GrassTheme");
        GroundTheme desertTheme = Resources.Load<GroundTheme>("SO_GroundTheme/DesertTheme");
        GroundTheme roadTheme = Resources.Load<GroundTheme>("SO_GroundTheme/RoadTheme");

        listGTheme.Add(grassTheme);
        listGTheme.Add(desertTheme);
        listGTheme.Add(roadTheme);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGTheme = listGTheme[0];
        
        groundPool.objPrefabs = currentGTheme.groundPrefabs;

        for (int i = 0; i < 5; i++)
        {
            newGround();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckChangeTheme()
    {
        if(amountPrefabOfTheme == 0)
        {
            ChangeTheme();
        }
    }

    public void ChangeTheme()
    {
        int randIndex = Random.Range(4, 7);
        amountPrefabOfTheme = randIndex;

        GroundTheme newTheme;
        do
        {
            newTheme = listGTheme[Random.Range(0, listGTheme.Count)];
        }
        while (newTheme == currentGTheme);
        currentGTheme = newTheme;
        if (groundPool != null)
        {
            groundPool.objPrefabs = currentGTheme.groundPrefabs;
            groundPool.ResetPool();
        }
        Debug.Log(currentGTheme);

    }

    public void newGround()
    {
        CheckChangeTheme();

        if (groundHeihest == null)
        {
            groundHeihest = groundPool.GetObj();
            groundHeihest.transform.position = Vector3.zero;
        }
        else
        {
            Ground groundComp = groundHeihest.GetComponent<Ground>();
            groundHeihest = groundPool.GetObj();
            groundHeihest.transform.position = groundComp.spawnPos.transform.position;
        }

        amountPrefabOfTheme -= 1;
        
    }



    public void RecenterMap()
    {
        Player playerController = Player.instance;

        Vector3 offset = playerController.transform.position;

        playerController.transform.position -= offset;

        foreach (GameObject tile in groundPool.GetUsingList())
        {
            tile.transform.position -= offset;
        }

        foreach (GameObject tile in groundPool.GetListQueue())
        {
            tile.transform.position -= offset;
        }
    }
}
