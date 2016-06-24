using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleeState : iStateHolder {

	EnemyBehavior myself;
	Vector3 actual;
	public Vector3 rpoint = Vector3.zero;

	public float distance = 2.5f;


	public float timetochange = 5.0f;
	public float timeinit = 0.0f;

	public FleeState (EnemyBehavior statePatternEnemy)
	{
		myself = statePatternEnemy;
		//points = new Transform[GameManager.LISTADO.] ();
	}

	/// <summary>
	/// Caso que ocurre cada frame 
	/// </summary>
	public void UpdateState()
	{
		timeinit += Time.deltaTime;
		if (timeinit > timetochange)
			ChangeState ("Wander");
		Flee ();
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


	void Flee()
	{
		var targetRotation = Quaternion.LookRotation (-myself.target.transform.position + myself.transform.position);
		myself.transform.rotation = Quaternion.Slerp (myself.transform.rotation, targetRotation, GameManager.BOT_SPEED * Time.deltaTime*.25f);
		myself.rb.velocity = myself.transform.forward.normalized * 3.0f;
			//rpoint = new Vector3 (Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE), 0,Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE));
	}

	/// <summary>
	/// Son los pasos que deebn ocurri antes de entrar a un estado
	/// </summary>
	public void onPreLoad()
	{
		myself.rend.material.color = Color.black;
		rpoint = new Vector3 (Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE), 0,Random.Range (-GameManager.BOT_WANDER_SPACE, GameManager.BOT_WANDER_SPACE));
		timeinit = 0.0f;
		//actual = GameManager.LISTADO[points[Random.Range(0, points.Count)]].position; // selecciona un punto pseudo aleatorio que sera su destino 
		Debug.Log ("Me voy a escapar!!!");	
	}



}
