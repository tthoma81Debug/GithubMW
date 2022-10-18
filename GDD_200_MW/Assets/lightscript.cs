using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lightscript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject thingThatTripped;
    Light2D lightComponent;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Was Tripped");

        if(collision.gameObject.name == "Woodcutter")
        {
            thingThatTripped = GameObject.Find("Candle");
            lightComponent = thingThatTripped.GetComponent<Light2D>();
            lightComponent.intensity += 3.0f;
            Destroy(this.gameObject);
        }
        



    }
}
