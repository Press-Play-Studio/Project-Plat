using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalacticeScript : MonoBehaviour
{

    [SerializeField] BoxCollider TriggerArea;
    [SerializeField] Rigidbody Rigidbody;

    [Range(1f, 900f)]
    public float fallSpeed;

    Vector3 fallVector;

    public bool isInPri;
    public bool isInSec;

    public bool isInFF;
    MeshRenderer selfRender;
    // Start is called before the first frame update
    void Start()
    {
        var selfRender = GetComponent<Renderer>();
        selfRender.enabled = true;
        selfRender.material.SetColor("_color", Color.blue);
        Rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        fallVector = new Vector3(0, -fallSpeed, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched");
        if (other.tag == "Player")
        {
            Debug.Log("Touched The Player!");
            fallDown();
        }
    }


    void spawnTriggerBoxes()
    {
        // Spawn with numbers to expose in inspector to resize
    }
    void fallDown()
    {
        Rigidbody.AddForce(fallVector);
        // Rigidbody.useGravity= true; 
    }

    /// for now code to detect when in area and activate rigidbodyto fall down then delete the collider for performance and then think about adjusting size through gizmos with numbers to make easy to use
}
