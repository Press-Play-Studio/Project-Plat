using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PauseScript : MonoBehaviour
{
    // FOR THIS SCRIPT TO WORK WELL YOU WILL NEED TO DROP THE CAMERA OF THE SCENE ON TO THE FIELD FOR 'MAIN CAM' 
    public Camera mainCam;
    public InputAction pause;
    public bool isPause;
    public bool pauseButton;
    private float fixedDeltaTime;


    // Player character reference
    public PlayerCharacter pScript;
    public GameObject player;

    // 


    //UI variables
    public Canvas pauseCanvas;
    public TextMeshProUGUI pauseText;

    // Start is called before the first frame update
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        pause = InputSystem.actions.FindAction("Pause");
        isPause = false;
        pauseCanvas.gameObject.SetActive(false);
        pScript = GetComponentInParent<PlayerCharacter>();
        //player = GetComponentInParent<GameObject>();
        pScript = player.GetComponent<PlayerCharacter>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        pauseHandler();
        
    }

    void GetInput()
    {
        pauseButton = pause.WasPerformedThisFrame();
        pauseCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        pauseCanvas.worldCamera = mainCam;


    }
    void pauseHandler()
    {
        if (pauseButton)
        {
            if (isPause)
            {
                Time.timeScale = 1f;
                pauseCanvas.gameObject.SetActive(false);//remove ui overlay
                isPause = false;
                Debug.Log("In play state");
            }
            else
            {
                Time.timeScale = 0.01f; //stop time
                pauseCanvas.gameObject.SetActive(true); //overlay ui
                isPause = true;
                Debug.Log("In Pause State");
            }
            Debug.Log("Pause button Pressed");

        }
        else
        {
            Debug.Log("Pause not Pressed");
        }

    }

    public void resumeButton()
    {
        Time.timeScale = 1f;
        pauseCanvas.gameObject.SetActive(false);//remove ui overlay
        isPause = false;
        Debug.Log("In play state");
    }

    public void closeGame()
    {
        Application.Quit();
    }
    
    IEnumerator UnscaledUpdate()// put other code that needs to run while paused here
    {
        while (isPause)
        {
            GetInput();
            pauseHandler();
            if (!isPause)
            {
                yield break;
            }
        }
        yield return null;

    }
}
    


