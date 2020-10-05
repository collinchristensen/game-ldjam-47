using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    public float sightRadius = 4f;

    public int health;

    public float moveSpeed = 2f;

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

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= sightRadius)
            {
                Vector3 faceDir = target.position - transform.position;
                faceDir.z = transform.position.z;

                transform.position += faceDir * moveSpeed * Time.deltaTime;
            }
        }
    }
}
