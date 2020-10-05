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

    public GameObject coin;
    public GameObject treasure;
    public GameObject goldBlock;

    //public GameObject ladder;

    public Transform levelsTransform;

    public GameObject SpawnObject(GameObject go, int x, int y)
    {
        return SpawnObject(go, levelsTransform, x, y);
    }

    public GameObject SpawnObject(GameObject go, Transform levelsTransform, int x, int y)
    {

        GameObject temp;

        Vector3 pos = new Vector3(x, y);
        temp = Instantiate(go, pos, Quaternion.identity, levelsTransform);

        return temp;
    }

    public void RotateLevel()
    {
        levelsTransform.parent.Rotate(90, 0, 0);
    }
}
