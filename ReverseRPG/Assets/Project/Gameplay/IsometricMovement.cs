using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsometricMovement : MonoBehaviour 
{
	public float forwardSpeed = 20.0f;
	public float sideWaysSpeed = 30.0f; 

	public float minX = -20.0f;
	public float maxX = 20.0f;

	public float minZ = -30.0f;

	public bool moveEnabled = false;
	public bool freeMove = false;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script

		//LugusCoroutines.use.StartRoutine( StartMoveRoutine() );
		
		Animator[] anims = transform.GetComponentsInChildren<Animator>();
		foreach( Animator anim in anims )
		{
			if( anim != null )
				anim.enabled = false;
		}
	}

	public void StartMoving()
	{
		Animator[] anims = transform.GetComponentsInChildren<Animator>(); 
		foreach( Animator anim in anims )
		{
			if( anim != null )
				anim.enabled = true;
		}

		moveEnabled = true;
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
		if( moveEnabled )
			Move ();
	}

	protected void Move()
	{
		Vector3 originalPosition = transform.position;
		
		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");

		if( freeMove )
		{
			zMove = Input.GetAxis("Vertical");
			
			Animator anim = transform.GetComponentInChildren<Animator>();
			if( zMove == 0.0f && xMove == 0.0f )
			{
				anim.enabled = false;
			}
			else
			{
				anim.enabled = true;
			}
		}

		Move ( xMove, zMove );

		
		if( transform.position.x < minX )
		{
			transform.position = originalPosition.x (minX);
			Move ( 0.0f, 1.0f );
		}
		
		if( transform.position.x > maxX )
		{
			transform.position = originalPosition.x ( maxX );
			Move ( 0.0f, 1.0f );
		}

		if( transform.position.z < minZ )
		{
			transform.position = originalPosition.z( minZ );
			Move ( xMove, 0.0f );
		}
	}
	
	protected void Move(float xMove, float zMove)
	{
		Vector3 move = new Vector3( xMove , 0, zMove );

		//Debug.Log ("MOVE " + move + " // "+ Vector3.ClampMagnitude(move, 1) );

		move = Vector3.ClampMagnitude(move, 1);
		move = move.xMul(sideWaysSpeed * Time.deltaTime).zMul(forwardSpeed * Time.deltaTime);
		
		//transform.position += Vector3.Scale( move, transform.forward );
		
		transform.Translate( move, Space.Self );
	}

	/*
	protected void Move()
	{
		Vector3 originalPosition = transform.position;
		
		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");
		
		// when moving right, the move feels much slower for some reason...
		if( xMove != 0.0f )
		{
			xMove *= 0.7071f;
			zMove *= 0.7071f;
		}
		
		//if( xMove < 0.0f )
		//	xMove *= 0.7f;
		
		//if( xMove < 0.0f )
		//	xMove *= 0.5f;
		
		// moveSpeed /2.0f for x because we have a 2:1 aspect ratio in isometric
		
		Move ( xMove, zMove );
		
		
		if( transform.position.x < minX )
		{
			transform.position = originalPosition.x (minX);
			Move ( 0.0f, 1.0f );
		}
		
		if( transform.position.x > maxX )
		{
			transform.position = originalPosition.x ( maxX );
			Move ( 0.0f, 1.0f );
		}
		
		
	}
	
	protected void Move(float xMove, float zMove)
	{
		Vector3 move = new Vector3( xMove , 0, zMove );
		//Debug.Log ("MOVE " + move + " // "+ Vector3.ClampMagnitude(move, 1) );
		//move = Vector3.ClampMagnitude(move, 1);
		move = move.xMul(sideWaysSpeed * Time.deltaTime).zMul(forwardSpeed * Time.deltaTime);
		
		//transform.position += Vector3.Scale( move, transform.forward );
		
		transform.Translate( move, Space.Self );
	}


	protected void Move()
	{
		// eerst x, dan y

		Vector3 originalPosition = transform.position;
		
		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");
		
		//if( xMove < 0.0f )
		//	xMove *= 0.5f;
		
		// moveSpeed /2.0f for x because we have a 2:1 aspect ratio in isometric
		
		//Move ( xMove, zMove );

		
		Vector3 move = new Vector3( xMove , 0, 0 );
		//Debug.Log ("MOVE " + move + " // "+ Vector3.ClampMagnitude(move, 1) );
		//move = Vector3.ClampMagnitude(move, 1);
		move = move.xMul(sideWaysSpeed * Time.deltaTime);
		
		//transform.position += Vector3.Scale( move, transform.forward );

		transform.localPosition += move;
		//transform.Translate( move, Space.Self );

		move = new Vector3( 0, 0, zMove );
		move = move.zMul(forwardSpeed * Time.deltaTime);

		
		transform.localPosition += move;
		//transform.Translate( move, Space.Self );

	}

	protected void Move()
	{
		Vector3 originalPosition = transform.position;
		
		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");

		//if( xMove < 0.0f )
		//	xMove *= 0.5f;
		
		// moveSpeed /2.0f for x because we have a 2:1 aspect ratio in isometric
		
		Move ( xMove, zMove );
		
		
		if( transform.position.x < minX )
		{
			transform.position = originalPosition.x (minX);
			Move ( 0.0f, 1.0f );
		}
		
		if( transform.position.x > maxX )
		{
			transform.position = originalPosition.x ( maxX );
			Move ( 0.0f, 1.0f );
		}
		
		
	}
	
	protected void Move(float xMove, float zMove)
	{
		Vector3 move = new Vector3( xMove , 0, zMove );
		//Debug.Log ("MOVE " + move + " // "+ Vector3.ClampMagnitude(move, 1) );
		//move = Vector3.ClampMagnitude(move, 1);
		move = move.xMul(sideWaysSpeed * Time.deltaTime).zMul(forwardSpeed * Time.deltaTime);
		
		//transform.position += Vector3.Scale( move, transform.forward );
		
		transform.Translate( move, Space.Self );
	}
	*/

	/*
	protected void Move()
	{
		Vector3 originalPosition = transform.position;

		float xMove = Input.GetAxis("Horizontal");
		float zMove = 1.0f; //Input.GetAxis("Vertical");

		// when moving right, the move feels much slower for some reason...
		if( xMove != 0.0f )
		{
			xMove *= 0.7071f;
			zMove *= 0.7071f;
		}

		//if( xMove < 0.0f )
		//	xMove *= 0.7f;

		//if( xMove < 0.0f )
		//	xMove *= 0.5f;

		// moveSpeed /2.0f for x because we have a 2:1 aspect ratio in isometric

		Move ( xMove, zMove );


		if( transform.position.x < minX )
		{
			transform.position = originalPosition.x (minX);
			Move ( 0.0f, 1.0f );
		}
		
		if( transform.position.x > maxX )
		{
			transform.position = originalPosition.x ( maxX );
			Move ( 0.0f, 1.0f );
		}


	}

	protected void Move(float xMove, float zMove)
	{
		Vector3 move = new Vector3( xMove , 0, zMove );
		//Debug.Log ("MOVE " + move + " // "+ Vector3.ClampMagnitude(move, 1) );
		//move = Vector3.ClampMagnitude(move, 1);
		move = move.xMul(sideWaysSpeed * Time.deltaTime).zMul(forwardSpeed * Time.deltaTime);
		
		//transform.position += Vector3.Scale( move, transform.forward );
		
		transform.Translate( move, Space.Self );
	}
	*/

}
