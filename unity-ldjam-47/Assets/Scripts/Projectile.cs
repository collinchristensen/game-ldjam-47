using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 MovementDirection { get; set; }

    float movementSpeed = 20f;

    int damage = 1;

    private void OnEnable()
    {
        //Destroy(gameObject, 2f);
        //gameObject.DestroyDelayed(2f);
        StartCoroutine("WaitForDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += MovementDirection * movementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("projectile collided with" + other.name);

        if (other.tag == "enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            //Destroy(gameObject);
            gameObject.DestroyGameObject();
        }

    }

    IEnumerator WaitForDestroy()
    {
        Debug.Log("projectile coroutine");
        yield return new WaitForSeconds(2);
        gameObject.DestroyGameObject();
        Debug.Log("projectile destroy");
    }
}
