using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIController : LugusSingletonExisting<UIController> 
{
	public HealthBar healthBar = null;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( healthBar == null )
		{
			GameObject healthBarObj = GameObject.Find ("HealthBar");
			healthBar = healthBarObj.GetComponent<HealthBar>();
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
	
	}
}
