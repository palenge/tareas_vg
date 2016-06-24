using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// esta clase tiene los datos globales d el apalicacion
/// </summary>
public class GameManager  {

	public const string DEFAULT_STATE = "Follow";
	public const string INTERACTIVE_TAG = "interactive";
	public const string ITEM_TAG = "item";
	public const string NPC_TAG = "NPC";
	public static Transform[] LISTADO;


	public const float BOT_SPEED = 8.0f;
	public const float BOT_WANDER_SPACE = 15.5f;

	public const int BOSS_MIN_FOLLOW = 4;
}
