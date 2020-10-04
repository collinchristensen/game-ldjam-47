using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetHolder : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> floors;

    public List<GameObject> walls;

    public GameObject door;

    public List<GameObject> enemies;

    public GameObject portal;
    public GameObject treasure;
    public GameObject coin;
    public GameObject ladder;


    public Transform levelsTransform;

    public void SpawnObject(GameObject go, int x, int y)
    {
        SpawnObject(go, levelsTransform, x, y);
    }

    public void SpawnObject(GameObject go, Transform levelsTransform, int x, int y)
    {
        Vector3 pos = new Vector3(x, y);
        Instantiate(go, pos, Quaternion.identity, levelsTransform);
    }

    public void RotateLevel()
    {
        levelsTransform.parent.Rotate(90, 0, 0);
    }
}
