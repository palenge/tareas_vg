using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class graphics : MonoBehaviour {

	public  List<Transform> nodes =  new List<Transform>();
	public  List<conn> vertex =  new List<conn>();
	public  Transform enemy;
	public List<int> road;
	//public List<int> visited;

	public Vector3 mynode;
	public int Destiny;
	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			nodes.Add (child.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float min = 1000000000.0f;
		for(int x = 0 ; x< nodes.Count;x++){
			float dist = Vector3.Distance(nodes[x].position, transform.position);
			if (dist < min) {
				dist = min;
				Destiny = x;
			}
		}
		road.Clear ();

		road = recursive (0, Destiny, new List<int> ());
	}


	public List<int> recursive(int iam, int ddestiny, List<int> visited)
	{
		visited.Add (iam);
		for (int y = 0; y < vertex.Count; y++) {
			if(vertex[iam].ini == ddestiny || vertex[iam].end == ddestiny)
			{ //caso base
				road = visited;
				return  visited;
			}

			if(vertex[y].ini != ddestiny && !visited.Contains(vertex[y].ini)){
				List<int> aux = visited;
				aux.Add(y);
				road.Add (y);
				return recursive (vertex [y].ini, ddestiny, aux);
			}

			if(vertex[y].end != ddestiny && !visited.Contains(vertex[y].end)){
				List<int> aux = visited;
				aux.Add(y);
				road.Add (y);
				return recursive (vertex [y].end, ddestiny, aux);
			}
		}
		return new List<int> ();

	}
}


[System.Serializable]
public class conn
{
	public int ini;
	public int end;
}