using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreAmount;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("collision with player: " + gameObject.name);

            // add score
            Messenger.Broadcast<int>(GameActionKeys.playerScored, scoreAmount);

            Destroy(gameObject);

        }

    }
}
