using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTarget : MonoBehaviour 
{
	public bool markedForDestruction = false;

	public void OnAttacked()
	{
		EffectsManager.use.Spawn( EffectsManager.use.explosion1, this.transform.position );
	}

	public void OnDestroy()
	{
		Debug.Log ("Ya man");
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
