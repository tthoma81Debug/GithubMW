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
    private GameObject meleeSoundObject;
    private AudioSource meleeSound;
    public Transform spawnSpot;
    private Rigidbody2D zombiePhysics;

    int speed;
    bool canJump = false;
    float fallingForce;


    void Start()
    {
        spawnSpot = GameObject.Find("SpawnPoint").transform;
        meleeSoundObject = GameObject.Find("MeleeSound");
        meleeSound = meleeSoundObject.GetComponent<AudioSource>();
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
        jumpForce = new Vector2(0, 5);
        poundForce = new Vector2(0, -25);
        fallingForce = 0f;
        Debug.Log("Reached end of start method");
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


       // if(Input.GetAxis("Horizontal") != 0)
       // {
            Debug.Log("horizontal input is " + Input.GetAxis("Horizontal"));


        float speed = 20f;
        float movement = Input.GetAxis("Horizontal") * speed;
        Vector2 newSpeed = new Vector2(movement, pacmanPhysics.velocity.y);
        pacmanPhysics.velocity = newSpeed;

       // }

        //physics based jump and movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Move right");
            //transform.Translate(MoveRight * Time.deltaTime * speed);
            fallingForce = pacmanPhysics.velocity.y;
            pacmanPhysics.velocity = new Vector2(10, fallingForce);
            characterAnimator.SetBool("isRunning", true);

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

             meleeSound.Play();
             characterAnimator.SetBool("meleeAttack", true);
            //melee attack
            //characterAnimator.Play("meleeState"); //commented out to show parameters

        }
        else if(Input.GetKey(KeyCode.B))
        {
            //fire projectile
            currentProjectile = Instantiate(theProjectileHolder, spawnSpot.position, spawnSpot.rotation);
            Rigidbody2D projectilePhysics = currentProjectile.GetComponent<Rigidbody2D>();
            projectilePhysics.AddForce(new Vector2(15, 0), ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            //define a "layer mask" so rays only affect certain things
            int theLayerMask = LayerMask.GetMask("Enemy");
            //cast a ray
            //also look at raycast all for array
            Vector2 rayOrigin = new Vector2(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y);
            Vector2 rayRightLength = new Vector2(100, 0);

            

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, Mathf.Infinity, theLayerMask);
            Debug.DrawRay(rayOrigin, rayRightLength, Color.green, 5);

            if (hit == false)
            {
                Debug.Log("ray fired. Didn't hit anything");
            }
            else
            {
                Debug.Log("Ray hit something. It hit " + hit.transform.gameObject.name);

                if(hit.transform.gameObject.name == "zombie")
                {
                    Debug.Log("Which is an enemy. Grappling");
                    //add force here

                    Vector2 distance = hit.transform.position - this.transform.position;

                    //hardcoded force. usually replace with dynamic distance
                    //Vector2 grappleForce = new Vector2(-5, 3);

                    //force based on distance
                    float distanceForce = (distance.x * -1) * 0.75f;
                    Vector2 grappleForce = new Vector2(distanceForce, distance.y + 2);

                    zombiePhysics = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                    zombiePhysics.AddForce(grappleForce, ForceMode2D.Impulse);

                    
                    Debug.Log("Initial Distance is " + distance);

                }
            }
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
