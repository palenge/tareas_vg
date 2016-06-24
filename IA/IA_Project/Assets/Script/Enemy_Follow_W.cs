using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Follow_W : MonoBehaviour {

	//public static List<GameObject> enemies = new List<GameObject> ();
	public static float offset = 0.0f;
	public Transform tr;

	public Transform enemytransform;
	public Rigidbody rb;
	public Vector3 dir;
	public float speed = 8.0f;
	public float distance = 2.5f;
	public Vector3 rpoint = Vector3.zero;
	public float space = 15.0f;
	public float outforce = 3.0f;public float inforce = 3.0f;
	public float enemyforce = 3.0f;
	int mode = 0;
	Vector3 aux;
	public int side = 1;
	public int id = 0;
	//public Vector3 currentVelociy = 0.0f;
	// Use this for initialization
	void Start () {
		//Enemy_Follow_C.enemies.Add (this.gameObject);
		id = Enemy_Follow_C.enemies.Count;
		rb = this.gameObject.GetComponent<Rigidbody>();
	}


	public float degreesPerSecond = 45f;
	public float rotationDegreesAmount = 90f;
	private float totalRotation = 0;

	void test2()
	{

		var targetRotation = Quaternion.LookRotation(tr.transform.position - transform.position);
		Enemy_Follow_C.offset = Enemy_Follow_C.offset + 0.05f;//Vector3 aux = (-tr.transform.forward.normalized + (side * tr.transform.right.normalized)) * 3.0f;
		float aux1 = Mathf.Sin(Mathf.Deg2Rad * ((360.0f / Enemy_Follow_C.enemies.Count * id)));
			float aux2 = Mathf.Cos(Mathf.Deg2Rad * ((360.0f / Enemy_Follow_C.enemies.Count * id)));
		Vector3 aux = (tr.transform.position + (new Vector3(aux1,0,aux2)*2.0f)); 



		if (Vector3.Distance (aux, this.transform.position) > distance * 0.2f && (Vector3.Distance (enemytransform.position, this.transform.position) > distance * 1.0f)) {
			targetRotation = Quaternion.LookRotation (aux - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
			rb.velocity = transform.forward.normalized * 3.0f;
		
		} else {
			
			if (Vector3.Distance (enemytransform.position, this.transform.position) < distance * 1.0f) {
				targetRotation = Quaternion.LookRotation ((enemytransform.transform.position) - transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				if(Vector3.Distance (enemytransform.position, this.transform.position) > distance * 0.9f)
					rb.velocity = transform.forward.normalized * 2.0f;
				else
					rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			} else {
				targetRotation = Quaternion.LookRotation (-(tr.transform.position) + transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
			}

		}
			//if (Vector3.Distance (tr.transform.position, this.transform.position) > distance * 0.3f) {
			
//		rb.velocity = transform.forward.normalized * inforce * (Vector3.Distance (tr.transform.position + aux, this.transform.position) / 2.0f);
//		rb.velocity -= transform.forward.normalized * outforce * (1.0f/Vector3.Distance (tr.transform.position + aux, this.transform.position));
//
//		foreach (var p in enemies) {
//			if(p.name != this.name)
//				rb.velocity -=  ((tr.transform.position - this.transform.position).normalized) * ((enemyforce *1.0f) / ( (tr.transform.position - this.transform.position).magnitude ));
//		} 
//
//		//if (rb.velocity.magnitude > 1.3f) {
//			targetRotation = Quaternion.LookRotation (tr.transform.position + aux - transform.position);
//			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime * 4);
//		//}
//
//
//
//			//rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
//
////		}
////		else {
////			targetRotation = Quaternion.LookRotation ((tr.transform.position) - transform.position);
////			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
////			rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
//		}
//		
	}

	// Update is called once per frame
	void Update () {

		dir = tr.transform.position - this.transform.position;
		test2();

	}

}
