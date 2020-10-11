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

    public GameObject levelChunkDetectorPrefab;

    public Transform levelsTransform;

    private Vector3 spawnPointVector;

    public GameObject SpawnObject(GameObject go, int x, int y)
    {
        return SpawnObject(go, levelsTransform, x, y);
    }

    public GameObject SpawnObject(GameObject go, Transform levelsTransform, int x, int y)
    {

        GameObject temp;

        Vector3 pos = new Vector3(x, y);

        if (GameGlobals.ObjectsArePooled)
        {

            ////Debug.Log("CREATING GAME OBJECT:" + go.name);
            //temp = go.CreateGameObject(pos, Quaternion.identity, true);
            ////Debug.Log("CREATED GAME OBJECT:" + temp.name);
            //temp.transform.SetParent(levelsTransform);
            
            temp = go.CreateGameObject(pos, Quaternion.identity, true);
            temp.transform.SetParent(levelsTransform);
        }
        else
        {
            temp = Instantiate(go, pos, Quaternion.identity, levelsTransform);
        }


        return temp;
    }

    //public GameObject MoveObject(GameObject go, int x, int y)
    //{
    //    Vector3 pos = new Vector3(x, y);
    //    go.transform.parent = levelsTransform;
    //    //go.transform.position = pos;

    //    return go;
    //}

    public void RotateLevel()
    {
        levelsTransform.parent.Rotate(90, 0, 0);
    }
}
