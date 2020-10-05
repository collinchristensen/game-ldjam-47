using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    //public Vector3 offset;

    public float movementSpeed = 5f;

    private void Start()
    {
        target = Player.Instance.transform;
    }

    // late update for camera
    //private void Update()
    //{
    //    //Vector3 newPosition = transform.position;
    //    //newPosition.x = Mathf.Lerp(newPosition.x, targetTransform.position.x, movementSpeed * Time.deltaTime);
    //    //transform.localPosition = newPosition;
    //}

    private void LateUpdate()
    {
        if (target != null)
        {
            //transform.position = target.position + offset;

            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(newPosition.x, target.position.x, movementSpeed * Time.deltaTime);
            transform.localPosition = newPosition;
        }
    }
}
