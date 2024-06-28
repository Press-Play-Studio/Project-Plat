using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotColliderScript : MonoBehaviour
{
    public bool isBottom;
    bool isBot;
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
        if (isBottom && trigger.tag == "Normal")
        {
            isBot = true;
            gameObject.SendMessageUpwards("groundContact", isBot);
            Debug.Log(this.name + "trigger!!");
        }
        if (!isBottom && trigger.tag == "Normal")
        {
            isBot = true;
            gameObject.SendMessageUpwards("groundContact", isBot);
            Debug.Log(this.name + "trigger!!");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT SIGNAL");
        if (isBottom && other.tag == "Normal")
        {
            isBot = false;
            gameObject.SendMessageUpwards("groundContact", isBot);
        }
        if (!isBottom && other.tag == "Normal")
        {
            isBot = false;
            gameObject.SendMessageUpwards("groundContact", isBot);
        }
    }


}
