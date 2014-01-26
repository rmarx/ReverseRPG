using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DowngradeButton : MonoBehaviour 
{
	public string configKey = "";

	public Sprite activeSprite = null;
	public Sprite inactiveSprite = null;

	public bool activated = false;
	public string description = "Button description";

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

	public void Activate()
	{
		activated = true;
		this.GetComponent<SpriteRenderer>().sprite = activeSprite;
	}

	public void Deactivate()
	{
		this.activated = false;
		this.GetComponent<SpriteRenderer>().sprite = inactiveSprite;

	}
	
	protected void Update () 
	{
		if( LugusInput.use.RayCastFromMouse(LugusCamera.ui) == this.transform )
		{
			this.GetComponent<SpriteRenderer>().sprite = activeSprite;
			DowngradeMenu.use.description.text = this.description;

			if( LugusInput.use.down && !this.activated )
			{
				DowngradeMenu.use.ChooseDowngrade( this );
			}
		}
		else
		{
			if( !activated )
				this.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
		}
	}
}
