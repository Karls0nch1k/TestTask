using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sphere : MonoBehaviour {

	public Vector3 Startpos;
	public bool ResetB;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager>();
		Startpos = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {

		//if the white ball fall into the hole
		if (transform.position.y < -0.01f)
		{
			gm.PlusObj();
			gameObject.SetActive(false);
		}

		//if the game restart
		if (ResetB)
		{
			ResetB = false;
			transform.position = Startpos;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
	public void ResetBall()
	{
		ResetB = true;
	}
}