using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// separate component added to object pooler

public class DespawnItemDistance : MonoBehaviour
{

    //private bool LevelLoaded = false;


    private void OnEnable()
    {
        Messenger.AddListener(GameActionKeys.gameResetState, OnGameResetState);
        Messenger.AddListener(GameActionKeys.LevelLoaded, OnLevelLoaded);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameActionKeys.gameResetState, OnGameResetState);
        Messenger.RemoveListener(GameActionKeys.LevelLoaded, OnLevelLoaded);

    }

    private void OnGameResetState()
    {
        StopAllCoroutines();
    }

    private void OnLevelLoaded()
    {
        StartCoroutine("CheckVisibility");
    }

    IEnumerator CheckVisibility()
    {
        float sightRadius = Player.Instance.sightRadius;

        Debug.Log("Checking Visibility");

        int i = 0;
        //foreach (GameObject item in ObjectPooler.SharedInstance.pooledObjects)
        //{
        //    if (item.active)
        //    {
        //        Transform playerTransform = Player.Instance.transform;

        //        Debug.Log("Player position = " + playerTransform.position);

        //        if (Vector3.Distance(playerTransform.position, item.transform.position) > sightRadius)
        //        {
        //            item.SetActive(false);
        //        }
        //        else
        //        {
        //            item.SetActive(true);
        //        }
        //    }
        //    i++;
        //}
        //Debug.Log("Checked " + i + " objects");

        yield return new WaitForSeconds(1);

        StartCoroutine("CheckVisibility");
    }
}
