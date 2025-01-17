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
    public InputAction pause;
    public bool isPause;
    public bool pauseButton;
    private float fixedDeltaTime;


    //UI variables
    public GameObject pauseUI;
    public GameObject exitButtonUI;
    GameObject pauseText;
    public Canvas myCanvas;
    RectTransform rectTransform;
    public TextMeshPro pausedText;

    // Start is called before the first frame update
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        pause = InputSystem.actions.FindAction("Pause");

        initCanvas();

    }

    // Update is called once per frame
    void Update()
    {
        pauseHandler();
        
    }

    void pauseHandler()
    {
        if (pauseButton = pause.WasPressedThisFrame())
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


    public void closeGame()
    {
        Application.Quit();
    }
    void initCanvas()
    {

        mainCam = GetComponentInChildren<Camera>();
        pauseUI.name = "Pause Screen";
    

        //myCanvas= pauseUI.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera= mainCam;
   

        //pauseUI.AddComponent<CanvasScaler>();
        //pauseUI.AddComponent<GraphicRaycaster>();

        myCanvas.gameObject.SetActive(false);
        //Text

        // -Game Paused- Text
        pauseText = new GameObject();
        pauseText.transform.parent = pauseUI.transform;
        pauseText.name = "Game Paused";


        pausedText = pauseText.AddComponent<TextMeshPro>();
        pausedText.text = "game paused";
        pausedText.fontSize = 100;
        pausedText.color = Color.red;

        // - Game Paused -Text position
        rectTransform = pauseText.GetComponent<RectTransform>();
        //rectTransform.localPosition = new Vector3(0, 0, 0);
        //rectTransform.sizeDelta = new Vector2(400, 200);

        Debug.Log("canvas exists");
    }
}
    


