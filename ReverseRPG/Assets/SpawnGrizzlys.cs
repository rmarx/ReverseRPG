using UnityEngine;
using System.Collections;

public class SpawnGrizzlys : MonoBehaviour {
	public GameObject grizzly = null;

	public int numberOfGrizzlys = 3;
	public int numberOfWaves = 3;
	public float distanceBetweenWaves = 5.0f;

	void Awake()
	{
		if (grizzly == null) 
		{
			grizzly = GameObject.Find ("Grizzly");
		}

		//spawn X waves
		for (int waves = 0; waves < numberOfWaves; waves++) 
		{
			//instantiate X Grizzlys
			for (int i = 0; i < numberOfGrizzlys; i++) 
			{
				Vector3 position = new Vector3(Random.Range(-13, 13), 0, Random.Range(-13, 13));
				Vector3 relativePos = new Vector3(grizzly.transform.position.x + position.x, 0, grizzly.transform.position.z + position.z + (waves * (distanceBetweenWaves+1)));
				GameObject tempGrizzly = (GameObject) GameObject.Instantiate (grizzly, relativePos, Quaternion.identity);	 	
				tempGrizzly.transform.parent = this.transform.parent;
			}
		}
		GameObject.Destroy (grizzly);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
