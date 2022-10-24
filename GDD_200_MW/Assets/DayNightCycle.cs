using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    private float amountOfRotation = 1f;
    private Light2D theGlobalLight;
    private GameObject lightObject;
    private GameObject moonObject;
    bool lowerLight = false;
    void Start()
    {
        lightObject = GameObject.Find("Global Light 2D");
        moonObject = GameObject.Find("Moon");
        theGlobalLight = lightObject.GetComponent<Light2D>();
        StartCoroutine(dayNightCycle());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator dayNightCycle()
    {
        while(true) //note. This is a coroutine, not  the main thread.
        {
            yield return new WaitForSecondsRealtime(0.1f);
            Debug.Log("Rotating");
            
            //rotate here
            Vector3 currentRotation = this.transform.rotation.eulerAngles;
            Vector3 nextRotation = new Vector3(0, 0, (currentRotation.z + amountOfRotation));

            Debug.Log("Next rotation is " + nextRotation.z);

            Quaternion theRotation = Quaternion.Euler(nextRotation);

            this.transform.rotation = theRotation;

            //change light here
            checkDayNight();
            
        }
    }

    private void checkDayNight()
    {
        Vector2 Point_1 = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        Vector2 moonPosition = moonObject.transform.TransformPoint(moonObject.transform.position);
        Vector2 Point_2 = new Vector2(moonPosition.x, moonPosition.y);
        float angle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        Debug.Log("angle is " + angle);

        if(angle >= 120) //this needs to be tested. does not match. adjust as needed
        {
            lowerLight = false;
        }
        else
        {
            lowerLight = true;
        }

        if(lowerLight == false)
        {
            theGlobalLight.intensity += .01f;
        }
        else
        {
            theGlobalLight.intensity -= 0.1f;
        }

        
    }
}
