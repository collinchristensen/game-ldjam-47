using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;

    public float movementSpeed = 5f;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    bool ready = false;

    private bool allowFire;
    public GameObject projectile;
    private float fireRate = 0.1f;

    Vector3 move;


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

        // instantiate
        move = Vector3.zero;
        allowFire = true;
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
            move = new Vector3(horizontalMovement, verticalMovement);
            controller.Move(move * Time.deltaTime * movementSpeed);
            if (allowFire)
            {
                StartCoroutine("Fire");
            }
        }


    }

    IEnumerator Fire()
    {
        Debug.Log("PROJECTILE");
        allowFire = false;

        GameObject temp = Instantiate(projectile, transform.position, transform.rotation);
        temp.GetComponent<Projectile>().MovementDirection = move;

        yield return new WaitForSeconds(fireRate);

        allowFire = true;
    }
}
