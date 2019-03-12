using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public GameObject SpawnCam;
    private Rect windowRect = new Rect(425, 225, 300, 50);
    private Transform SpawnPositionPedestrian;
    private Transform SpawnPositionCarDriver;
    private bool ChoosingPlayer = false;
    public bool FollowPedestrian = false;
    public bool FollowCarDriver = false;
    private GameObject PedestrianCamera;

    private void Awake()
    {     

    }
    // Start is called before the first frame update
    void Start()
    {
        ChoosingPlayer = true;
        GameObject temp = GameObject.Find("StartPositionPedestrian");
        SpawnPositionPedestrian = temp.GetComponent<Transform>();
        temp = GameObject.Find("StartPositionCarDriver");
        SpawnPositionCarDriver = temp.GetComponent<Transform>();
        //OnGUI();
    }

    void OnGUI()
    {
        if (ChoosingPlayer == true) 
        {
            // Register the window. 
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Select Player");
        }    
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(25, 20, 100, 20), "Pedestrian"))
        {
            Instantiate(PlayerPrefabs[0], SpawnPositionPedestrian.position, SpawnPositionPedestrian.rotation);
            ChoosingPlayer = false;
            FollowPedestrian = true;
            Instantiate(SpawnCam, SpawnPositionPedestrian.position + new Vector3(0, 10, 0), SpawnPositionPedestrian.rotation);
        }
        else if (GUI.Button(new Rect(175, 20, 100, 20), "Car Driver"))
        {
            Instantiate(PlayerPrefabs[1], SpawnPositionCarDriver.position, SpawnPositionCarDriver.rotation);
            ChoosingPlayer = false;
            FollowCarDriver = true;
            Instantiate(SpawnCam, SpawnPositionCarDriver.position + new Vector3(0, 10, 0), SpawnPositionCarDriver.rotation);
        }
    }
}
