using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 moveValue;
    public bool jumpValue;
    public bool grabValue;
    public bool dashValue;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction grabAction;
    public InputAction dashAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        grabAction = InputSystem.actions.FindAction("Grab");
        dashAction = InputSystem.actions.FindAction("Dash Attack");
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        jumpValue = jumpAction.IsPressed();
        grabValue= grabAction.IsPressed();
        dashValue= dashAction.IsPressed();
    }


}
