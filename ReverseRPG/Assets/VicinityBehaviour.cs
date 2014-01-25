using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VicinityBehaviour : MonoBehaviour {

	public float moveSpeed = 50.0f;
	public GameObject mainCharacter = null;
	public GameObject teddyBearHolesParent;
	public Transform[] teddyBearHolesChildren;
	public Vector3 directionToMainCharacter;
	public Vector3 directionToMove;
	public bool mainCharacterIsClose = false;
	public bool startedRunning = false;
	public Transform closestHole = null;


	public void SetupLocal()
	{
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script
		//GameObject.FindObjectsOfType<VicinityBehaviour>
		
		// assign variables that have to do with this class only
		if (mainCharacter == null) 
		{
			mainCharacter = GameObject.Find ("Character");
		}
		if (teddyBearHolesParent == null) 
		{
			teddyBearHolesParent = GameObject.Find ("Holes");
			teddyBearHolesChildren = teddyBearHolesParent.transform.GetComponentsInChildren<Transform> ();		
		}
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
		//Checks vicinity of the player to the Teddybear
		if (Vector3.Distance (this.transform.position, mainCharacter.transform.position) < 10f) {
			mainCharacterIsClose = true;
			
		} 
		else 
		{
			mainCharacterIsClose = false;
		}

		//Action when player is close
		if (mainCharacterIsClose && !startedRunning) 
		{
			//Run to the nearest TeddybearHole object
			foreach(Transform hole in teddyBearHolesChildren)
			{
				if( hole == teddyBearHolesParent.transform || hole == null)
				{
					continue;
				}
				if(closestHole == null || Vector3.Distance(this.transform.position, hole.transform.position) < Vector3.Distance(this.transform.position, closestHole.transform.position)) 
				{
					closestHole = hole;
				} 
			}
			if(closestHole != null)
			{
				startedRunning = true;
				RunToHole(closestHole);
			} 
			else
			{
				startedRunning = true;
				startRunningAway();
			}

			//if none present or all are taken, just run away in the direction of the game
			//RunAway ();	
		}
	}

	protected void RunToHole(Transform hole)
	{
		//Move to hole
		Debug.Log("Running to hole wee wee " + hole.name);
		//Vector3 relativePos = this.transform.position - hole.position;
		//this.transform.Translate(relativePos.normalized * 25.0f * Time.deltaTime, Space.World);

		this.gameObject.MoveTo(hole.position).Speed(25.0f).Execute();
	}

	protected void startRunningAway() 
	{
		//Runs away from target by half of the mainCharacter's speed
		Vector3 relativePos = this.transform.position - mainCharacter.transform.position;
		transform.Translate (relativePos.normalized * 25.0f * Time.deltaTime);	
	}

}
