using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	protected LineRenderer line;
	protected MainSphere MainSphere;
	protected Sphere[] balls;

	public int i = 0;

	// Use this for initialization
	void Start () {

		line = FindObjectOfType<LineRenderer>();
		MainSphere = FindObjectOfType<MainSphere>();
		balls = FindObjectsOfType<Sphere>();
		line.positionCount = 3;

		line.gameObject.SetActive(false);

	}

	// Update is called once per frame
	void Update () {

		// raycast to check certain objects
		RaycastHit hit;
		Ray ray;
		if (Input.touchCount > 0)
		{
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		}
		else
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
			
		var direction = Vector3.zero;

		if (Physics.Raycast(ray, out hit))
		{
			var ballPos = new Vector3(MainSphere.transform.position.x, 0.1f, MainSphere.transform.position.z);
			var mousePos = new Vector3(hit.point.x, 0.1f, hit.point.z);
			//aim line (trajectory after hit off)
			line.SetPosition(0, ballPos);
			line.SetPosition(1, mousePos);
			line.SetPosition(2, mousePos);

			if (hit.collider.tag == "Sphere")
			{
				var dirX = hit.collider.gameObject.transform.position.x + 8*(hit.collider.gameObject.transform.position.x - hit.point.x);
				var dirZ = hit.collider.gameObject.transform.position.z + 8*(hit.collider.gameObject.transform.position.z - hit.point.z);
				var dirPos = new Vector3(dirX, 0.1f, dirZ);

				//trajectory after hit on
				line.SetPosition(2, dirPos);
			}

			else
			{
				line.SetPosition(2, mousePos);
			}
			direction = (mousePos - ballPos).normalized;
		}

		//when you tap on the left side of the screen you can move the white ball
		if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && line.gameObject.activeSelf)
		{
			line.gameObject.SetActive(false);
			MainSphere.GetComponent<Rigidbody>().velocity = direction * 5f;
		}

		if (!line.gameObject.activeSelf && MainSphere.GetComponent<Rigidbody>().velocity.magnitude < 0.3f && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			line.gameObject.SetActive(true);
		}

		if (Input.GetMouseButtonDown(0))
		{
			line.gameObject.SetActive(true);
		}
			
		//game reset
		if (i == 10)
		{
			i = 0;
			MainSphere.ResetBall();
			foreach (var ball in balls)
			{
				ball.gameObject.SetActive(true);
				ball.ResetBall();
			}
		}
	}

	//counter of balls in holes
	public void PlusObj()
	{
		i++;
	}
}