﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeddyGrizzlies : MonoBehaviour 
{
	public GameObject grizzlies = null;
	public GameObject teddys = null;

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
