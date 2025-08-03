using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    //Ground Theme
    public List<GroundTheme> themeList = new List<GroundTheme>();

    Dictionary<GroundTheme, List<GameObject>> dicThemePrefab = new Dictionary<GroundTheme, List<GameObject>>();

    public GroundTheme currentGTheme;

    private int amountPrefabOfTheme;

    public ObjectPool pools;

    public ObjectPool currentThemePool;

    private GameObject groundHighest;

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
        pools = GetComponent<ObjectPool>();

        LoadThemeFromResource();

    }

    void LoadThemeFromResource()
    {
        GroundTheme grassTheme = Resources.Load<GroundTheme>("SO_GroundTheme/GrassTheme");
        GroundTheme desertTheme = Resources.Load<GroundTheme>("SO_GroundTheme/DesertTheme");
        GroundTheme roadTheme = Resources.Load<GroundTheme>("SO_GroundTheme/RoadTheme");

        themeList.Add(grassTheme);
        themeList.Add(desertTheme);
        themeList.Add(roadTheme);

        //currentGTheme = themeList[0];

        GenerateThemePrefabDict();


    }

    [ContextMenu("Generate Theme Dictionary")]
    public void GenerateThemePrefabDict()
    {
        dicThemePrefab.Clear();

        foreach (var theme in themeList)
        {
            var list = new List<GameObject>();

            for (int i = 0; i < 10; i++)
            {
                var randomPrefab = theme.groundPrefabs[Random.Range(0, theme.groundPrefabs.Count)];
                pools.objPrefabs = randomPrefab;
                pools.GeneratePool();
                list.Add(randomPrefab);
            }

            dicThemePrefab[theme] = list;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGTheme = themeList[0];

        //currentThemePool.objPrefabs = currentGTheme.groundPrefabs;

        for (int i = 0; i < 5; i++)
        {
            newGround();
        }
    }

    public bool CheckChangeTheme()
    {
        if(amountPrefabOfTheme == 0)
        {
            return true;
        }
        return false;
    }

    public void ChangeTheme()
    {
        int randIndex = Random.Range(3,8);
        amountPrefabOfTheme = randIndex; 

        GroundTheme newTheme;
        do
        {
            newTheme = themeList[Random.Range(0, themeList.Count)];
        }
        while (newTheme == currentGTheme);

        currentGTheme = newTheme;
    }

    public void newGround()
    {
        CheckChangeTheme();

        GameObject newGroundObj = pools.GetObj();
        newGroundObj.transform.SetParent(transform);

        if (groundHighest == null)
        {
            newGroundObj.transform.position = Vector3.zero;
        }
        else
        {
            Ground prevGround = groundHighest.GetComponent<Ground>();
            newGroundObj.transform.position = prevGround.spawnPos.position;
        }

        groundHighest = newGroundObj;

//      Ground newGroundComp = newGroundObj.GetComponent<Ground>();
//      SpawnCoinMachine.instance.SpawnCoinOnGround(newGroundObj, newGroundComp.center);

        amountPrefabOfTheme -= 1;
    }
}
