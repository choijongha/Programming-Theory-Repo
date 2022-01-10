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
    public float speed = 0;
    [SerializeField] LayerMask groundlayer;
    [SerializeField] GameObject trail;
    [SerializeField] GameObject catchTrail;

    private float restoreCatchTrail;
    private bool trailShoot;
    private void Awake()
    {
        MainManager = GameObject.Find("Main Manager").gameObject.GetComponent<GameManager>();
        playerNavMA = GetComponent<NavMeshAgent>();
        //playerNavMA.speed = speed;
        playerNavMA.acceleration = 999;
        playerNavMA.angularSpeed = 999;

        playerRb = GetComponent<Rigidbody>();

        //playerNavMA.updatePosition = false;
    }
    void Start()
    {
        playerNavMA.speed = speed;
        restoreCatchTrail = catchTrail.transform.localPosition.z;
    }
    private void Update()
    {
        
        PlayerRotate();
        MousePosOnPlane();
        if (playerNavMA.isStopped == true)
        {
            playerNavMA.velocity = Vector3.zero;
        }
        InstantiateParticle();
        ThrowTrail();
        ThrowTrailBack();

        if (trailShoot)
        {
            StartCoroutine("TrailBack");
        }
        

    }
    /*private Vector3 MousePosWorldPoint()
    {
        mousePos = MainManager.playerCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, MainManager.cameraHeight));
        return mousePos;
    }*/
    private void MousePosOnPlane() 
    {
        if (MainManager.isRoundStart && Input.GetMouseButtonDown(1))
        {
         //MousePosWorldPoint();
         Ray ray = MainManager.playerCamera.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
        if (Physics.Raycast(ray, out hit, groundlayer))
            {

            playerNavMA.isStopped = false;

            if (playerNavMA.speed == 0)
                {
                    playerNavMA.speed = speed;
                }
            playerNavMA.SetDestination(hit.point);

            //transform.position = Vector3.SmoothDamp(transform.position, playerNavMA.nextPosition, ref velocity, 0.1f);
            }
        }
    }
    private void PlayerRotate()
    {
        // ScreenToWorldPoint를 사용해 마우스 위치를 가져옴.
        /*Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = transform.position;

        mousePosition.z = playerPosition.y - MainManager.playerCamera.transform.position.y;
        
        Vector3 target = MainManager.playerCamera.ScreenToWorldPoint(mousePosition);

        float dx = target.x - playerPosition.x;
        float dz = target.z - playerPosition.z;

        float rotateDegree = Mathf.Atan2(-dx,-dz) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);*/

        // Ray로 마우스위치를 가져옴.
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = transform.position;

        Ray mouseRay = MainManager.playerCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit))
        {
            float dx = hit.point.x - playerPosition.x;
            float dy = hit.point.z - playerPosition.z;

            float rotateDegree = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);
        }
    }
    private void InstantiateParticle()
    {
        trail.transform.position = transform.position;
    }
    private void ThrowTrail()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            
            restoreCatchTrail += 10f * Time.deltaTime;
            
        }
    }
    private void ThrowTrailBack()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            catchTrail.transform.localPosition = new Vector3(0, 0, restoreCatchTrail);
            trailShoot = true;
        }
    }
    IEnumerable TrailBack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("z");
        catchTrail.transform.localPosition = Vector3.zero;
        trailShoot = false;
    }
}
