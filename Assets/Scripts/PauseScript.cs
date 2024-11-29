using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PauseScript : MonoBehaviour
{
    // FOR THIS SCRIPT TO WORK WELL YOU WILL NEED TO DROP THE CAMERA OF THE SCENE ON TO THE FIELD FOR 'MAIN CAM' 
    public Camera mainCam;
    public InputActionReference pause;
    public bool isPause;
    private float fixedDeltaTime;


    //UI variables
    public GameObject myUI;
    GameObject myText;
    public Canvas myCanvas;
    RectTransform rectTransform;
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        initCanvas();

    }

    // Update is called once per frame
    void Update()
    {
        pauseHandler();

    }

    void pauseHandler()
    {
        if (onPause())
        {
            if (isPause)
            {
                Time.timeScale = 1f;
                myCanvas.gameObject.SetActive(false);//remove ui overlay
                isPause = false;
                Debug.Log("In play state");
            }
            else
            {
                Time.timeScale = 0f; //stop time
                myCanvas.gameObject.SetActive(true); //overlay ui
                isPause = true;
                Debug.Log("In Pause State");
            }
        }

    }
    bool onPause()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("escape key was pressed");
            return true;
        }
        else
        {
            return false;
        }
    }

    void initCanvas()
    {
        myUI = new GameObject();
        mainCam = GetComponent<Camera>();
        myUI.name = "Pause Screen";
    

        myCanvas= myUI.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera= mainCam;
        myUI.AddComponent<CanvasScaler>();
        myUI.AddComponent<GraphicRaycaster > ();

        myCanvas.gameObject.SetActive(false);
        //Text

        // -Game Paused- Text
        myText = new GameObject();
        myText.transform.parent = myUI.transform;
        myText.name = "Game Paused";


        text = myText.AddComponent<TextMeshPro>();
        text.text = "game paused";
        text.fontSize = 25;

        // - Game Paused -Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        //rectTransform.sizeDelta = new Vector2(400, 200);

        Debug.Log("canvas exists");
    }
}
    


