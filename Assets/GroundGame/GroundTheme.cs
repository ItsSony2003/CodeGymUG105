using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GroundConfig")]
public class GroundTheme : ScriptableObject
{
    public string themeName;
    public List<GameObject> groundPrefabs;
    public int amount;
}

