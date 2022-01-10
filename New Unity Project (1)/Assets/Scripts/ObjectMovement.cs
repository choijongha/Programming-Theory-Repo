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
    [SerializeField] GameObject dongPrefab;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Manager").GetComponent<GameManager>();
    }
    protected virtual void AnimalMovement()
    {
        InitialMovemenet();
        PlayerTrigger();
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
    private void PlayerTrigger()
    {
        if (OnplayerTrigger)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
