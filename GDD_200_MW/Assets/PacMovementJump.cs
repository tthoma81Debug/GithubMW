using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovementJump : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 MoveRight;
    Vector3 MoveLeft;
    Vector3 MoveUp;
    Vector3 MoveDown;
    Vector2 jumpForce;
    Vector2 poundForce;

    Rigidbody2D pacmanPhysics;
    Animator characterAnimator;
    public GameObject theProjectileHolder;
    private GameObject currentProjectile;
    public Transform spawnSpot;

    int speed;
    bool canJump = false;
    float fallingForce;


    void Start()
    {
        Debug.Log("Start function called");
        spawnSpot = GameObject.Find("SpawnPoint").transform;

        //for snap movement
        /*
        MoveRight = new Vector3(5, 0, 0);
        MoveLeft = new Vector3(-5, 0, 0);
        MoveUp = new Vector3(0, 5, 0);
        MoveDown = new Vector3(0, -5, 0);
        */

        MoveRight = new Vector3(1, 0, 0);
        MoveLeft = new Vector3(-1, 0, 0);
        MoveUp = new Vector3(0, 1, 0);
        MoveDown = new Vector3(0, -1, 0);


        speed = 3;
        pacmanPhysics = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        jumpForce = new Vector2(0, 25);
        poundForce = new Vector2(0, -25);
        fallingForce = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Update function called");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("User is pressing space");
        }



        //movement controls (snap movement)
        /*
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Move right");
            transform.Translate(MoveRight);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Move left");
            transform.Translate(MoveLeft);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Move up");
            transform.Translate(MoveUp);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Move down");
            transform.Translate(MoveDown);
        }

    */
        //movement continues 

        /*
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Move right");
            transform.Translate(MoveRight * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Move left");
            transform.Translate(MoveLeft * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Move up");
            transform.Translate(MoveUp * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Move down");
            transform.Translate(MoveDown * Time.deltaTime * speed);
        }

    */


        /*
        //physics based jump
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Move right");
            transform.Translate(MoveRight * Time.deltaTime * speed);
           // pacmanPhysics.velocity = new Vector2(2, 1);
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           // Debug.Log("Move left");
            transform.Translate(MoveLeft * Time.deltaTime * speed);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(canJump == true)
            {
                pacmanPhysics.AddForce(jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
            else
            {
                Debug.Log("Why...you little double jumper. You are busted");
            }
           // Debug.Log("jump");
            //transform.Translate(MoveUp * Time.deltaTime * speed);
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
           // Debug.Log("Move down");
            transform.Translate(MoveDown * Time.deltaTime * speed);
        }
        */

        //physics based jump and movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Move right");
            //transform.Translate(MoveRight * Time.deltaTime * speed);
            fallingForce = pacmanPhysics.velocity.y;
            pacmanPhysics.velocity = new Vector2(10, fallingForce);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Debug.Log("Move left");
            //transform.Translate(MoveLeft * Time.deltaTime * speed);
            fallingForce = pacmanPhysics.velocity.y;
            pacmanPhysics.velocity = new Vector2(-10, fallingForce);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump == true)
            {
                pacmanPhysics.AddForce(jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
            else
            {
                Debug.Log("Why...you little double jumper. You are busted");
            }
            // Debug.Log("jump");
            //transform.Translate(MoveUp * Time.deltaTime * speed);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // Debug.Log("Move down");
            //transform.Translate(MoveDown * Time.deltaTime * speed);
            pacmanPhysics.AddForce(poundForce, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            //melee attack
            characterAnimator.Play("meleeState");

        }
        else if(Input.GetKey(KeyCode.B))
        {
            //fire projectile
            currentProjectile = Instantiate(theProjectileHolder, spawnSpot.position, spawnSpot.rotation);
            Rigidbody2D projectilePhysics = currentProjectile.GetComponent<Rigidbody2D>();
            projectilePhysics.AddForce(new Vector2(15, 0), ForceMode2D.Impulse);
        }



    }
    

    /*
    //gets called whenever object runs into something
    public void OnCollisionEnter2D(Collision2D thingPacmanHit)
    {
        if(thingPacmanHit.gameObject.name == "zombie")
        {
            Debug.Log("Pacman ran into something.... it is " + thingPacmanHit.gameObject.name);
            Destroy(thingPacmanHit.gameObject);
        }
       

    }
    */

    //gets called whenever object runs into something
    //part of fixed jump
    public void OnCollisionEnter2D(Collision2D thingPacmanHit)
    {
        if (thingPacmanHit.gameObject.CompareTag("ground"))
        {
            Debug.Log("Pacman ran into something.... it is " + thingPacmanHit.gameObject.name);
            canJump = true;
        }
        else if(thingPacmanHit.gameObject.CompareTag("pitFloor"))
        {
            Debug.Log("Pacman was killed by the guardians...");
        }


    }

}
