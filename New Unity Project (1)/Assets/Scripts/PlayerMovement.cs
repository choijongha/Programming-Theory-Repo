using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameManager MainManager;
    private Vector3 mousePos;
    private void Awake()
    {
        MainManager = GameObject.Find("Main Manager").gameObject.GetComponent<GameManager>();
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float playerRotate = playerRb.rotation.y;
        playerRotate = Quaternion.Euler(MousePosWorldPoint());
    }
    private void FixedUpdate()
    {
        if (MainManager.isRoundStart)
        {
            MousePosWorldPoint();
            Debug.Log(MousePosWorldPoint());
            PlayerMoveMousePos();
        }
    }
    private Vector3 MousePosWorldPoint()
    {
        mousePos = MainManager.playerCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, MainManager.cameraHeight));
        return mousePos;
    }
    private void PlayerMoveMousePos()
    {
        if (Input.GetMouseButtonDown(1))
            {
                
                playerRb.AddForce(mousePos - transform.position, ForceMode.Impulse);
                Debug.Log(mousePos);
            }
    }

}
