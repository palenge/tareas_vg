using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmorCounter : MonoBehaviour {

	public List<GameObject> Enemies;
	public int count;

	// Use this for initialization
	void Start () {
		foreach (Transform  child in transform)
			Enemies.Add (child.gameObject);
		count = Enemies.Count;
		StartCoroutine(Change());
	}

	IEnumerator Change(){
		while (true) {
			yield return new WaitForSeconds (3.0f);
			if (count != transform.childCount) {
				Enemies.Clear ();
				foreach (Transform  child in transform)
					Enemies.Add (child.gameObject);
			}
		}
	}
}
