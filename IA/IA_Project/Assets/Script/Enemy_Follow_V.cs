using UnityEngine;
using System.Collections;

public class Enemy_Follow_V : MonoBehaviour {


	public Transform tr;
	public Rigidbody rb;
	public Vector3 dir;
	public float speed = 8.0f;
	public float distance = 2.5f;
	public Vector3 rpoint = Vector3.zero;
	public float space = 15.0f;
	int mode = 0;
	Vector3 aux;
	public int side = 1;
	//public Vector3 currentVelociy = 0.0f;
	// Use this for initialization
	void Start () {
	
		rb = this.gameObject.GetComponent<Rigidbody>();
	}


	public float degreesPerSecond = 45f;
	public float rotationDegreesAmount = 90f;
	private float totalRotation = 0;

	void test2()
	{

		var targetRotation = Quaternion.LookRotation(tr.transform.position - transform.position);

		Vector3 aux = (-tr.transform.forward.normalized + (side * tr.transform.right.normalized)) * 3.0f;
	
		if (Vector3.Distance (tr.transform.position + aux, this.transform.position) > distance * 0.3f) {
			
			targetRotation = Quaternion.LookRotation (tr.transform.position + aux - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime*4);
			rb.velocity = transform.forward.normalized * 3.0f * (Vector3.Distance (tr.transform.position + aux, this.transform.position) / 2.0f);
			//rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);

		}
		else {
			targetRotation = Quaternion.LookRotation ((tr.transform.position) - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
			rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
		}
		
	}

	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.E)) {
//			mode = (mode + 1) % 5;
//			dir = Vector3.zero;
//			if(mode == 4)
//				rpoint = new Vector3 (Random.Range (-space, space), 0,Random.Range (-space, space));
//		}
		//this.transform.position = Vector3.Lerp (transform.position, tr.position, Time.deltaTime); 

		//var targetPoint = targetPosition.transform.position;
		//Vector3 targetRotation = Quaternion.LookRotation(tr.transform.position - this.transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
		dir = tr.transform.position - this.transform.position;
		test2();

	}

}
