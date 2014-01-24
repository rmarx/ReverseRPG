﻿using UnityEngine;
using System.Collections;
using System;

public interface ILugusCoroutineHandle
{
	bool Running { get; set; }
	bool Claimed { get; set; }
	
	//void StartRoutine( IEnumerator routine ); 
	Coroutine StartRoutine( IEnumerator routine );
	void StartRoutineDelayed( IEnumerator routine, float delay );
	
	void StopRoutine();
	void StopRoutineDelayed(float delay);
	
	void Claim();
	void Claim(string name);
	void Release();
}

[Serializable]
public class LugusCoroutineHandleDefault : MonoBehaviour, ILugusCoroutineHandle
{
	[SerializeField] // NOTE: this is just to make it show up in the inspector. Can be removed without problem.
	protected int _routineCount = 0; 
	public int RoutineCount
	{
		get{ return _routineCount; }
	}
	
	public bool Running
	{
		get{ return (_routineCount != 0); }
		set{}
	}
	
	[SerializeField] // NOTE: this is just to make it show up in the inspector. Can be removed without problem.
	protected bool _claimed = false;
	public bool Claimed
	{
		get{ return _claimed; }
		set{}
	}
	
	/*
	public void StartRoutine( IEnumerator subject )
	{
		if( subject == null ) 
		{
			Debug.LogError("LugusCoroutineHandle : method for coroutine was null!");
			return;	
		}
		
		// TODO: check if _helper.Running is still false
		// if not: get a new Helper (from the Singleton maybe?) and start it on that?
		// as long as this is not fixed, and 2 routines are running on the same helper, Stop() will stop both routines!
		// Note though: this might be expected behaviour... ex. launching multiple routines that belong together on 1 handle...
		// possibly handle that with a flag? or even separate Singleton interface? (ex. GetHandle vs GetGroupHandle)
		
		if( this.Running && !_claimed )
		{
			Debug.LogWarning("LugusCoroutineHandle : there was already a routine running on this un-claimed Handle! Starting new one anyway...");
		}
		
		this.StartCoroutine( RoutineRunner(subject) );
	}
	*/

	
	public Coroutine StartRoutine( IEnumerator subject )
	{
		if( subject == null ) 
		{
			Debug.LogError("LugusCoroutineHandle : method for coroutine was null!");
			return null;	
		}
		
		// TODO: check if _helper.Running is still false
		// if not: get a new Helper (from the Singleton maybe?) and start it on that?
		// as long as this is not fixed, and 2 routines are running on the same helper, Stop() will stop both routines!
		// Note though: this might be expected behaviour... ex. launching multiple routines that belong together on 1 handle...
		// possibly handle that with a flag? or even separate Singleton interface? (ex. GetHandle vs GetGroupHandle)
		
		if( this.Running && !_claimed )
		{
			Debug.LogWarning("LugusCoroutineHandle : there was already a routine running on this un-claimed Handle! Starting new one anyway...");
		}
		
		return this.StartCoroutine( RoutineRunner(subject) );
	}

	
	public void StartRoutineDelayed( IEnumerator subject, float delay )
	{
		StartCoroutine( StartRoutineDelayedRoutine(subject, delay) );
	}
	
	protected IEnumerator StartRoutineDelayedRoutine( IEnumerator subject, float delay )
	{
		yield return new WaitForSeconds( delay );
		
		StartRoutine( subject );
	}
	
	public void StopRoutine()
	{
		StopAllCoroutines();
		
		_routineCount = 0;
	}
	
	public void StopRoutineDelayed(float delay)
	{
		StartCoroutine( StopRoutineDelayedRoutine(delay) );
	}
	
	protected IEnumerator StopRoutineDelayedRoutine(float delay)
	{
		yield return new WaitForSeconds( delay );
		
		StopRoutine();
	}
	
	
	
	public IEnumerator RoutineRunner( IEnumerator routine )
	{	
		
		_routineCount++;
		
		yield return StartCoroutine( routine );
		
		_routineCount--;
		
		if( _routineCount < 0 )
		{
			Debug.LogError(name + " : routineCount was < 0! " + _routineCount);
		}
	}
	
	
	void Awake()
	{
		_routineCount = 0;
		_claimed = false;
	}
	
	public void Claim()
	{
		_claimed = true;
	}
	
	public void Claim(string name)
	{
		Claim ();
		this.transform.name = name;
	}
	
	public void Release()
	{
		_claimed = false;
	}
}
