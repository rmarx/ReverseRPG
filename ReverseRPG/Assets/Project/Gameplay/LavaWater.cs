using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaWater : MonoBehaviour 
{
	public Material lavaMaterial = null;
	public bool lavaAnimationStarted = false;

	public void Switch()
	{
		this.renderer.material = lavaMaterial;

		this.GetComponent<EnemyTarget>().enabled = true;

	}

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
		if (!lavaAnimationStarted) 
		{
			Debug.LogWarning("starting lava anim");
			lavaAnimationStarted = true;
			LugusCoroutines.use.StartRoutine (animateLava());
		}
	}

	protected IEnumerator animateLava()
	{
		//yield break;
		
		Debug.LogWarning("lava anim");
		//add random lava effects
		while (true) {
						Vector3 position = new Vector3 (Random.Range (-8, 8), 0, Random.Range (-8, 8));
						Vector3 relativePos = new Vector3 (this.transform.position.x + position.x, 0, this.transform.position.z + position.z);
						EffectsManager.use.Spawn (EffectsManager.use.smokeTrail, relativePos);
		
						position = new Vector3 (Random.Range (-8, 8), 0, Random.Range (-8, 8));
						relativePos = new Vector3 (this.transform.position.x + position.x, 0, this.transform.position.z + position.z);
						EffectsManager.use.Spawn (EffectsManager.use.lavaBubble, relativePos);

						yield return new WaitForSeconds (2.0f);
				}
	}
}
