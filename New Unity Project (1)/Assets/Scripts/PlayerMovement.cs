using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameManager MainManager;
    private Vector3 mousePos;

    private NavMeshAgent playerNavMA;
    [SerializeField] float speed = 0;
    [SerializeField] LayerMask groundlayer;
    [SerializeField] GameObject trail;
    [SerializeField] GameObject catchTrail;
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
    }
    private void Update()
    {       
        PlayerRotate();
        MousePosOnPlane();
        InstantiateParticle();
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
        if (!MainManager.onPauseButton)
        {
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
        
    }
    private void InstantiateParticle()
    {
        trail.transform.position = transform.position;
    }
}
