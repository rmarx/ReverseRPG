using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaWater : MonoBehaviour 
{
	public Material lavaMaterial = null;

	public void Switch()
	{
		this.renderer.material = lavaMaterial;

		this.GetComponent<EnemyTarget>().enabled = true;
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
