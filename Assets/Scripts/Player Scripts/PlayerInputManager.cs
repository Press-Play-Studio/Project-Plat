using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{



    public Vector2 moveDir;

    public InputAction moveAction;
    public InputAction jumpAction;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnEnable()
    {
       


    }

    private void OnDisable()
    {
        
    }

    void onjump(InputAction.CallbackContext obj)
    {
        Debug.Log("fired");
        Debug.Log(obj.performed.ToString());
    }


}
