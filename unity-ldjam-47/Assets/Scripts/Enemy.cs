using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    public float sightRadius = 4f;

    public int health = 10;

    public float moveSpeed = 1.1f;

    public int damageAmount = -4;

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
            //Debug.Log("collision with player: " + gameObject.name);

            Messenger.Broadcast<int>(GameActionKeys.playerHealthChanged, damageAmount);

            Destroy(gameObject);

        }
        //else if (other.tag == "projectile" || other.name.Contains("projectile"))
        //{
        //    Debug.Log("enemy collided with projectile");
        //    health--;
        //    if (health <= 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //    Destroy(other);
        //}

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
            //Debug.Log("Player position = " + target.position.x);

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= sightRadius)
            {
                Vector3 faceDir = target.position - transform.position;
                faceDir.z = transform.position.z;

                transform.position += faceDir * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
