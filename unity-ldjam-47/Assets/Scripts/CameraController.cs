using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;

    public float movementSpeed = 5f;


    private void Update()
    {
        //Vector3 newPosition = transform.position;
        //newPosition.x = Mathf.Lerp(newPosition.x, targetTransform.position.x, movementSpeed * Time.deltaTime);
        //transform.localPosition = newPosition;
    }
}
