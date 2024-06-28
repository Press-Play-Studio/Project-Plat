using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector2 camPos;
    public Vector2 playerPos;
    public float yoffset;
    public float xoffset;
    // Start is called before the first frame update
    void Start()
    {
        camPos= new Vector2 (transform.position.x,transform.position.y);
        playerPos = new Vector2 (Player.transform.position.x,Player.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        trackPlayerPos();
    }

    void trackPlayerPos()
    {
        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        transform.position = new Vector3(playerPos.x+ xoffset, playerPos.y + yoffset, transform.position.z);
    }
}
