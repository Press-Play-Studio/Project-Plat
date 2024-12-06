using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerCharacter : Character
{
    private float hInput;
    private bool jInput;

    public PlayerInputManager pInput;

    Vector3 wallGForce;
    float lowGrav;
    bool gOn;

    [SerializeField] private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        gForceVec= new Vector3(0, -gravityForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        gravity(gForceVec, wallGForce, -lowGrav, gOn);
    }

    // ALl the input handling for the player character
    void GetInputs()
    {
        hInput = pInput.moveValue.x;
        jInput = pInput.jumpValue;
    }

}
