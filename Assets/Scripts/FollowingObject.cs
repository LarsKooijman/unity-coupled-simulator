using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    //public GameObject CarDriver;
    //public GameObject Pedestrian;
    private GameObject player;
    private GameObject PlayerObject;
    private GameObject NetworkManager;
    
    private GameObject[] CheckCams;

    private Vector3 offset;

    private bool FollowPedestrian = false;
    private bool FollowCarDriver = false;
    private bool Host = false;
    private bool Lan = false;

    private int CameraNumber;

    private void Awake()
    {
        if (GameObject.Find("CarDriver(Clone)") != null)
        {
            player = GameObject.Find("CarDriver(Clone)");
        }
        else if (GameObject.Find("Pedestrian(Clone)") != null)
        {
            player = GameObject.Find("Pedestrian(Clone)");
        }
            
        PlayerObject = GameObject.Find("PlayerObject(Clone)");
        NetworkManager = GameObject.Find("NetworkManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        // CheckCams = GameObject.FindGameObjectsWithTag("Camera");
        // if (CheckCams.Length > 1)
        // {
        //     Debug.Log("Multiple Cameras Active. Abort Simulation");
        //     Application.Quit();               
        // }

        FollowPedestrian = PlayerObject.GetComponent<PlayerObject>().FollowPedestrian;
        FollowCarDriver = PlayerObject.GetComponent<PlayerObject>().FollowCarDriver;
        Host = NetworkManager.GetComponent<WhoJoins>().Host;
        Lan = NetworkManager.GetComponent<WhoJoins>().Lan;

        if (FollowPedestrian == true || FollowCarDriver == false)
        {
            CameraNumber = 1;
            Debug.Log("1");
            offset = transform.position - player.transform.position;
        }
        else if (FollowPedestrian == false || FollowCarDriver == true)
        {
            CameraNumber = 2;
            Debug.Log("2");
            offset = transform.position - player.transform.position;
        }

        else if (FollowPedestrian == false || FollowCarDriver == false)
        {
            Debug.Log("No Cameras Spawned or Active");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}