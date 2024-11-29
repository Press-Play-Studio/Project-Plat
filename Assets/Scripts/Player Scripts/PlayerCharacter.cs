using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerCharacter : Character
{
    private float hInput;
    private bool jInput;

    [SerializeField] private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    // ALl the input handling for the player character
    void GetInputs()
    {
        hInput = Input.GetAxis("Horizontal");
        jInput = Input.GetKey(KeyCode.Space);
    }

}
