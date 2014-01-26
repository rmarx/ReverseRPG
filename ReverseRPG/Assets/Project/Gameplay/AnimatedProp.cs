using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatedProp : MonoBehaviour 
{
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

	public void OnTriggerEnter(Collider collider)
	{
		Debug.Log ("START PROPPING!");

		Animator anim = GetComponent<Animator>();
		anim.enabled = true;
	}
}
