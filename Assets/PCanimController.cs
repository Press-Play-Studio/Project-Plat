using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCanimController : MonoBehaviour
{
    public Animator animCont;
    public GameObject player;
    public GameObject vis;
    private PlatformerController platCont;


    public bool isRun;
    // Start is called before the first frame update
    void Start()
    {
        platCont= player.GetComponent<PlatformerController>();
        animCont= vis.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        platCont = player.GetComponent<PlatformerController>();
        if (platCont.rb.velocity.x < 2f && platCont.rb.velocity.x > -2f)
        {
            isRun= false;
            animCont.SetBool("isRun", isRun);

        }
        else
        {
            isRun= true;
            animCont.SetBool("isRun", isRun);
        }
        animCont.SetBool("IsGround", platCont.isColliding);
        animCont.SetBool("wallTouch", platCont.wallTouch);
        faceDir();
    }
    // Method to know what direction you're moving in
    int moveDirection()
    {
        if (platCont.rb.velocity.x > 2)
        {
            return 2;
        }
        else if (platCont.rb.velocity.x < -2)
        {
            return 1;
        }
        else
        {
            return 3;
        }
    }
    void faceDir()
    {
        if (moveDirection() == 2)
        {
            vis.transform.rotation = Quaternion.Euler(0, 90, 0);// fill later
        }
        if (moveDirection() == 1)
        {
            vis.transform.rotation = Quaternion.Euler(0, 270, 0);// fill later
        }
    }
}