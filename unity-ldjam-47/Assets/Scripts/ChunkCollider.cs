using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCollider : MonoBehaviour
{
    public Chunk parentChunk { get; set; }

    private bool levelLoaded = false;

    private Transform playerTransform;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name.Contains("player"))
    //    {
    //        Debug.Log("<color=blue>collided with player at position" + transform.position + "</color>");
    //        SetChunkActive();
    //    }
    //    else
    //    {
    //        SetChunkInactive();
    //    }
    //}

    private void ShowChunk()
    {
        foreach (List<GameObject> tileRow in parentChunk.Tiles)
        {
            foreach (GameObject tile in tileRow)
            {
                tile.Show();
            }
        }
    }

    private void HideChunk()
    {
        foreach (List<GameObject> tileRow in parentChunk.Tiles)
        {
            foreach (GameObject tile in tileRow)
            {
                tile.Hide();
            }
        }
    }

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
        //if (levelLoaded)
        //{
        //    StopAllCoroutines();
        //    levelLoaded = false;
        //}
    }

    private void OnLevelLoaded()
    {
        levelLoaded = true;
        StartCoroutine("CheckVisibility");
    }

    IEnumerator CheckVisibility()
    {
        float sightRadius = Player.Instance.sightRadius;

        // Debug.Log("Checking Visibility");

        playerTransform = Player.Instance.transform;

        // Debug.Log("Player position = " + playerTransform.position);

        if (Vector3.Distance(playerTransform.position, transform.position) > sightRadius)
        {
            HideChunk();
        }
        else
        {
            ShowChunk();
        }

        float delay = UnityEngine.Random.Range(1f, 1.9f);
        // Debug.Log("Delay = " + delay);

        yield return new WaitForSeconds(delay);

        StartCoroutine("CheckVisibility");
    }
}
