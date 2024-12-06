using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Collider platformCollider;
    [SerializeField] Transform platformTransform;
    [SerializeField] Transform[] targetPoints;
    [SerializeField] Rigidbody platformRb;

    public float moveRange;
    public bool isVertical;
    public float speed;
    public int targetPointsRef;



    Vector3 startPos;
    Vector3 moveDir;


    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        platformTransform = transform.Find("Platform").GetComponent<Transform>();
        platformRb = transform.Find("Platform").GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        
    }

    // Update is called once per frame
    void Update()
    {
        updateDirection();
    }
    
    void updateDirection()
    {
        if (isVertical)
        {
            moveDir =  transform.up;
        }
        else
        {
            moveDir = transform.right;
        }
    }

    private void FixedUpdate()
    {
        moveBlock();
        drawRayBounds();
    }

    void moveBlock()
    {
        // if in bounds move change direction
        // Compute directionand choose speed


        if (isVertical)
        {

            if (((platformTransform.position.y - startPos.y) < moveRange) && platformRb.linearVelocity.y < 0)
            {
                platformRb.linearVelocity = moveDir* speed;
                Debug.Log("moving up");

            }
            else
            {
                platformRb.linearVelocity = moveDir* -speed;
                Debug.Log("moving down");
            }
        }

    }

    void drawRayBounds()
    {
        if(isVertical)
        {
            Debug.DrawRay(transform.position, moveDir * moveRange, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, moveDir * moveRange, Color.yellow);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
