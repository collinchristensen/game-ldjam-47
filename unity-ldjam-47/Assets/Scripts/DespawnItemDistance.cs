using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// separate component added to object pooler

public class DespawnItemDistance : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("CheckVisibility");
    }
    private void OnEnable()
    {
        Messenger.AddListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnGameResetState()
    {
        StopAllCoroutines();
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
