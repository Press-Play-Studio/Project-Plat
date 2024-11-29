using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideColliderScript : MonoBehaviour
{
    public bool isLWall;
    public bool isRWall;
    public bool isLeft;
    public bool isBtm;
    public bool isBtmT;
    bool isDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider trigger)
    {
        if (isLeft && trigger.tag == "Normal")
        {
            isLWall = true;
            gameObject.SendMessageUpwards("leftWallSet", isLWall);
            Debug.Log(this.name + " trigger!!");
        }
        if (!isLeft && trigger.tag == "Normal")
        {
            isRWall = true;
            gameObject.SendMessageUpwards("rightWallSet", isRWall);
            Debug.Log(this.name + " trigger!!");
        }


    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT SIGNAL");
        if (isLeft && other.tag == "Normal")
        {
            isLWall = false;
            gameObject.SendMessageUpwards("leftWallSet", isLWall);
        }
        if (!isLeft && other.tag == "Normal")
        {
            isRWall = false;
            gameObject.SendMessageUpwards("rightWallSet", isRWall);
        }
    }
    
}
