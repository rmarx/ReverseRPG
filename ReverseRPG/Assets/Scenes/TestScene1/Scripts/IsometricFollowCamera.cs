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
		this.transform.position = new Vector3(target.transform.position.x - originalOffset.x, transform.position.y, target.transform.position.z - originalOffset.z );
	}
}
