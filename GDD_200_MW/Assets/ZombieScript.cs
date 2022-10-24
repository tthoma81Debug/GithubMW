using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 4;

    public bool someRandomVariableHereCanAccessCool = true;
    public bool invincible = false;
    private IFrameScript theIFrameScript;
    void Start()
    {
        theIFrameScript = GameObject.Find("IFrameLogic").GetComponent<IFrameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {

        if (theIFrameScript.invincible == false)
        {


            Debug.Log("Health pre hit is " + health);
            health--;



            if (health <= 0)
            {
                Destroy(this.gameObject);
                //change level
                GameObject Woodcutter = GameObject.Find("Woodcutter");
                DontDestroyOnLoad(Woodcutter);
                SceneManager.LoadScene("Melee1MW");
            }

            Debug.Log("Health post hit is " + health);

        }
        else
        {
            //Debug.Log("Invincible. Hit but no damage :)");
            health--;
        }
    }
}
