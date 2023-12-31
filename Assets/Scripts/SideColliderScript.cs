using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideColliderScript : MonoBehaviour
{
    public bool isLWall;
    public bool isRWall;
    public bool isLeft;
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
        if (isLeft)
        {
            isLWall = true;
            gameObject.SendMessageUpwards("leftWallSet", isLWall);
            Debug.Log(this.name + "trigger!!");
        }
        if (!isLeft)
        {
            isRWall = true;
            gameObject.SendMessageUpwards("rightWallSet", isRWall);
            Debug.Log(this.name + "trigger!!");
        }           

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT SIGNAL");
        if (isLeft)
        {
            isLWall = false;
            gameObject.SendMessageUpwards("leftWallSet", isLWall);
        }
        if (!isLeft)
        {
            isRWall = false;
            gameObject.SendMessageUpwards("rightWallSet", isRWall);
        }
    }
    
}
