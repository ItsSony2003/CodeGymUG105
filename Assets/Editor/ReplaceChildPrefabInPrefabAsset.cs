using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ReplaceChildPrefabInPrefabAsset : EditorWindow
{
    GameObject containerPrefab;     // Prefab cha (có chứa nhiều con)
    GameObject currentPrefab;       // Prefab con cần thay thế
    GameObject targetPrefab;        // Prefab thay thế

    [MenuItem("Tools/Replace Child Prefab In Prefab Asset")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceChildPrefabInPrefabAsset>("Replace Child Prefab");
    }

    void OnGUI()
    {
        GUILayout.Label("Chọn Prefabs", EditorStyles.boldLabel);

        containerPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab Container", containerPrefab, typeof(GameObject), false);
        currentPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab cần thay", currentPrefab, typeof(GameObject), false);
        targetPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab thay thế", targetPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Thay tất cả"))
        {
            if (containerPrefab == null || currentPrefab == null || targetPrefab == null)
            {
                Debug.LogError("Vui lòng gán đủ 3 Prefab.");
                return;
            }

            ReplacePrefabsInAsset();
        }
    }

    void ReplacePrefabsInAsset()
    {
        string prefabPath = AssetDatabase.GetAssetPath(containerPrefab);
        GameObject prefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);

        int replaced = 0;

        var children = prefabRoot.GetComponentsInChildren<Transform>(true);
        var toReplace = new List<(Transform child, Transform parent, int index, Vector3 pos, Quaternion rot, Vector3 scale)>();

        foreach (Transform child in children)
        {
            if (PrefabUtility.GetPrefabInstanceStatus(child.gameObject) == PrefabInstanceStatus.Connected)
            {
                GameObject source = PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject);
                if (source == currentPrefab)
                {
                    var parent = child.parent;
                    int index = child.GetSiblingIndex();

                    // Lưu lại thông tin trước khi destroy
                    toReplace.Add((child, parent, index, child.position, child.rotation, child.localScale));
                }
            }
        }

        foreach (var (child, parent, index, pos, rot, scale) in toReplace)
        {
            // Destroy old object
            Object.DestroyImmediate(child.gameObject);

            // Instantiate new prefab
            GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(targetPrefab, prefabRoot.scene);
            newObj.transform.SetParent(parent);
            newObj.transform.SetSiblingIndex(index);
            newObj.transform.position = pos;
            newObj.transform.rotation = rot;
            newObj.transform.localScale = scale;

            replaced++;
        }

        // Save and unload
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabPath);
        PrefabUtility.UnloadPrefabContents(prefabRoot);

        Debug.Log($"Đã thay {replaced} instance của prefab.");
    }
}
