using UnityEngine;
using System.Collections;

public class Enemy_Follow : MonoBehaviour {


	public Transform tr;
	public Player_Movement pv;
	public Rigidbody rb;
	public Vector3 dir;
	public float speed = 8.0f;
	public float distance = 2.5f;
	public Vector3 rpoint = Vector3.zero;
	public float space = 15.0f;
	int mode = 0;
	Vector3 aux;
	//public Vector3 currentVelociy = 0.0f;
	// Use this for initialization
	void Start () {
	
		rb = this.gameObject.GetComponent<Rigidbody>();
	}


	public float degreesPerSecond = 45f;
	public float rotationDegreesAmount = 90f;
	private float totalRotation = 0;

	void test(){

		float currentAngle = transform.rotation.eulerAngles.y;
		transform.rotation = 
			Quaternion.AngleAxis(currentAngle + (Time.deltaTime * degreesPerSecond), transform.up);
		totalRotation += Time.deltaTime * degreesPerSecond;
	}


	void test2()
	{

		var targetRotation = Quaternion.LookRotation(tr.transform.position - transform.position);

		// Smoothly rotate towards the target point.

		switch (mode) {
		case 0:
			targetRotation = Quaternion.LookRotation (tr.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
			if (Vector3.Distance (tr.transform.position, this.transform.position) > distance * 3.0f)
				rb.velocity = transform.forward.normalized * 3.0f;
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;
		case 1:
			//targetRotation = Quaternion.LookRotation (-tr.transform.position + transform.position);
			//transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
			if (Vector3.Distance (tr.transform.position, this.transform.position) < distance * 2.5f) {
				targetRotation = Quaternion.LookRotation (-tr.transform.position + transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				rb.velocity = transform.forward.normalized * 3.0f;
			} else {
				targetRotation = Quaternion.LookRotation (tr.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			
			}
			break;
		case 2:
			aux = pv.vec.magnitude < 0.1f ? aux : pv.vec.normalized;
			targetRotation = Quaternion.LookRotation ((tr.transform.position + (aux * 3.5f)) - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed + 3.5f * Time.deltaTime);
			if (Vector3.Distance (tr.transform.position, this.transform.position) > distance)
				rb.velocity = transform.forward.normalized * 3.0f;
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;
		
		case 3:
		//targetRotation = Quaternion.LookRotation (-tr.transform.position + transform.position);
		//transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
			aux = pv.vec.magnitude < 0.1f ? aux : pv.vec.normalized;
			if (Vector3.Distance (tr.transform.position, this.transform.position) < distance * 2.5f) {
				targetRotation = Quaternion.LookRotation (-(tr.transform.position + (aux * 3.5f)) + transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				rb.velocity = transform.forward.normalized * 3.0f;
			} else {
				targetRotation = Quaternion.LookRotation ((tr.transform.position + (aux * 3.5f)) - transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);

			}
			break;
		case 4:
			targetRotation = Quaternion.LookRotation (rpoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime*.25f);
			if (Vector3.Distance (rpoint, this.transform.position) > distance)
				rb.velocity = transform.forward.normalized * 3.0f;
			else
				rpoint = new Vector3 (Random.Range (-space, space), 0,Random.Range (-space, space));
			break;
		}
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			mode = (mode + 1) % 5;
			dir = Vector3.zero;
			if(mode == 4)
				rpoint = new Vector3 (Random.Range (-space, space), 0,Random.Range (-space, space));
		}
		//this.transform.position = Vector3.Lerp (transform.position, tr.position, Time.deltaTime); 

		//var targetPoint = targetPosition.transform.position;
		//Vector3 targetRotation = Quaternion.LookRotation(tr.transform.position - this.transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
		dir = tr.transform.position - this.transform.position;
		test2();
		/*

		switch (mode) {
		case 0:
			//Caso Chase
			dir = tr.transform.position - this.transform.position;
			if (Vector3.Distance (tr.transform.position, this.transform.position) > distance)
				rb.velocity = Vector3.Lerp (rb.velocity, dir.normalized * speed, Time.deltaTime * 5);
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;
		case 1:
			//Caso Chase
			dir = tr.transform.position - this.transform.position ;
			if (Vector3.Distance (tr.transform.position, this.transform.position) < distance *2.5f)
				rb.velocity = Vector3.Lerp (rb.velocity, -dir.normalized * speed, Time.deltaTime * 5);
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;
		case 2:
			aux = pv.vec.magnitude < 0.1f ? aux : pv.vec.normalized;

			dir = (tr.transform.position + (aux * 4.5f)) - this.transform.position ;
			if (Vector3.Distance (tr.transform.position, this.transform.position) > distance)
				rb.velocity = Vector3.Lerp (rb.velocity, dir.normalized * speed * 3, Time.deltaTime * 5);
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;
		case 3:
			dir = (tr.transform.position + (pv.vec.normalized * 12.5f)) - this.transform.position ;
			if (Vector3.Distance (tr.transform.position, this.transform.position) < distance)
				rb.velocity = Vector3.Lerp (rb.velocity, -dir.normalized * speed, Time.deltaTime * 5);
			else
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			break;

		case 4:
			dir = rpoint - this.transform.position;
			if (Vector3.Distance (rpoint, this.transform.position) > distance)
				rb.velocity = Vector3.Lerp (rb.velocity, dir.normalized * speed, Time.deltaTime * 5);
			else
				rpoint = new Vector3 (Random.Range (-space, space), 0,Random.Range (-space, space));
			break;


		}

*/
		//Obtain direction vector
	
			

	}


}
