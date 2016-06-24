using UnityEngine;
using System.Collections;

public interface iStateHolder  
{
	void UpdateState(); //actualizaciones

	void OnTriggerEnter (Collider other); // cuando ocurran colisiones con el bounding

	void ChangeState (string id); //cambiar al estado con nombre id

	void onPreLoad(); // lo que debe hacer antes de pasar a este estado
}


