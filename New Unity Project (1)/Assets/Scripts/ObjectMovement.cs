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
    [SerializeField] GameObject dongPrefab;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Manager").GetComponent<GameManager>();
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
    
}
