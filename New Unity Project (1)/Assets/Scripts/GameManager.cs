using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isRoundStart { get; private set; }
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject playerCameraObject;
    [SerializeField] private GameObject playerObject;

    public float cameraHeight = 10f;
    private Vector3 playerCameraView;
    public Camera playerCamera { get; private set; }
    

    private void Awake()
    {
        playerCamera = playerCameraObject.transform.GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCameraView = new Vector3(0, cameraHeight, 0);
        StartCoroutine("RoundStart");   
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator RoundStart()
    {
        yield return new WaitForSeconds(5f);
        isRoundStart = true;
        playerCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        playerCameraObject.transform.position = playerObject.transform.position + playerCameraView;
    }
}