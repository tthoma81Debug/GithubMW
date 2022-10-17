using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    // Start is called before the first frame update

    ZombieScript theZombieScript;
    GameObject theZombie;
    private AudioSource zombieAudio;
    void Start()
    {
        theZombie = GameObject.FindGameObjectWithTag("zombie");
        Debug.Log("We found a game object with a name of " + theZombie.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("zombie"))
        {
            //Destroy(collision.gameObject);

            //collision.gameObject.ZombieScript.takeDamage(); //want to do this. but unity has a different way

            //this way works:
            theZombieScript = collision.gameObject.GetComponent<ZombieScript>();
            zombieAudio = collision.gameObject.GetComponent<AudioSource>();
            zombieAudio.Play();
            theZombieScript.takeDamage();
            theZombieScript.someRandomVariableHereCanAccessCool = false;

        }
    }
}
