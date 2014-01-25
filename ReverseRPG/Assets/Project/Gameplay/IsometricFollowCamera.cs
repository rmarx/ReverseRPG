using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsometricFollowCamera : MonoBehaviour 
{
	public GameObject target = null;

	public Vector3 originalOffset = Vector3.zero;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		originalOffset = target.transform.position - this.transform.position;

		if (target == null) 
		{
			target = GameObject.Find ("Character");
		}
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
		Vector3 targetPos = new Vector3(target.transform.position.x - originalOffset.x, transform.position.y, target.transform.position.z - originalOffset.z );

		//this.transform.position = Vector3.Lerp( this.transform.position, targetPos, 0.3f );
		
		this.transform.position = Vector3.Lerp( this.transform.position, targetPos, 50.0f * Time.deltaTime );
	}
}
