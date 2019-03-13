using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

    //public Camera camera1;
    //public Camera camera2;

	// Use this for initialization
	void Start () {
        // Is this actually my own local PlayerConnectionObject?
        if( isLocalPlayer == false )
        {
            // This object belongs to another player.
            return;
        }

        //camera1 = GameObject.Find("Camera1");
        //camera2 = GameObject.Find("Camera2");

        //if (isServer)
        //{
        //    camera1.enabled = true;
        //    camera2.enabled = false;
        //}
        //else
        //{
        //    camera1.enabled = false;
        //    camera2.enabled = true;
        //}

        // Since the PlayerConnectionObject is invisible and not part of the world,
        // give me something physical to move around!



        // Instantiate() only creates an object on the LOCAL COMPUTER.
        // Even if it has a NetworkIdentity is still will NOT exist on
        // the network (and therefore not on any other client) UNLESS
        // NetworkServer.Spawn() is called on this object.

        //Instantiate(PlayerUnitPrefab);

        // Command (politely) the server to SPAWN our unit
        if (isServer)
        {
            Debug.Log("PlayerConnectionObject::Start -- Spawning pedestrian.");
            CmdSpawnPedestrian();
        }
        else if (isClient)
        {
            Debug.Log("PlayerConnectionObject::Start -- Spawning driver.");
            CmdSpawnDriver();
        }
    }

    public GameObject PedestrianPrefab;
    public GameObject DriverPrefab;

    // SyncVars are variables where if their value changes on the SERVER, then all clients
    // are automatically informed of the new value.
    [SyncVar(hook="OnPlayerNameChanged")]
    public string PlayerName = "Anonymous";
	
	// Update is called once per frame
	void Update () {
		// Remember: Update runs on EVERYONE's computer, whether or not they own this
        // particular player object.

        if( isLocalPlayer == false )
        {
            return;
        }

        //if( Input.GetKeyDown(KeyCode.S) )
        //{
        //    CmdSpawnMyUnit();
        //}

        if( Input.GetKeyDown(KeyCode.Q) )
        {
            string n = "Quill" + Random.Range(1, 100);

            Debug.Log("Sending the server a request to change our name to: " + n);
            CmdChangePlayerName(n);
        }

	}

    void OnPlayerNameChanged(string newName)
    {
        Debug.Log("OnPlayerNameChanged: OldName: "+PlayerName+"   NewName: " + newName);

        // WARNING:  If you use a hook on a SyncVar, then our local value does NOT get automatically
        // updated.
        PlayerName = newName;

        gameObject.name = "PlayerConnectionObject ["+newName+"]";
    }

    //////////////////////////// COMMANDS
    // Commands are special functions that ONLY get executed on the server.

    [Command]
    void CmdSpawnPedestrian()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(PedestrianPrefab);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdSpawnDriver()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(DriverPrefab);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdChangePlayerName(string n)
    {
        Debug.Log("CmdChangePlayerName: " + n);

        // Maybe we should check that the name doesn't have any blacklisted words it?
        // If there is a bad word in the name, do we just ignore this request and do nothing?
        //    ... or do we still call the Rpc but with the original name?

        PlayerName = n;

        // Tell all the client what this player's name now is.
        //RpcChangePlayerName(PlayerName);
    }

    //////////////////////////// RPC
    // RPCs are special functions that ONLY get executed on the clients.

/*    [ClientRpc]
    void RpcChangePlayerName(string n)
    {
        Debug.Log("RpcChangePlayerName: We were asked to change the player name on a particular PlayerConnectionObject: " + n);
        PlayerName = n;
    }

*/}
