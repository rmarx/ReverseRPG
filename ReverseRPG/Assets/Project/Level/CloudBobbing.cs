using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudBobbing : MonoBehaviour 
{
	public void SetupLocal()
	{
		// assign variables that have to do with this class only
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script

		Vector3 target = this.transform.position + new Vector3( Random.Range(-3, 3), Random.Range(0,1), Random.Range(0,3) );
		this.gameObject.MoveTo( target ).Time ( Random.Range(4, 5) ).LoopType(iTween.LoopType.pingPong).Delay ( Random.Range(0,1) ).EaseType(iTween.EaseType.linear).Execute();

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
