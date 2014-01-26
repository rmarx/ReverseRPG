using UnityEngine;
using System.Collections;

public class StartingScript : MonoBehaviour {
	public GameObject teddyBear;
	public GameObject runningTeddyBear;
	public GameObject teddyBearHole;
	public GameObject teddyBearHolesContainer;

	public int numberOfTeddyBears = 5;
	public int numberOfRunningTeddyBears = 5;
	public int numberOfTeddyBearHoles = 5;

	// Use this for initialization
	void Awake () {
		if (teddyBear == null) 
		{
			teddyBear = GameObject.Find ("Teddybear");
		}
		if (runningTeddyBear == null) 
		{
			runningTeddyBear = GameObject.Find ("RunningTeddybear");
		}
		if (teddyBearHole == null) 
		{
			teddyBearHole = GameObject.Find ("TeddybearHole");
		}
		if (teddyBearHolesContainer == null) 
		{
			teddyBearHolesContainer = GameObject.Find ("TeddybearHolesContainer");
		}

		//instantiate X Teddybears
		for (int i = 0; i < numberOfTeddyBears; i++) 
		{
		 	Vector3 position = new Vector3(Random.Range(-13, 13), 0, Random.Range(-13, 13));
			Vector3 relativePos = new Vector3(teddyBear.transform.position.x + position.x, 0, teddyBear.transform.position.z + position.z);
		 	GameObject tempTeddybear = (GameObject) GameObject.Instantiate (teddyBear, relativePos, Quaternion.identity);	 	
		 	tempTeddybear.transform.parent = this.transform.parent;
		}
		GameObject.Destroy (teddyBear);
		//instantiate X Running Teddybears
		for (int i = 0; i < numberOfTeddyBears; i++) 
		{
			Vector3 position = new Vector3(Random.Range(-13, 13), 0, Random.Range(-13, 13));
			Vector3 relativePos = new Vector3(runningTeddyBear.transform.position.x + position.x, 0, runningTeddyBear.transform.position.z + position.z);
			GameObject tempTeddybear = (GameObject) GameObject.Instantiate (runningTeddyBear, relativePos, Quaternion.identity);	 	
			tempTeddybear.transform.parent = this.transform.parent;
		}
		GameObject.Destroy (runningTeddyBear);
		for (int i = 0; i < numberOfTeddyBearHoles; i++) 
		{
			Vector3 position = new Vector3(Random.Range(-13, 13), 0, Random.Range(-13, 13));
			Vector3 relativePos = new Vector3(teddyBearHole.transform.position.x + position.x, 0, teddyBearHole.transform.position.z + position.z);
			GameObject tempTeddybearHole = (GameObject) GameObject.Instantiate(teddyBearHole, relativePos, Quaternion.identity);
			tempTeddybearHole.transform.parent = teddyBearHolesContainer.transform;
		}
		GameObject.Destroy (teddyBearHole);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
