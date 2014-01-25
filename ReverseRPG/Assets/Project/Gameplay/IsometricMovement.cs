using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsometricMovement : MonoBehaviour 
{
	public float forwardSpeed = 20.0f;
	public float sideWaysSpeed = 30.0f; 

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
		Move ();
	}

	protected void Move()
	{
		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");

		// when moving right, the move feels much slower for some reason...
		if( xMove > 0.0f )
			xMove *= 1.5f;

		// moveSpeed /2.0f for x because we have a 2:1 aspect ratio in isometric
		Vector3 move = new Vector3( xMove * sideWaysSpeed * Time.deltaTime, 0, zMove * forwardSpeed * Time.deltaTime );
		//transform.position += Vector3.Scale( move, transform.forward );

		transform.Translate( move, Space.Self );


	}
}
