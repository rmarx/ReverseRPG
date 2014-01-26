using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thomb : MonoBehaviour 
{
	public CharacterInteraction character = null;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		character = CharacterInteraction.use;
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

	public bool countingDown = false;
	public float startTime = 0.0f;

	public bool activated = true;

	protected void Update () 
	{
		if( !activated )
			return;

		if( !character.timeCounting )
			return;

		if( Vector3.Distance(transform.position, character.transform.position) < 10.0f )
		{
			if( !countingDown )
			{
				countingDown = true;
				startTime = Time.time;
			}
		}
		else
		{
			countingDown = false;
			startTime = 0.0f;
		}

		if( countingDown && (Time.time - startTime) > 4.0f )
		{
			activated = false;
			character.ChangeHealth( -2000000.0f, GameObject.Find ("DEATH TRIGGER").GetComponent<EnemyTarget>() );
		}
	}
}
