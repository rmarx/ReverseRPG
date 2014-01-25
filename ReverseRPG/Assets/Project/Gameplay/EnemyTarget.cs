using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTarget : MonoBehaviour 
{
	public RPG.DamageType damageType = RPG.DamageType.Melee;

	public bool markedForDestruction = false;

	public void OnAttacked()
	{
		EffectsManager.use.Spawn( EffectsManager.use.explosion1, this.transform.position );
	}

	public void OnInteractionDone()
	{
		this.collider.enabled = false;
	}

	public void OnDestroy()
	{
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
