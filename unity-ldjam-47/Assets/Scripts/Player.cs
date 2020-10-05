using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;

    public float movementSpeed = 10f;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    bool ready = false;

    public static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // singleton check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);


        // character controller check
        controller = GetComponent<CharacterController>();

        if (controller != null)
        {
            ready = true;

            //controller.detectCollisions = false;
        }
    }

    void Update()
    {
        if (ready)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");
        }

    }
    void FixedUpdate()
    {
        if (ready)
        {
            Vector3 move = new Vector3(horizontalMovement, verticalMovement);
            controller.Move(move * Time.deltaTime * movementSpeed);
        }


    }
}
