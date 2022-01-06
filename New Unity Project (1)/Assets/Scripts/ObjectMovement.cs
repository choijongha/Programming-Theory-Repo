using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Rigidbody objectRb;
    private GameManager gameManager;
    public int speed = 0;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Manager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
    protected virtual void InitialStart()
    {
        if (gameManager.IsDoorOpen)
        {
            Vector3 initialPos = transform.position;
            initialPos.z = initialPos.z + speed * Time.deltaTime;
            transform.position = initialPos;
        }
    }
}
