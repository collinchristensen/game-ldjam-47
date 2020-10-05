using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float sightRadius = 5.5f;

    public int health;

    public Transform target;

    //private void OnEnable()
    //{
    //    Messenger.AddListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);
    //}

    //private void OnDisable()
    //{
    //    Messenger.RemoveListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);
    //}

    //void OnPlayerSpawned()
    //{
    //    target = Player.Instance.transform;
    //}

    private void Start()
    {
        target = Player.Instance.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("collision with player: " + gameObject.name);

            // add damageg
            //Messenger.Broadcast<int>(GameActionKeys.playerDamaged, scoreAmount);

            Destroy(gameObject);

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    private void Update()
    {
        if (target != null)
        {
            Debug.Log("Player position = " + target.position.x);
        }
    }
}
