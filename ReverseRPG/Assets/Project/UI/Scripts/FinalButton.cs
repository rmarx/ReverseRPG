using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalButton : MonoBehaviour 
{
	public Sprite hoverSprite = null;
	public Sprite idleSprite = null;

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
		Transform finalButton = this.transform.FindChild("Button");
		if( LugusInput.use.RayCastFromMouse(LugusCamera.ui) == finalButton.transform )
		{
			finalButton.GetComponent<SpriteRenderer>().sprite = hoverSprite;
			
			if( LugusInput.use.down )
			{
				SoundManager.use.PlaySFX( SoundManager.use.continueButton );
				LugusCoroutines.use.StartRoutine( EndRoutine() );
			}
		}
		else
		{
			finalButton.GetComponent<SpriteRenderer>().sprite = idleSprite;
		}
	}

	protected IEnumerator EndRoutine()
	{
		LugusConfig.use.User.SetBool( "downgrade.ending.freemove", true, true );
		LugusConfig.use.SaveProfiles();
		
		foreach( Transform child in this.transform )
		{
			if( child.name != "Background" )
				child.gameObject.MoveTo( child.transform.position.yAdd( -20.0f ) ).Time (1.0f).Execute();
		}
		
		yield return new WaitForSeconds(1.0f);
		
		
		Application.LoadLevel( Application.loadedLevelName );
	}
}
