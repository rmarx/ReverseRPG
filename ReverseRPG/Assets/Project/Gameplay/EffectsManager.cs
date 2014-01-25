using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectsManager : LugusSingletonExisting<EffectsManager> 
{
	public GameObject explosion1 = null;

	public void Spawn(GameObject effect, Vector3 position)
	{
		Debug.Log ("Spawning " + effect.name + " @ " + position);
		GameObject effectNew = (GameObject) GameObject.Instantiate( effect );
		effectNew.transform.position = position;
	}

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script
	}
	
	protected void Awake()
	{
		SetupLocal();
	}

	protected void Start () 
	{
		SetupGlobal();
	}
	
	protected void Update () 
	{
	
	}
}
