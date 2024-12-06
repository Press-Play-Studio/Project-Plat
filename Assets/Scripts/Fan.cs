using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    [SerializeField] Rigidbody playerRigid;
    [SerializeField] Transform playerTransform;
    MonoBehaviour player;


    public Collider fanCollider;


    public float fanForce;

    public bool fanEnabled;
    public bool isUp;
    public bool isRight;

    Vector3 fanVector = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        initFan();
    }

    // Update is called once per frame
    void Update()
    {
        updateFan();
    }

    void updateFan()
    {
        if (isUp)
        {
            fanVector.y = fanForce;
        }
        else if (!isUp)
        {
            fanVector.y = -fanForce;
        }
    }
    void initFan()
    {
        fanCollider= GetComponent<Collider>();


    }

    private void OnTriggerStay(Collider rigidObject)
    {
        player = rigidObject.GetComponent<PlatformerController>();
        if (player != null) 
        {
            playerRigid = player.GetComponent<Rigidbody>();
            blowFan(playerRigid);
            Debug.Log("caught player rigidbody");
        }

    }


    void blowFan(Rigidbody playerRigidB)
    {
        playerRigidB.AddForce(fanVector);
    }


}
