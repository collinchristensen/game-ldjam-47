using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public float forwardAcceleration = 100f;
    public float forwardMaxSpeed = 200f;

    public float turnSpeed = 50f;
    public float brakeSpeed = 200f;

    public float hoverHeight = 3f;    
    public float heightSmoothRate = 10f;  
    public float rotationSmoothRate = 5f; 

    private Vector3 previousUpVector;

    public float yaw;
    private float ySmoothRate;
    private float currentSpeed;


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += (currentSpeed >= forwardMaxSpeed) ? 0f : forwardAcceleration * Time.deltaTime;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        yaw += turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");

        previousUpVector = transform.up;

        transform.rotation = Quaternion.Euler(0, yaw, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -previousUpVector, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);

            Vector3 targetUpVector = Vector3.Lerp(previousUpVector, hit.normal, Time.deltaTime * rotationSmoothRate);

            Quaternion tilt = Quaternion.FromToRotation(transform.up, targetUpVector);

            transform.rotation = tilt * transform.rotation;

            ySmoothRate = Mathf.Lerp(ySmoothRate, hoverHeight - hit.distance, Time.deltaTime * heightSmoothRate);
            transform.localPosition += previousUpVector * ySmoothRate;
        }

        transform.position += transform.forward * (currentSpeed * Time.deltaTime);
    }
}