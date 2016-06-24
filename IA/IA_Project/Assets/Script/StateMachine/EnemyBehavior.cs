using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour {


	public Dictionary<string, iStateHolder> States = new Dictionary<string, iStateHolder>(); // listado key value de los estados 


	public SphereCollider bc;  // caja de collision
	public Renderer rend; // componente de renderizado de la capsula
	public Transform transform;
	public Rigidbody rb;

	public Transform target;

	public iStateHolder currentState;


	///BOSS ZONE
	public bool isBoss = false;
	public List<EnemyBehavior> Enemies;
	public int count;
	public Transform EnemyHolder;
	public Vector3 ArmorTarget;

	// Use this for initialization

	/// <summary>
	/// se anaden todos los estados de esta maquina
	/// </summary>
	void Start () {
		States.Add ("Wander", new WanderState (this));
		States.Add ("Follow", new FollowState (this));
		States.Add ("Flee", new FleeState (this));
		States.Add ("Armor", new ArmorState (this));
//		States.Add ("Wait", new WaitState (this));
//		States.Add ("Interactive", new InteractiveState (this));
//		States.Add ("Talking", new TalkState (this));

		//States[GameManager.DEFAULT_STATE].onPreLoad(); 
		currentState = States[GameManager.DEFAULT_STATE]; // se asigna el estado inicial 

		bc = this.GetComponent<SphereCollider> ();
		rend = this.GetComponent<Renderer> ();
		rb = this.GetComponent<Rigidbody> ();
		transform = gameObject.transform;

		States[GameManager.DEFAULT_STATE].onPreLoad(); //se realiza su estaod preload del estado inicial


		target = GameObject.Find("Player").transform;
		EnemyHolder = GameObject.Find("Enemies_Holder").transform;

		if (isBoss) {
			foreach (Transform child in EnemyHolder) {
				Enemies.Add (child.gameObject.GetComponent<EnemyBehavior> ());
				//child.gameObject.GetComponent<EnemyBehavior> ().ChangeRemote ("Armor");
			}
			count = Enemies.Count;
			StartCoroutine (Change ());
		}
		//rend.material.color = Color.blue;
		//gameObject.renderer.Material.Color = new Color (1, 0, 0);
	}


	IEnumerator Change(){
		while (true) {
			yield return new WaitForSeconds (3.0f);
			if (count != EnemyHolder.childCount) {
				Enemies.Clear ();
				foreach (Transform child in EnemyHolder)
				{
					Enemies.Add (child.gameObject.GetComponent<EnemyBehavior> ());
					child.gameObject.GetComponent<EnemyBehavior> ().ChangeRemote ("Armor");
				}
				count = Enemies.Count;
			}
		}
	}

	/// Update is called once per frame
	void Update () {
		currentState.UpdateState ();// se llama al update del estado actual
		if(Input.GetKey(KeyCode.Q) && !isBoss)
			ChangeRemote("Armor");
		if(Input.GetKey(KeyCode.R) && !isBoss)
			ChangeRemote("Follow");
	}


	public void ChangeRemote(string id){
		if (States.ContainsKey (id)) {
		currentState = States[id]; // se asigna el estado inicial 
		States[id].onPreLoad(); //se realiza su estaod preload del estado inicial
		} else { // ahora bien si no consigue el estado solicitado regresa a un estado default en caso de necesitar escalarlo
			Debug.Log ("Estado no valido, volviendo al estado original");
			States[GameManager.DEFAULT_STATE].onPreLoad ();
			currentState = States[GameManager.DEFAULT_STATE];
		}

	}

	/// <summary>
	/// ontrigger enter que llama al obtrigger enter del estado actual
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		currentState.OnTriggerEnter (other);
	}

	public void disableTrigger (){
		bc.enabled = false;
		StartCoroutine (ReactivateTriggers ());
	}

	IEnumerator ReactivateTriggers()
	{
		yield return new WaitForSeconds (4.0f);
		bc.enabled = true;
	}
}
