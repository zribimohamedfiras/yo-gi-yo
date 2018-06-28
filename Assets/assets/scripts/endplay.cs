using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class endplay : MonoBehaviour {
	public GameObject win;
	public Text winner;
	public int player1 = 0,player2 = 0;

	
	// Update is called once per frame
	void Update () {
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
	}

	public void MainScene()
	{
		Application.LoadLevel ("index0");
	}
}

