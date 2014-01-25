using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float moveSpeed = 20.0f;
	
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
		float zMove = Input.GetAxis("Vertical");
		
		Vector3 move = new Vector3( xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime );
		//transform.position += Vector3.Scale( move, transform.forward );
		
		transform.Translate( move, Space.Self );
		
		
	}
}
