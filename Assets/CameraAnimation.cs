using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
	public Transform[] nodes;
	private Transform currentNode;
	private int currentIndex = 0 ;
	public float speed = 0.2f;
	void Start () {
		currentNode = nodes [0];
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, currentNode.position, speed);
		transform.rotation = Quaternion.Slerp (transform.rotation, currentNode.rotation, speed);
	}

	public void nextNode()
	{
		if (currentIndex >= nodes.Length - 1) {
			currentIndex = 0; 
		} else {
			currentIndex += 1;
		}
		currentNode = nodes [currentIndex];
	}

	public void buttonClick(string action) {
		StartCoroutine (doAction (action));
	}



	IEnumerator doAction(string action)
	{
		yield return new WaitForSeconds (2f);
		switch (action) {
			case "start":
			Application.LoadLevel ("OfflineScene");
				break;
			case "options":
				break;
			case "exit":
				Application.Quit ();
				break;
		}
	}
}
