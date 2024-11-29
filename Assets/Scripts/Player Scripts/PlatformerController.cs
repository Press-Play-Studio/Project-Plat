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
    public float airMoveSpeed;
    public float gravityForce = 80;
    public float lowGrav = 80;
    public float jumpCooldownTime;
    public float wallSlideSpeed;
    public float wallJumpSpeed;



    //Jumpable ground check
    RaycastHit gHit;


    // child components
    public GameObject rCol;
    public GameObject lCol;
    public Rigidbody rb;
    public Animator animCont;
    public GameObject vis;

    // Integers
    int jTimer;
    int jBufferTimer;
    int coyoteBuffer;
    int wallJbuffer; //restricts sideways movement for a few frames after wall jump
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
    Vector3 wallSlideVel;
    Vector3 defaultPos;


    //booleans
    public bool isBtmColliding;
    public bool previsBtmColliding;

    bool coyoteSig;
    bool jInput;
    bool wInput;
    bool jSig;
    bool wallJSig;
    public bool wallTouch;
    public bool leftCol;
    public bool rightCol;
    public bool downCol;
    public bool upCol;
    bool isOnGround;
    bool gOn;
    
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

        //VISUAL ASSETS UNCOMMENT IN SCENES WITH 3D MODEL


        
    }

    private void FixedUpdate()
    {
        GetInput();
        bufferRefresh();
        move();
        coyoteTime();
        groundContact();// handles logic for mechanics that suspend gravity
        jump();
        gravity(gForceVec, wallGForce, -lowGrav, gOn); 
        wallSlide();
        wallJump();
        faceDir();
    }

    void bufferRefresh()
    {
        if (coyoteSig)
        {
            coyoteBuffer++;
            if (coyoteBuffer == 5)
            {
                coyoteBuffer = 0;
                coyoteSig = false;
            }
        }
        if (wallJSig)
        {
            wallJbuffer++;
            if (wallJbuffer == 5)
            {
                wallJbuffer = 0;
                wallJSig = false;
            }
        }    
        

    }

    void stopMoving()
    {
        rb.velocity = Vector3.zero;
    }
    void GetInput()
    {
        hInput = Input.GetAxis("Horizontal");
        wInput = Input.GetKey(KeyCode.LeftShift);
        jInput = Input.GetKey(KeyCode.Space);
    }

    void initPlayerParams()
    {
        leftCol= false;
        rightCol= false;
        upCol= false;
        downCol= false;
        wallJbuffer = 0;
        gOn = true;
        stopMoving();
        defaultPos = transform.position;
        wallSlideVel = new Vector3(0, -wallSlideSpeed, 0);


        animCont = vis.GetComponent<Animator>(); 
    }
    void updatePlayerParams()
    {
        wallGForce = Vector3.zero;
        gForceVec = new Vector3(0, -gravityForce, 0);
    }

    // Method to make player move
    void move()
    {
        hvec.y = rb.velocity.y;
        if (wallJSig)
        {
            Debug.Log("No movement due to wall touch");
        }
        else 
        {
             if (rightCol)//restrict direction if blocked
                    {
                        if (hvec.x > 0) { hvec.x = 0; Debug.Log("right move blocked"); }
                        
                    }
            else
            {
                hvec.x = hInput;
            }
            if (leftCol)//restrict direction if blocked
            {
                if (hvec.x < 0) { hvec.x = 0; Debug.Log("left move blocked"); }
                
            }
            else
            {
                hvec.x = hInput;
            }

            if (!isBtmColliding)
            {
                hvec.x = hvec.x * airMoveSpeed;

            }
            else
            {
                hvec.x = hvec.x * moveSpeed;
            }
            rb.velocity = hvec;

        }
       
    }

   
    void wallSlide()
    {


        if (rightCol && !isBtmColliding)
        {
            if (wInput)
            {
                wallTouch = true;
                if (rb.velocity.y < -2f)
                {
                    wallSlideVel = new Vector3(rb.velocity.x, -wallSlideSpeed, 0);

                }
                else
                {
                    wallSlideVel = rb.velocity;
                }
                rb.velocity = wallSlideVel;
                gOn = false;
            }

            else
            {
                wallTouch = false;
            }
            Debug.Log("Right Air Collision");
        }
        else if (leftCol && !isBtmColliding)
        {
            if (wInput)
            {
                wallTouch = true;
                if (rb.velocity.y < -2f)
                {
                    wallSlideVel = new Vector3(rb.velocity.x, -wallSlideSpeed, 0);

                }
                else
                {
                    wallSlideVel = rb.velocity;
                }
                rb.velocity = wallSlideVel;
                gOn = false;
                Debug.Log("Wall is being Grabbed");
            }
            else
            {
                wallTouch = false;
                

            }
            Debug.Log("Left Air Collision");
            
        }

        // checks for walls before granting gravity
        else if (!isBtmColliding)
        {
            if (!rightCol)
            {
                if (!leftCol)
                {
                    
                    gOn = true;
                    Debug.Log("ground contact gravity on");
                    wallTouch = false;
                    Debug.Log("No Air collision");

                }

            }


        }
        else if (!leftCol && !rightCol)
        {
            wallTouch = false;
            Debug.Log("No Air collision");
        }
        else if (rightCol || leftCol)
        {
            Debug.Log("Ground collision");
        }
    }


    void wallJump()
    {
        if (wallTouch)
        {
            if (jInput && rightCol)
            {
                rb.velocity = new Vector3(-30, wallJumpSpeed, 0);
                wallJSig = true;
                animCont.SetTrigger("Jump");
                Debug.Log("Just Wall Jumped to the right");
            }
            else if (jSig && rightCol)
            {
                rb.velocity = new Vector3(-30, wallJumpSpeed, 0);
                wallJSig = true;
                animCont.SetTrigger("Jump");
                Debug.Log("Just Wall Jumped to the right");
            }
            if (jInput && leftCol)
            {
                rb.velocity = new Vector3(30, wallJumpSpeed, 0);
                wallJSig = true;
                animCont.SetTrigger("Jump");
                Debug.Log("Just Wall Jumped to the left");
            }
            else if (jSig && leftCol)
            {
                rb.velocity = new Vector3(30, wallJumpSpeed, 0);
                Debug.Log("Just Wall Jumped to the left");
                animCont.SetTrigger("Jump");
                wallJSig = true;
            }
        }
        
    }

    void coyoteTime()
    {
        if(jInput && coyoteSig)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
            //animCont.SetTrigger("Jump");
            Debug.Log("Just jumped with COYOTE");
            coyoteBuffer = 0;
            coyoteSig= false;
        }
        if (coyoteSig)
        {
            
            Debug.Log("Coyote buffer frame count is " + jBufferTimer);

        }
    }
    // Jump method
    [ContextMenu("jump")]
    void jump()
    {
        if (jInput && isBtmColliding)//Retain Jump for 5 frames if not touchning ground yet
        {
            if (isBtmColliding)
            {
                Debug.Log("Just jumped");
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
                animCont.SetTrigger("Jump");
                coyoteSig= false;
            }

        }
        else if(jInput && !isBtmColliding)
        {
            Debug.Log("Just stored input");
            jSig = true;
            jBufferTimer = 0;
        }
        if (jSig == true)
        {
            jBufferTimer++;
            Debug.Log("Jump buffer frame count is " + jBufferTimer);
            if (isBtmColliding && downCol)
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

    void groundContact()
    {

        previsBtmColliding = isBtmColliding;


        // Raycast handles ground contact from now on.

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out gHit, 1.2f))
        {
            Debug.Log("Ground Check HIT");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * gHit.distance, Color.red);


            isBtmColliding = true;
            animCont.SetBool("IsGround", isBtmColliding);
        }

        else
        {
            Debug.Log("Did not Hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1, Color.white);
            isBtmColliding = false;
            animCont.SetBool("IsGround", isBtmColliding);
            if (previsBtmColliding)
            {
                coyoteSig = true;
            }

        }



        if (isBtmColliding == true)//restrict direction if blocked
        {
            if (hvec.y < 0) { hvec.y = 0; Debug.Log("Fall speed stop blocked"); }
            rb.velocity = hvec;
            gOn = false;
        }

        
        else
        {
            gOn = true;
        }
        Debug.Log("is Btm Colliding is " + isBtmColliding);
    }
    //
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
        //isColliding = true;
      
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
        //isColliding = false;
        //coyoteSig = true;
        Debug.Log("No more coliding" + collisionInfo.gameObject.name);
    }

    // gravity Vector
    void gravity(Vector3 gForce, Vector3 wallForce, float lowGForce, bool gON)
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


    private void OnGUI()
    {
        if (GUILayout.Button("Reset Player"))
        {
            transform.position = defaultPos;
            Debug.Log("Hello!");
        }
    }
}


