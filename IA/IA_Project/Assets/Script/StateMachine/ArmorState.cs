using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmorState : iStateHolder {

	EnemyBehavior myself;
	Vector3 actual;
	public Vector3 rpoint = Vector3.zero;

	public float distance = 2.5f;

	public ArmorState (EnemyBehavior statePatternEnemy)
	{
		myself = statePatternEnemy;
		//points = new Transform[GameManager.LISTADO.] ();
	}

	/// <summary>
	/// Caso que ocurre cada frame 
	/// </summary>
	public void UpdateState()
	{
		Follow ();
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 1.0f;
	}


	/// <summary>
	/// En este caso hace validaciones apra saber a que estado moverse si es necesario , al estado look, estado interactive o estado de hablar 
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnTriggerEnter (Collider other)
	{

	}

	public void ChangeState (string id)
	{
		if (myself.States.ContainsKey (id)) {
			Debug.Log ("Entrado en Estado: " + id);
			myself.States [id].onPreLoad ();
			myself.currentState = myself.States [id]; // la magia de la interfaz gracias a esta linea el mismo dice y cambia al estado que debe empezar a funcionar (me parece magico xD)
		} else { // ahora bien si no consigue el estado solicitado regresa a un estado default en caso de necesitar escalarlo
			Debug.Log ("Estado no valido, volviendo al estado original");
			myself.States[GameManager.DEFAULT_STATE].onPreLoad ();
			myself.currentState = myself.States[GameManager.DEFAULT_STATE];
		}
	}




	void Follow(){
//		var targetRotation = Quaternion.LookRotation (myself.ArmorTarget - myself.transform.position);
//		if (Vector3.Distance (myself.ArmorTarget, myself.transform.position) > distance * 0.2f && (Vector3.Distance ( myself.ArmorTarget, myself.transform.position) > distance * 1.0f)) {
//			targetRotation = Quaternion.LookRotation (aux - transform.position);
//			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
//			rb.velocity = transform.forward.normalized * 3.0f;
//
//		} else {
//
//			if (Vector3.Distance (enemytransform.position, this.transform.position) < distance * 1.0f) {
//				targetRotation = Quaternion.LookRotation ((enemytransform.transform.position) - transform.position);
//				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
//				if(Vector3.Distance (enemytransform.position, this.transform.position) > distance * 0.9f)
//					rb.velocity = transform.forward.normalized * 2.0f;
//				else
//					rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
//			} else {
//				targetRotation = Quaternion.LookRotation (-(tr.transform.position) + transform.position);
//				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
//				rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, Time.deltaTime * 5);
//			}
//
//		}
	
		var targetRotation = Quaternion.LookRotation (myself.ArmorTarget - myself.transform.position);
		myself.transform.rotation = Quaternion.Slerp (myself.transform.rotation, targetRotation, GameManager.BOT_SPEED * Time.deltaTime*1.5f);
		if (Vector3.Distance (myself.ArmorTarget, myself.transform.position) > distance * 0.2f)
			myself.rb.velocity = myself.transform.forward.normalized * 3.0f;
		else
			myself.rb.velocity = Vector3.zero;
		
	}
	//funcion de patrol utilizada para moverse
//	void Patrol ()
//	{
//		var targetRotation = Quaternion.LookRotation (rpoint -  myself.transform.position);
//		myself.transform.rotation = Quaternion.Slerp (myself.transform.rotation, targetRotation, GameManager.BOT_SPEED * Time.deltaTime*.25f);
//		if (Vector3.Distance (rpoint, myself.transform.position) > distance)
//			myself.rb.velocity = myself.transform.forward.normalized * 3.0f;
//		else
//			rpoint = new Vector3 (Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE), 0,Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE));
//	}

	/// <summary>
	/// Son los pasos que deebn ocurri antes de entrar a un estado
	/// </summary>
	public void onPreLoad()
	{
		myself.rend.material.color = Color.yellow;
		rpoint = new Vector3 (Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE), 0,Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE));

		//actual = GameManager.LISTADO[points[Random.Range(0, points.Count)]].position; // selecciona un punto pseudo aleatorio que sera su destino 
		Debug.Log ("Me voy a mover para algun lado!!");	
	}

}
