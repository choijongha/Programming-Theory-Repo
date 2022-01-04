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
    public float Speed = 10;
    public LayerMask groundlayer;
    private void Awake()
    {
        MainManager = GameObject.Find("Main Manager").gameObject.GetComponent<GameManager>();
        playerNavMA = GetComponent<NavMeshAgent>();
        playerNavMA.speed = Speed;
        playerNavMA.acceleration = 999;
        playerNavMA.angularSpeed = 999;

        playerRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if (MainManager.isRoundStart && Input.GetMouseButtonDown(1))
        {
            //MousePosWorldPoint();
            MousePosOnPlane();
        }
    }
    /*private Vector3 MousePosWorldPoint()
    {
        mousePos = MainManager.playerCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, MainManager.cameraHeight));
        return mousePos;
    }*/
    private void MousePosOnPlane() 
    {
        Ray ray = MainManager.playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, groundlayer))
        {
            playerNavMA.SetDestination(hit.point);
            playerNavMA.isStopped = false;
        }

    }
    public virtual void GoTo(Vector3 position)
    {
    
        playerNavMA.SetDestination(position);
        
    }

}
