using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;
public class GameManager : NetworkBehaviour {
	GameObject fighter = null,enemy;
	public GameObject win;
	public Text winner;
	public int player1 = 0,player2 = 0;
	void Update(){


		if (player2 == 3) {
			Debug.Log("player1");
			win.SetActive (true);
			winner.text = "GreenRockEater & SliferKing & HalleBarde win";
		}
		if (player1 == 3) {
			Debug.Log("player2");
			win.SetActive (true);
			winner.text = "Predator & GodOfShadow & DioBrando win";
		}

		if (Input.GetMouseButtonDown (0)) { // if left button pressed...
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				//hit.transform.gameObject.GetComponent<Charactercontroller>().Attack();
				if(fighter == null){
					fighter = hit.transform.gameObject;
				} else {
					if(fighter.GetComponent<Charactercontroller>().id != hit.transform.gameObject.GetComponent<Charactercontroller>().id)
					{
						fighter.GetComponent<Charactercontroller>().Enemy = hit.transform.gameObject.GetComponent<Charactercontroller>();
						enemy = hit.transform.gameObject;
						Cmdgoserverfight1 (fighter,enemy);
						fighter = null;
					}
					else{
						fighter = null;
					}
				}
			}
			//Cmdgoserver ();
		}

	}
	public void MainScene()
	{
		Application.LoadLevel ("index0");
	}

	[Command]
	void Cmdgoserverfight1(GameObject fighter,GameObject enemi)
	{
		RpcGoPlayerfight1(fighter,enemi);

	}

	[ClientRpc]
	void RpcGoPlayerfight1(GameObject fighter,GameObject enemi)
	{
		//if (hasAuthority) {
		GameObject gm,en;
		Debug.Log (fighter.tag);
		Debug.Log (enemi.tag);
		gm = GameObject.FindWithTag (fighter.tag);
		Debug.Log (gm.tag);
		en = GameObject.FindWithTag (enemi.tag);
		Debug.Log (en.tag);
		gm.GetComponent<Charactercontroller>().Enemy = en.GetComponent<Charactercontroller>();
		gm.GetComponent<Charactercontroller> ().Attack ();


	}

}
