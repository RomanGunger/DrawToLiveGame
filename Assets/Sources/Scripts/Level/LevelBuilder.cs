using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEditor;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] List<GameObject> levelBlocksGameObjects;
    [SerializeField] int blocksCount = 30;

    List<GameObject> listLevelBlocks = new List<GameObject>();

    public void Build()
    {
#if UNITY_EDITOR

        var rnd = new System.Random();

        foreach (var obj in GetComponentsInChildren<LevelBlock>())
        {
            if (listLevelBlocks.Contains(obj.gameObject))
                listLevelBlocks.Remove(obj.gameObject);

            DestroyImmediate(obj.gameObject);
        }

        for (int i = 0; i < blocksCount; i++)
        {
            GameObject obj = PrefabUtility.InstantiatePrefab(levelBlocksGameObjects[rnd.Next(0, levelBlocksGameObjects.Count)], transform) as GameObject;

            var block = obj.GetComponent<LevelBlock>();
            float positionZ = block.boxCollider.size.z * i;
            Debug.Log(positionZ);

            obj.transform.rotation = Quaternion.identity;
            obj.transform.localPosition = new Vector3(0, 0, positionZ);

            listLevelBlocks.Add(obj);
        }

#endif

    }
}
