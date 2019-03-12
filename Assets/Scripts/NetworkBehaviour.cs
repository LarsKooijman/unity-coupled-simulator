using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkBehaviour: MonoBehaviour
{
    //public Camera cam; // Drag camera into here

    void Start()
    {
        // IF I'M THE PLAYER, STOP HERE (DON'T TURN MY OWN CAMERA OFF)
        //if (isLocalPlayer == true) return;

        // DISABLE CAMERA AND CONTROLS HERE (BECAUSE THEY ARE NOT ME)
        //cam.enabled = false;
        //GetComponent<PlayerController>().enabled = false;
        //GetComponent<PlayerMovement>().enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

