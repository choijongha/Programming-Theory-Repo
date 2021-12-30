using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameManager MainManager;
    private Vector3 mousePos;
    private NavMeshAgent playerNavMA;

    public LayerMask clickMask;
    public float Speed = 3;
    private void Awake()
    {
        MainManager = GameObject.Find("Main Manager").gameObject.GetComponent<GameManager>();
        playerNavMA = GetComponent<NavMeshAgent>();
        playerNavMA.speed = Speed;
        playerNavMA.acceleration = 999;
        playerNavMA.angularSpeed = 999;
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (MainManager.isRoundStart)
        {
            //MousePosWorldPoint();
            PlayerMoveMousePos();
        }
    }
    /*private Vector3 MousePosWorldPoint()
    {
        mousePos = MainManager.playerCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, MainManager.cameraHeight));
        return mousePos;
    }*/
    private void PlayerMoveMousePos()
    {
        if (Input.GetMouseButtonDown(1))
            {
            //playerRb.AddForce(MousePosOnPlane() - transform.position, ForceMode.Impulse);
            //Debug.Log(mousePos);
            Debug.Log(MousePosOnPlane());
            GoTo(MousePosOnPlane());
        }
    }
    private Vector3 MousePosOnPlane() 
    {
        Vector3 clickPos = Vector3.one;
        Ray ray = MainManager.playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            clickPos = hit.point;
        }
        return clickPos;

    }
    public virtual void GoTo(Vector3 position)
    {
    
        playerNavMA.SetDestination(position);
        playerNavMA.isStopped = false;
    }

}
