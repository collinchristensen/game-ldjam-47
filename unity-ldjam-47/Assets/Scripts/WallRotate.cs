using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotate : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Rotate(0,0,90f * Random.Range(1, 4));
    }
    private void OnDisable()
    {
        transform.localRotation = Quaternion.identity;
    }
}
