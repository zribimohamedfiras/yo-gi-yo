using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Charactercontroller : NetworkBehaviour
{
	public float Health;
	public float deffense;
	public float Damage;
	public Charactercontroller Enemy;
	public int id;
	public bool IsAttacking = false,backToPosition = false;
	public GameObject InitialPosition,endplay;
	public ParticleSystem blood;
	float distance,BackDistance;
	public float attackTime=1f;
	public Slider HealthBar;
	//public GameManager Gm;
	void Start()
	{
		Health = 10000f+(deffense*2);
		HealthBar.maxValue = Health;
		HealthBar.value = Health;
	}
	void Update()
	{


		if (GetComponent<BoxCollider> ().enabled)
		{
			HealthBar.gameObject.SetActive (true);
		}
		else
		{
			HealthBar.gameObject.SetActive (false);
		}

		if (IsAttacking) {
			transform.LookAt (Enemy.transform.position);
			transform.Translate (Vector3.forward * 500f * Time.deltaTime);
			distance = Vector3.Distance (transform.position, Enemy.transform.position);
			if (distance <= 500f) {
				GetComponent<Animator> ().SetInteger ("attack", 1);
				GetComponent<Animator>().SetInteger("walk",0);
				Enemy.TakeDamage (Damage);
				StartCoroutine (stopattacke ());
				IsAttacking = false;

			}
		} else {
			if (backToPosition) {
				transform.LookAt (InitialPosition.transform.position);
				transform.Translate (Vector3.forward * 500f * Time.deltaTime);
				BackDistance = Vector3.Distance (transform.position, InitialPosition.transform.position);
				Debug.Log (BackDistance);
				if (BackDistance <= 10f) {
					transform.rotation = new Quaternion (0, 0, 0, 0);
					GetComponent<Animator>().SetInteger("walk",0);
					backToPosition = false;
				}
			}
		}
	}
	public void TakeDamage(float amount)
	{
		StartCoroutine (takeDamage(amount));
	}
	public void Attack(){
		GetComponent<Animator>().SetInteger("walk",1);
		IsAttacking = true;
	}
	public void StopAttack(){
		GetComponent<Animator>().SetInteger("attack",0);
	}
	public void StopTakeDamage(){
		GetComponent<Animator>().SetInteger("take",0);
	}
	IEnumerator takeDamage(float amount){
		blood.transform.position = transform.position;
		yield return new WaitForSeconds (1.5f);

		GetComponent<Animator>().SetInteger("take",1);
		blood.Play();
		Health -= amount;
		HealthBar.value -= amount;
		StartCoroutine (StopTakeDamagee ());
		if (Health <= 0f) {
			GetComponent<Animator>().SetInteger("die",1);
			StartCoroutine(destroyGameObject());
			switch (id) {
			case 0:
				endplay.GetComponent<endplay>().player1 += 1;
				break;
			case 1:
				endplay.GetComponent<endplay>().player2 += 1;
				break;
			}
		}

	}
	IEnumerator destroyGameObject(){
		yield return new WaitForSeconds (5f);
		Destroy (gameObject);
		Destroy(HealthBar.gameObject);
	}
	IEnumerator stopattacke(){
		yield return new WaitForSeconds (attackTime);
		GetComponent<Animator>().SetInteger("attack",0);
		IsAttacking = false;
		backToPosition = true;
		GetComponent<Animator>().SetInteger("walk",1);

	}
	IEnumerator StopTakeDamagee(){
		yield return new WaitForSeconds (1f);
		GetComponent<Animator>().SetInteger("take",0);

	}

	[Command]
	void Cmdgoserver()
	{
	}
}
