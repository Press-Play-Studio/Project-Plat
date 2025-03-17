using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

    // MAKE THIS SCRIPT CREATE THE CAMERA WE ARE USING SO IT IS  REPEATABLE ON NEW LEVELS

{
    public GameObject Player;
    public Rigidbody rb;



    public Vector2 camPos;
    public Vector2 playerPos;


    public Vector2 camError;
    public Vector2 newCamError;
    public Vector2 de;
    public Vector3 camVel;



    public float yoffset;
    public float xoffset;
    public float xTune;
    public float yTune;

    public Vector2 integral;
    public float Ti;
    public float Td;
    public float dt;
    public float gain;


    // Start is called before the first frame update
    void Start()
    {
        camPos= new Vector2 (transform.position.x,transform.position.y);
        playerPos = new Vector2 (Player.transform.position.x,Player.transform.position.y);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        trackElasticVel();
    }

    void trackPlayerPos()
    {
        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        transform.position = new Vector3(playerPos.x+ xoffset, playerPos.y + yoffset, transform.position.z);
    }

    void trackElasticVel()
    {
        newCamError = camError;
        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        camError.x = playerPos.x - transform.position.x + xoffset;
        camError.y = playerPos.y - transform.position.y + yoffset;
        applySpeed(newCamError);

    }

    void applySpeed(Vector2 xyError)
    {
        de.x =  xyError.x - camError.x;
        de.y = xyError.y - camError.y;
        camVel = PIDcontroller(gain, camError, de, dt, Td, Ti, integral);
       




        if (Mathf.Abs(camError.x) < 1)
        {
            camVel.x = 0;
        }
        if (Mathf.Abs(camError.y) < 1)
        {
            camVel.y = 0;
        }


        rb.linearVelocity = camVel;


    }
    Vector3 PIDcontroller(float gain, Vector2 error, Vector2 de, float dt, float Td, float Ti, Vector2 integral)
    {
        integral.x += (1 / Ti) * error.x * dt;
        integral.y += (1 / Ti) * error.y * dt;


        if (integral.x > 20)
        {
            integral.x = 20f;
        }
        if(integral.x < -20)
        {
            integral.x = -20f;
        }
        if (integral.y > 10)
        {
            integral.y = 10f;
        }
        if (integral.y < -10)
        {
            integral.y = -10f;
        }


        Vector3 control;
        
        control = new (gain * (error.x + integral.x + (Td * (de.x / dt))), gain * (error.y + integral.y + (Td * (de.y / dt))), 0);


        return control;
    }


    }



