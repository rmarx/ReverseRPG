using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbitProjectile : MonoBehaviour 
{
	public Transform target = null;
	public float radius = 4.0f; 
	public float speed = 150.0f;
	public float rotationDirection = 1.0f; // 1.0f or -1.0f

	protected Vector3 axis = Vector3.zero;

	protected bool _goForOrbit = true;
	public bool OrbitEnabled
	{
		get{ return _goForOrbit; }
		set{ _goForOrbit = value; }
	}

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( target == null )
			target = GameObject.Find ("Character").transform; 

		axis = Vector3.up;

		RandomProperties();
	}
	
	public void SetupGlobal()
	{
		transform.position = GetOrbitStartPosition();

		LugusCoroutines.use.StartRoutine( PropertyChangeRoutine() );
	}

	public void Attack(EnemyTarget target, float duration)
	{

		LugusCoroutines.use.StartRoutine( RestartOrbitRoutine(target, duration) );
	}

	protected IEnumerator RestartOrbitRoutine(EnemyTarget enemy, float duration)
	{
		this.OrbitEnabled = false;
		float halfDuration = duration / 2.0f;

		iTween.EaseType goEase = iTween.EaseType.easeOutQuad;
		iTween.EaseType backEase = iTween.EaseType.linear;

		this.gameObject.MoveTo( enemy.transform.position ).Time( halfDuration ).EaseType(goEase).Execute();

		// note: use local coordinates here to return to the target
		// this only works if the projectiles are children of the target!!!
		this.gameObject.MoveTo( target.transform.InverseTransformPoint( target.transform.position ) ).IsLocal(true).Time ( halfDuration ).EaseType(backEase).Delay( halfDuration ).Execute();

		yield return new WaitForSeconds( halfDuration );
		if( enemy != null )
		{
			enemy.OnAttacked();
			GameObject.Destroy( enemy.gameObject );
		}
		yield return new WaitForSeconds( halfDuration );


		iTween.Stop( this.gameObject );
		transform.position = GetOrbitStartPosition();

		OrbitEnabled = true;
	}

	public Vector3 GetOrbitStartPosition()
	{
		return (transform.position - target.position).normalized * radius + target.position;
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
		if( _goForOrbit )
			Orbit ();
	}

	protected void Orbit()
	{
		transform.RotateAround (target.position, axis, rotationDirection * speed * Time.deltaTime);

		transform.eulerAngles = new Vector3(45,45,0);

		Vector3 desiredPosition = (transform.position - target.position).normalized * radius + target.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.5f);
	}

	protected void RandomProperties()
	{
		Vector3 targetAxis = new Vector3( new DataRange(-0, 0).Random(), new DataRange(0, 5).Random(), new DataRange(-0,0).Random() );
		//axis.Normalize();
		float targetSpeed = new DataRange( 150, 250 ).Random();
		float targetRadius = new DataRange( 4f, 7f ).Random ();

		LugusCoroutines.use.StartRoutine( PropertyLerpRoutine(targetAxis, targetSpeed, targetRadius) );
	}

	protected IEnumerator PropertyLerpRoutine(Vector3 targetAxis, float targetSpeed, float targetRadius)
	{
		float startTime = Time.time;
		float duration = 2.5f;

		while( Time.time - startTime < duration )
		{
			float timeProgress = duration / (Time.time - startTime);

			axis = Vector3.Lerp( axis, targetAxis, timeProgress );
			speed = Mathf.Lerp( speed, targetSpeed, timeProgress );
			radius = Mathf.Lerp( radius, targetRadius, timeProgress );

			yield return null;
		}

		yield break;
	}

	protected IEnumerator PropertyChangeRoutine()
	{
		float changePeriod = 5.0f;

		while( true )
		{
			//float startTime = Time.time;

			yield return new WaitForSeconds( changePeriod );

			/*
			while( Time.time - startTime < changePeriod )
			{
				yield return null;
			}
			*/

			RandomProperties();
		}

		yield return null;
	}
}
