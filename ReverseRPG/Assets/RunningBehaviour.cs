using UnityEngine;
using System.Collections;

public class RunningBehaviour : MonoBehaviour {
	
	public GameObject mainCharacter = null;
	public GameObject runningTeddyBear = null;
	public bool mainCharacterIsClose = false;
	public bool startedRunning = false;	
	
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
		if (mainCharacterIsClose) 
		{
			startedRunning = true;
			startRunningAway ();
			Debug.Log ("Running away!! ");
		}
	}
	
	protected void startRunningAway() 
	{
		//Runs away from target by half of the mainCharacter's speed
		Vector3 relativePos = this.transform.position - mainCharacter.transform.position;
		transform.Translate (relativePos.normalized.x * 40.0f * Time.deltaTime, 0, relativePos.normalized.z * 40.0f * Time.deltaTime);	
	}
}
