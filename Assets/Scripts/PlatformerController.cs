using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.Windows;

public class PlatformerController : MonoBehaviour
{
    //reference demo repo
    //Create array to store player input
    //Create state machine. design for scale
    //Create movement and jump methods(consider using 3d controller library unity)
    // Player parameters

    public float jumpSpeed = 30;

    public float gravityForce = 80;
    public float lowGrav = 80;
    public float jumpCooldownTime;

    public GameObject rCol;
    public GameObject lCol;
    public Rigidbody rb;
    public Animator animCont;
    public GameObject vis;

    // Integers
    int jTimer;
    int jBufferTimer;
    int coyoteBuffer;

    public LayerMask ground;

    // Player parameters tweaked in design
    public float hInput;
    public float vInput;
    public float moveSpeed = 25f;

    // Vectors
    Vector2 hvec;
    Vector2 vvec;
    public Vector3 gForceVec;
    public Vector3 wallGForce;
    Vector3 jumpVec;


    //booleans
    public bool isColliding;
    bool coyoteSig;
    bool jInput;
    bool jSig;
    public bool wallTouch;
    public bool leftCol;
    public bool rightCol;
    public bool downCol;
    public bool upCol;


    private colDir colDirc;

    // enums
    enum colDir { up, down, left, right };
    enum currentState { Idle, Running, Falling, Jumping, Climbing, Launch, Emotion1, EJump, Emotion2 };
    void Start()
    {

        initPlayerParams();

    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerParams();
        animCont = vis.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        GetInput();
        move();
        coyoteTime();
        jump();
        gravity(gForceVec, wallGForce, -lowGrav);
        wallSlide();
        wallJump();
        faceDir();
    }



    void wallSlide()
    {
        if (rightCol)
        {
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x,-10,0), -1, 0);
            wallTouch = true;
        }
        if (leftCol)
        {
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0, 10), -1, 0);
            wallTouch = true;
        }
        if (!leftCol && !rightCol)
        {
            wallTouch = false;
        }
    }

    void GetInput()
    {
        hInput = Input.GetAxis("Horizontal");
        jInput = Input.GetKey(KeyCode.Space);
    }

    void initPlayerParams()
    {
        leftCol= false;
        rightCol= false;
        upCol= false;
        downCol= false;
    }
    void updatePlayerParams()
    {
        wallGForce = Vector3.zero;
        gForceVec = new Vector3(0, -gravityForce, 0);
    }

    // Method to make player move
    void move()
    {
        if (rightCol)//restrict direction if blocked
        {
            if (hvec.x < 0) { hvec.x = 0; Debug.Log("right move blocked"); }
            rb.velocity = hvec;
        }
        else
        {
            hvec.x = hInput * moveSpeed;
        }
        if (leftCol)//restrict direction if blocked
        {
            if (hvec.x < 0) { hvec.x = 0; Debug.Log("right move blocked"); }
            rb.velocity = hvec;
        }
        else
        {
            hvec.x = hInput * moveSpeed;
        }
        hvec.y = rb.velocity.y;
        rb.velocity = hvec;
    }

    void wallJump()
    {
        if(jInput && rightCol)
        {
            rb.velocity = new Vector3(-10, jumpSpeed, 0);
        }
        if(jSig && rightCol)
        {
            rb.velocity = new Vector3(-10, jumpSpeed, 0);
        }
        if (jInput && leftCol)
        {
            rb.velocity = new Vector3(10, jumpSpeed, 0);
        }
        if (jSig && leftCol)
        {
            rb.velocity = new Vector3(10, jumpSpeed, 0);
        }
    }

    void coyoteTime()
    {
        if(jInput && coyoteSig)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
            animCont.SetTrigger("Jump");
            Debug.Log("Just jumped with COYOTE");
            coyoteBuffer = 0;
            coyoteSig= false;
        }
        if (coyoteSig)
        {
            coyoteBuffer++;
            Debug.Log("Coyote buffer frame count is " + jBufferTimer);
            if (coyoteBuffer == 5)
            {
                coyoteBuffer = 0;
                coyoteSig= false;
            }
        }
    }
    // Jump method
    [ContextMenu("jump")]
    void jump()
    {
        if (jInput && isColliding)//Retain Jump for 5 frames if not touchning ground yet
        {
            if (isColliding)
            {
                Debug.Log("Just jumped");
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
                animCont.SetTrigger("Jump");
                coyoteSig= false;
            }

        }
        else if(jInput && !isColliding)
        {
            Debug.Log("Just stored input");
            jSig = true;
            jBufferTimer = 0;
        }
        if (jSig == true)
        {
            jBufferTimer++;
            Debug.Log("Jump buffer frame count is " + jBufferTimer);
            if (isColliding && downCol)
            {
                jSig= false;
                jBufferTimer = 0;
                Debug.Log("Just jumped with buffer");
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
                animCont.SetTrigger("Jump");

            }
            if (jBufferTimer == 2)
            {
                jSig = false;
                jBufferTimer= 0;
            }

        }
    }

    void leftWallSet(bool isWall)
    {
        leftCol= isWall;
        Debug.Log("Left Message Received");
    }
    void rightWallSet(bool isWall)
    {
        rightCol = isWall;
        Debug.Log("Right Message Received");
    }

    //void OnCollisionStay(Collision collisionInfo)
    //{
    //    isColliding = true;

    //    Vector3 colPos = collisionInfo.transform.position;
    //    if (transform.position.y - colPos.y > 0) //check direction of collision to check if on ground
    //    {
    //        colDirc = colDir.down;
    //        downCol = true;
    //        Debug.Log("Touching the botton");
    //    }
    //    else
    //    {
    //        downCol = false;
    //    }

    //}
    void OnCollisionStay(Collision collision)
    {
        isColliding= true;
      
    }

    private void OnTriggerStay(Collider trigger)
    {

        if(trigger.gameObject.name == "LeftCollider")
        {
            Debug.Log("left side hit");
        }
        if (trigger.gameObject.name == "RightCollider")
        {
            Debug.Log("right side hit");
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        isColliding = false;
        coyoteSig = true;
        Debug.Log("No more coliding" + collisionInfo.gameObject.name);
    }

    // gravity Vector
    void gravity(Vector3 gForce, Vector3 wallForce, float lowGForce)
    {
        if (rb.velocity.y > 0)
        {
            rb.AddForce(new Vector3 (0,lowGForce,0));
        }
        if (rb.velocity.y<= 0)
        {
            rb.AddForce(gForce);
        }
    }

    // Method to know what direction you're moving in
    int moveDirection()
    {
        if (rb.velocity.x > 0)
        {
            return 2;
        }
        else if (rb.velocity.x < 0)
        {
            return 1;
        }
        else
        {
            return 3;
        }
    }
    void faceDir()
    {
        if(moveDirection() == 2)
        {
            // fill later
        }
    }
}


