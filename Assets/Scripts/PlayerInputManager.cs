using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{



    public Vector2 moveDir;

    public InputActionReference move;
    public InputActionReference jump;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = move.action.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        jump.action.started += onjump;


    }

    private void OnDisable()
    {
        jump.action.started -= onjump;
    }

    void onjump(InputAction.CallbackContext obj)
    {
        Debug.Log("fired");
        Debug.Log(obj.performed.ToString());
    }


}
