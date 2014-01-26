using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningStrike : MonoBehaviour 
{
	public GameObject effectPrefab = null;

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

		if( LugusConfig.use.User.GetBool ("downgrade.electric.lightning", false) )
		{
			StartCoroutine( StrikeRoutine() );
		}
	}
	
	protected void Update () 
	{
		
	}

	protected IEnumerator StrikeRoutine()
	{
		while( true )
		{
			GameObject effect = EffectsManager.use.Spawn( effectPrefab, this.transform.position );

			//this.collider.enabled = true;
			GameObject.Destroy( effect, 5.0f );

			if( Vector3.Distance(CharacterInteraction.use.transform.position, this.transform.position) < 20.0f )
				SoundManager.use.PlaySFX( SoundManager.use.thunder );

			yield return new WaitForSeconds( 0.5f );
			//this.collider.enabled = false;

			yield return new WaitForSeconds( Random.Range(0.5f, 1.0f) );
		}
	}
}
