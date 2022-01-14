using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isRoundStart { get; private set; }
    public bool IsDoorOpen { get; private set; } = false;
    public bool onPauseButton { get; private set; }
    public Camera playerCamera { get; private set; }

    [SerializeField] GameObject playerCameraObject;
    [SerializeField] GameObject playerObject;
    [SerializeField] float cameraHeight = 10f;
    [SerializeField] GameObject openDoor;
    [SerializeField] CowMovement cow;
    [SerializeField] HorseMovement horse;
    [SerializeField] Text timeText;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameCompletePanel;
    [SerializeField] GameObject reallyQuitPanel;
    [SerializeField] float doorOpenPower = 3f;


    private Vector3 playerPos;
    private Rigidbody openDoorRb;
    private Vector3 openDoorPos;
    private Vector3 playerCameraView;
    float timeAdd ;
    private void Awake()
    {
        playerCamera = playerCameraObject.transform.GetComponent<Camera>();
        openDoorRb = openDoor.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        openDoorPos = openDoor.transform.position;
        playerPos = playerObject.transform.position;
        playerCameraView = new Vector3(0, cameraHeight, 0);
        playerCameraObject.transform.position = Vector3.zero + playerCameraView;
        StartCoroutine("RoundStart");
        timeAdd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
        GameDone();
        TimeText();
    }
    
    private void LateUpdate()
    {
        if (isRoundStart)
        {
            playerCameraObject.transform.position = playerObject.transform.position + playerCameraView;
        }
    }
    IEnumerator RoundStart()
    {
        yield return new WaitForSeconds(4f);
        isRoundStart = true;
        //playerCamera.gameObject.SetActive(true);
        //mainCamera.gameObject.SetActive(false);
    }
    private void OpenDoor()
    {
        if (openDoor.transform.position.x >= 4.5f)
        {
            openDoorRb.isKinematic = true;
            IsDoorOpen = true;
        }
        else
        {
            openDoorRb.AddForce(Vector3.right * doorOpenPower, ForceMode.Impulse);          
        }
    }
    private void GameDone()
    {
        if(cow.isCatch && horse.isCatch)
        {
            isRoundStart = false;
            IsDoorOpen = false;
            openDoorRb.isKinematic = false;
            openDoor.transform.position = openDoorPos;
            playerObject.transform.position = playerPos;
            playerCameraObject.transform.position = playerObject.transform.position + playerCameraView;
            StartCoroutine("GameCompleteGUIPanel");
        }
    }
    public void MainPauseButton()
    {
        onPauseButton = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void MainResumeButton()
    {
        onPauseButton = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainQuitButton()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void ReallyQuitPanelOpen()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            reallyQuitPanel.SetActive(true);
        } else if (reallyQuitPanel.activeSelf){
            pausePanel.SetActive(true);
            reallyQuitPanel.SetActive(false);
        }
        
    }
    public void MainRestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void StartMenuReturn()
    {
        SceneManager.LoadScene(0);
    }
    private void TimeText()
    {
        if (isRoundStart)
        {
            timeText.text = $"Time : {timeAdd += Time.deltaTime} ";
        }
    }
    IEnumerator GameCompleteGUIPanel()
    {
        yield return new WaitForSeconds(2f);
        gameCompletePanel.SetActive(true);
    }

}
