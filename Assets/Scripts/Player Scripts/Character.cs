using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpGravityForce;
    Rigidbody rb;
    // Gravity (vector input)
    // Move (vector input)
    // Buffer Array??
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // freeze z location and rotations
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // gForce is the amount of normal gravity when falling, low g force is used during jumps and upward moving characters for game feel. gON is the condition to have gravity work or not
    void gravity(Vector3 gForce, float lowGForce, bool gON)
    {
        if (gON == true)
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(new Vector3(0, lowGForce, 0));
            }
            if (rb.velocity.y <= 0)
            {
                rb.AddForce(gForce);
            }
        }
        else
        {
            Debug.Log("gravity_suspended");
        }
    }

    void horizontalmove(Vector2 horizontalInput)
    {
        //input vector 2 control vector from child update
    }

    void verticalmove(Vector2 verticalInput)
    {

    }

    void die()
    {
        Destroy(this);
    }

    void stopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}
