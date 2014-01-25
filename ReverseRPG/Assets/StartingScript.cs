using UnityEngine;
using System.Collections;

public class StartingScript : MonoBehaviour {
	public GameObject teddyBear;
	public GameObject teddyBearHole;
	public GameObject teddyBearHolesContainer;

	public int numberOfTeddyBears = 5;
	public int numberOfTeddyBearHoles = 5;

	public float startingXOfTeddyBear = -11.0f;
	public float startingYOfTeddyBear = 0.7172546f;
	public float startingZOfTeddyBear = -2.0f;
	public float startingXOfTeddyBearHole = -32.0f;
	public float startingYOfTeddyBearHole = 3.778082f;
	public float startingZOfTeddyBearHole = -2.0f;

	public float xMultiplierOfTeddyBear = 1.0f;
	public float yMultiplierOfTeddyBear = 0.0f;
	public float zMultiplierOfTeddyBear = 5.0f;
	public float xMultiplierOfTeddyBearHole = 1.0f;
	public float yMultiplierOfTeddyBearHole = 0.0f;
	public float zMultiplierOfTeddyBearHole = 10.0f;

	// Use this for initialization
	void Awake () {
		if (teddyBearHolesContainer == null) 
		{
			teddyBearHolesContainer = GameObject.Find ("Holes");
		}

		//instantiate 5 Teddybears
		for (int i = 0; i < numberOfTeddyBears; i++) 
		{
			GameObject.Instantiate (teddyBear, new Vector3(startingXOfTeddyBear + (xMultiplierOfTeddyBear * i), 
			                                               startingYOfTeddyBear + (yMultiplierOfTeddyBear * i), 
			                                               startingZOfTeddyBear + (zMultiplierOfTeddyBear * i)), Quaternion.identity);	 	
		}
		GameObject.Destroy (teddyBear);
		for (int i = 0; i < numberOfTeddyBearHoles; i++) 
		{
			GameObject temp = (GameObject) GameObject.Instantiate(teddyBearHole, new Vector3(startingXOfTeddyBearHole + (xMultiplierOfTeddyBearHole * i), 
			                                                                                 startingYOfTeddyBearHole + (yMultiplierOfTeddyBearHole * i), 
			                                                                                 startingZOfTeddyBearHole + (zMultiplierOfTeddyBearHole * i)), Quaternion.identity);
			temp.transform.parent = teddyBearHolesContainer.transform;
		}
		GameObject.Destroy (teddyBearHole);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
