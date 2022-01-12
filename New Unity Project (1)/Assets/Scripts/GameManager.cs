using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public bool isRoundStart { get; private set; }
    public bool IsDoorOpen { get; private set; } = false;

    [SerializeField] GameObject playerCameraObject;
    [SerializeField] GameObject playerObject;
    Vector3 playerPos;
    [SerializeField] float cameraHeight = 10f;
    [SerializeField] GameObject openDoor;
    private Rigidbody openDoorRb;
    private Vector3 openDoorPos;
    [SerializeField] CowMovement cow;
    [SerializeField] HorseMovement horse;

    private Vector3 playerCameraView;
    public Camera playerCamera { get; private set; }
    
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
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
        GameDone();
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
            openDoorRb.AddForce(Vector3.right * 1.5f, ForceMode.Impulse);          
        }
    }
    private void GameDone()
    {
        if(cow.isCatch && horse.isCatch)
        {
            IsDoorOpen = false;
            openDoorRb.isKinematic = false;
            openDoor.transform.position = openDoorPos;
            playerObject.transform.position = playerPos;

        }
    }
}
