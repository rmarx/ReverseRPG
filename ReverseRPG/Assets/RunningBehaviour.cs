using UnityEngine;
using System.Collections;

public class RunningBehaviour : MonoBehaviour {
	
	public GameObject mainCharacter = null;
	public GameObject runningTeddyBear = null;
	public bool mainCharacterIsClose = false;
	public bool startedRunning = false;

	public float runSpeed = 30.0f;
	
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
		if (runningTeddyBear == null) 
		{
			runningTeddyBear = GameObject.Find ("RunningTeddybear");
		}
		Animator anim = transform.GetComponentInChildren<Animator> ();
		anim.enabled = false;
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
		if (Vector3.Distance (this.transform.position, mainCharacter.transform.position) < 20.0f) 
		{
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
		Animator anim = transform.GetComponentInChildren<Animator> ();
		anim.enabled = true;

		//Runs away from target by half of the mainCharacter's speed
		Vector3 relativePos = this.transform.position - mainCharacter.transform.position;
		if(relativePos.x < 0)
		{
			relativePos.x *= -1;
		
		}
//		Debug.LogWarning (" RUNNER " + relativePos.normalized + " // " + runSpeed);
		transform.Translate (relativePos.normalized.x * runSpeed * Time.deltaTime, 0, relativePos.normalized.z * runSpeed * Time.deltaTime);	
	}
}
