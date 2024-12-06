using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] public float gravityForce;
    [SerializeField] public float jumpGravityForce;
    Rigidbody rb;

    public Vector3 gForceVec= Vector3.zero;

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
        updateParams();
    }


    // gForce is the amount of normal gravity when falling, low g force is used during jumps and upward moving characters for game feel. gON is the condition to have gravity work or not
    public void gravity(Vector3 gForce, Vector3 wallForce, float lowGForce, bool gON)
    {
        if (gON == true)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(new Vector3(0, lowGForce, 0));
            }
            if (rb.linearVelocity.y <= 0)
            {
                rb.AddForce(gForce);
            }
            Debug.Log("Gravity Added");
        }
        else if (gON == false)
        {
            Debug.Log(" false gravity Block");
        }
        else
        {
            Debug.Log("gravity_suspended");
        }
    }

    public void horizontalmove(Vector2 horizontalInput)
    {
        //input vector 2 control vector from child update

    }

    void updateParams()
    {

        gForceVec = new Vector3(0, -gravityForce, 0);
    }


    public void die()
    {
        Destroy(this);
    }

    public void stopMoving()
    {
        rb.linearVelocity = Vector3.zero;
    }


}
