using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	Rigidbody rb;
	public Vector3 vec = Vector3.zero;
	public float velocity = 3.0f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		vec.z = Input.GetAxisRaw ("Vertical") * velocity;
		vec.x = Input.GetAxisRaw ("Horizontal") * velocity;
		rb.velocity = vec;	
	}
}
