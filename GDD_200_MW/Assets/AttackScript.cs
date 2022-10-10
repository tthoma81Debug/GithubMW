using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.Play("melee");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Trigger" + collision.gameObject.name);
        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit " + collision.gameObject.name);
    }
}
