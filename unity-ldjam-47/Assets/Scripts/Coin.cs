using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreAmount;

    public int healthAmount = 1;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("collision with player: " + gameObject.name);

            // add score
            Messenger.Broadcast<int>(GameActionKeys.playerScored, scoreAmount);

            Messenger.Broadcast<int>(GameActionKeys.playerHealthChanged, healthAmount);
            AudioController.instance.PlayCoinSound();

            //Destroy(gameObject);
            gameObject.DestroyGameObject();

        }

    }
}
