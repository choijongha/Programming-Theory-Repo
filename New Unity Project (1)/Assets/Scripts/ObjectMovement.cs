using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public List<GameObject> prefabList;
    private int prefabDongCount;

    private Rigidbody objectRb;
    private GameManager gameManager;
    public int speed = 0;
    private bool OnplayerTrigger;
    private bool OnArea1;
    private bool OnArea2;
    private bool OnArea3;
    private bool OnArea4;


    [SerializeField] GameObject dongPrefab;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Manager").GetComponent<GameManager>();
    }
    protected virtual void AnimalMovement()
    {
        InitialMovemenet();
        AreaMove();
    }
    protected virtual void InvokeInstantiate() 
    {
        InvokeRepeating("InstantiateDong", 1f, 1f);
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
            OnplayerTrigger = true;
        }
        if(other.gameObject.tag == "Area1")
        {
            OnArea1 = true;
            OnArea2 = false;
            OnArea3 = false;
            OnArea4 = false;
            
        }
        else if (other.gameObject.tag == "Area2")
        {
            OnArea2 = true;
            OnArea1 = false;
            OnArea3 = false;
            OnArea4 = false;

        }
        else if (other.gameObject.tag == "Area3")
        {
            OnArea3 = true;
            OnArea2 = false;
            OnArea1 = false;
            OnArea4 = false;
        }
        else if (other.gameObject.tag == "Area4")
        {
            OnArea4 = true;
            OnArea2 = false;
            OnArea3 = false;
            OnArea1 = false;
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
        if (OnArea1)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (OnArea2)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (OnArea3)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (OnArea4)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }   
}
