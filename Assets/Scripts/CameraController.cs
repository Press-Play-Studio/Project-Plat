using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector2 camPos;
    public Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        camPos= new Vector2 (transform.position.x,transform.position.y);
        playerPos = new Vector2 (Player.transform.position.x,Player.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        transform.position = new Vector3 (playerPos.x, playerPos.y, transform.position.z);
    }
}
