using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision: " + gameObject.name);
        if (other.tag == "projectile" || other.name.Contains("projectile"))
        {
            transform.parent.GetComponent<Enemy>().TakeDamage(1);
            Destroy(other);
            Destroy(gameObject);

        }
    }
}
