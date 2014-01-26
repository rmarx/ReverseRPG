using UnityEngine;
using System.Collections;

public class AttackBehaviour : MonoBehaviour {

	public float runSpeed = 20.0f;
	public GameObject mainCharacter = null;
	public bool mainCharacterIsClose = false;
	public string actionState = "idle"; // idle -> attacking -> runAway 

	public GameObject idle;
	public GameObject running;

	// Use this for initialization
	void Start () {
		if (mainCharacter == null) 
		{
			mainCharacter = GameObject.Find ("Character");
		}
		running.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//Checks vicinity of the player to the Teddybear
		if (Vector3.Distance (this.transform.position, mainCharacter.transform.position) < 20.0f) {
			mainCharacterIsClose = true;			
		} 
		else 
		{
			mainCharacterIsClose = false;
		}

		if (mainCharacterIsClose && actionState == "idle") 
		{
			Debug.Log("Idle -> attacking");
			actionState = "attacking";
		}

		if (actionState == "attacking") 
		{
			idle.SetActive(false);
			running.SetActive(true);
			//Debug.Log("running to mainchar");
			//Run to the maincharacter
			Vector3 relativePos = mainCharacter.transform.position - this.transform.position;
			transform.Translate (relativePos.normalized.x * runSpeed * Time.deltaTime, 0, relativePos.normalized.z * runSpeed * Time.deltaTime);

			//check if Grizzly is in close vicinity
			if(Vector3.Distance (this.transform.position, mainCharacter.transform.position) <= 5.0f)
			{
				Debug.Log("In vicinity, showing effect and turning to runAway");
				//show claw effect
				EffectsManager.use.Spawn (EffectsManager.use.slashWithText, new Vector3( mainCharacter.transform.position.x+1.0f, mainCharacter.transform.position.y+1.0f, mainCharacter.transform.position.z+1.0f));
				//change state
				actionState = "runAway";
			}

			//if mainchar is advanced too far, back off and switch to runAway
			if(mainCharacter.transform.position.z - this.transform.position.z >= 5.0f)
			{
				actionState = "runAway";
			}
		}
		if (actionState == "runAway") 
		{
			Debug.Log("running away");
			//Runs away from target by half of the mainCharacter's speed
			Vector3 relativePos = this.transform.position - mainCharacter.transform.position;
			if(relativePos.x < 0)
			{
				relativePos.x *= -1.0f;
			}
			transform.Translate( relativePos.normalized.x * runSpeed * Time.deltaTime, 0, relativePos.normalized.z * runSpeed * Time.deltaTime);	
		}
	}
}
