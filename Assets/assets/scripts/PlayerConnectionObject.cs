using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerConnectionObject : NetworkBehaviour {
	public GameObject PlayerUnitPrefab;
	// Use this for initialization
	void Start () {
        // Is this actually my own local PlayerConnectionObject?
        if( isLocalPlayer == false )
        {
            // This object belongs to another player.
            return;
        }

        // Since the PlayerConnectionObject is invisible and not part of the world,
        // give me something physical to move around!

        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");

        CmdSpawnMyUnit();
	}

    

    
	
	// Update is called once per frame
	void Update () {
		// Remember: Update runs on EVERYONE's computer, whether or not they own this
        // particular player object.

        /*if( isLocalPlayer == false )
        {
            return;
        }

        if( Input.GetKeyDown(KeyCode.S) )
        {
            CmdSpawnMyUnit();
        }

        if( Input.GetKeyDown(KeyCode.Q) )
        {
            string n = "Quill" + Random.Range(1, 100);

            Debug.Log("Sending the server a request to change our name to: " + n);
            CmdChangePlayerName(n);
        }
*/
	}

   

    //////////////////////////// COMMANDS
    // Commands are special functions that ONLY get executed on the server.

    [Command]
    void CmdSpawnMyUnit()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(PlayerUnitPrefab);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    

    //////////////////////////// RPC
    // RPCs are special functions that ONLY get executed on the clients.

/*   [ClientRpc]
    void RpcChangePlayerName(string n)
    {
        Debug.Log("RpcChangePlayerName: We were asked to change the player name on a particular PlayerConnectionObject: " + n);
        PlayerName = n;
    }

*/}
