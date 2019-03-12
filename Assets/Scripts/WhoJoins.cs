using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoJoins : MonoBehaviour
{
    public bool Host = false;
    public bool Lan = false;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Host = true;
            Debug.Log("Host");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Lan = true;
            Debug.Log("Lan");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Host = false;
            Lan  = false;
        }
    }
}
