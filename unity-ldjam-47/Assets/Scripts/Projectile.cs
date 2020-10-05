using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 MovementDirection { get; set; }

    float movementSpeed = 20f;

    int damage = 1;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += MovementDirection * movementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
