using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomColliderScript : MonoBehaviour
{
    public bool isBtm;
    public bool isBtmT;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (isBtm && other.tag == "Normal")
        {
            isBtmT = true;
            gameObject.SendMessageUpwards("groundContact", isBtmT);
            Debug.Log(this.name + "trigger!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT SIGNAL");
        if (isBtm && other.tag == "Normal")
        {
            isBtmT = false;
            gameObject.SendMessageUpwards("leftWallSet", isBtmT);
        }

    }
}
