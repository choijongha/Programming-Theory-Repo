using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ObjectMovement : MonoBehaviour
{
    public List<GameObject> prefabList;
    private int prefabDongCount;

    private Rigidbody objectRb;
    private GameManager gameManager;
    private Vector3 initialPos;
    public int speed = 0;

    private bool onplayerTrigger;
    private bool onArea1;
    private bool onArea2;
    private bool onArea3;
    private bool onArea4;
    private bool onInstant;
    public bool isCatch;

    private GameObject player;

    [SerializeField] GameObject dongPrefab;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }
    protected virtual void IsStart(int animalSpeed)
    {
        initialPos = transform.position;
        onInstant = true;
        speed = animalSpeed;
    }
    protected virtual void AnimalMovement()
    {
        // ABSTRACTION
        InitialMovemenet();
        OnPlayerTrigger();
        AreaMove();
    }
    protected virtual void InvokeInstantiate(float time) 
    {
        if (onInstant)
        {
            InvokeRepeating("InstantiateDong", 1f, time);
        }
        
    }
    private void InstantiateDong()
    { 
        prefabDongCount++;
        prefabList.Add(Instantiate(dongPrefab, transform.position, transform.rotation));

        if(prefabDongCount > 10)
        {
            GameObject.Destroy(prefabList[0].gameObject);
            prefabDongCount--;
            prefabList.RemoveAt(0);
        } 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameManager.isRoundStart)
        {
            onArea1 = false;
            onArea2 = false;
            onArea3 = false;
            onArea4 = false;
            onplayerTrigger = true;
        }
        if(other.gameObject.tag == "Area1" && !onplayerTrigger)
        {
            onArea1 = true;
            onArea2 = false;
            onArea3 = false;
            onArea4 = false;
            
        }
        else if (other.gameObject.tag == "Area2" && !onplayerTrigger)
        {
            onArea2 = true;
            onArea1 = false;
            onArea3 = false;
            onArea4 = false;

        }
        else if (other.gameObject.tag == "Area3" && !onplayerTrigger)
        {
            onArea3 = true;
            onArea2 = false;
            onArea1 = false;
            onArea4 = false;
        }
        else if (other.gameObject.tag == "Area4" && !onplayerTrigger)
        {
            onArea4 = true;
            onArea2 = false;
            onArea3 = false;
            onArea1 = false;
        }
        
    }
    private void InitialMovemenet()
    {
        if (gameManager.IsDoorOpen && !gameManager.isRoundStart)
        {
            Vector3 initialPos = transform.position;
            initialPos.z = initialPos.z + speed * Time.deltaTime;
            transform.position = initialPos;
            
        };
    }
    private void AreaMove()
    {
        if (onArea1)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (onArea2)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (onArea3)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (onArea4)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }     
    }   
    protected virtual void OnPlayerTrigger()
    {
        if (onplayerTrigger)
        {
            transform.position = initialPos;
            onInstant = false;
            isCatch = true;
        }
    }
    private void RandomSystem()
    {
        int randomBool = Random.Range(0, 4);
        switch (randomBool)
        {
            case 0:
                onArea1 = true;
                break;
            case 1:
                onArea2 = true;
                break;
            case 2:
                onArea3 = true;
                break;
            case 3:
                onArea4 = true;
                break;
        }
    }
}
