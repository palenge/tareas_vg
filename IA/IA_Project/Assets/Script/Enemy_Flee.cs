using UnityEngine;
using System.Collections;

public class Enemy_Flee : MonoBehaviour {


	public Transform tr;
	public float value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance (this.transform.position, tr.position) > value)
		{
			Vector3 tmp = Vector3.Normalize (this.transform.position - tr.position);


		}
	}
}
