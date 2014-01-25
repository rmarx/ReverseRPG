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
		// assign variables that have to do with this class only
		if (mainCharacter == null) 
		{
			mainCharacter = GameObject.Find ("Character");
		}
		if (teddyBearHolesParent == null) 
		{
			teddyBearHolesParent = GameObject.Find ("TeddybearHolesContainer");
		}
		
		teddyBearHolesChildren = teddyBearHolesParent.transform.GetComponentsInChildren<Transform>();
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
		if (Vector3.Distance (this.transform.position, mainCharacter.transform.position) < 20.0f) {
			mainCharacterIsClose = true;
			
		} 
		else 
		{
			mainCharacterIsClose = false;
		}

		//Action when player is close
		if (mainCharacterIsClose && !startedRunning) 
		{
			Debug.Log ("Badass is close!");
			//Run to the nearest TeddybearHole object
			Debug.Log ("length of holes to run to " + teddyBearHolesChildren.Length);
			foreach (Transform hole in teddyBearHolesChildren) 
			{
				if (hole == teddyBearHolesParent.transform || hole == null) 
				{
					continue;
				}
				if (closestHole == null || Vector3.Distance (this.transform.position, hole.transform.position) < Vector3.Distance (this.transform.position, closestHole.transform.position)) 
				{
					closestHole = hole;
				} 
			}
			if (closestHole != null) 
			{
				startedRunning = true;
				RunToHole (closestHole);
				Debug.Log ("Running to closest hole aaaah!  " + closestHole.name);
			}
		}
	}

	protected void RunToHole(Transform hole)
	{
		this.gameObject.MoveTo(hole.position).Speed(40.0f).Execute();
	}
}
