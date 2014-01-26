using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DowngradeMenu : LugusSingletonExisting<DowngradeMenu> 
{
	public Sprite[] buttons;
	public TextMesh description = null;

	public DowngradeButton chosenDowngrade = null;

	public Sprite continueButtonNormal = null;
	public Sprite continueButtonHover = null;

	public GameObject highlight = null;

	public void Show(float prevTime, float levelTime)
	{
		if( LevelBuilder.use.downgradeCount == 8 )
		{
			transform.FindChild("FinalButton").gameObject.SetActive(true);
		}

		if( LevelBuilder.use.downgradeCount == 9 )
		{
			transform.FindChild("EndScreen").gameObject.SetActive(true);
		}

		//float prevTime = LugusConfig.use.User.GetFloat("level.previousTime", 0.0f);
		if( prevTime != 0.0f )
		{
			string message = "";
			if( prevTime < levelTime )
			{
				message = "You were " + (levelTime - prevTime) + "s slower than last time!\nGet another downgrade to die quicker!";
			}
			else
			{
				message = "Excellent! You're improving your time!\nYou can die even more quickly with one of these downgrades!";
			}

			message = "Time: " + levelTime + "s\n" + message;

			transform.FindChild("LevelDescription").GetComponent<TextMesh>().text = message;
		}
		else
		{
			transform.FindChild("LevelDescription").GetComponent<TextMesh>().text = "You have succeeded in killing yourself in " + levelTime + " seconds.\nNow pick a new downgrade and die faster next time!";

		}

		
		gameObject.MoveTo( this.transform.localPosition.y(0.0f) ).IsLocal(true).Time(2.0f).EaseType(iTween.EaseType.easeOutBack).Execute();

	}

	public void ChooseDowngrade(DowngradeButton downgrade)
	{
		downgrade.Activate();
		if( chosenDowngrade != null )
			chosenDowngrade.Deactivate();

		chosenDowngrade = downgrade;

		highlight.transform.position = chosenDowngrade.transform.position.zAdd( 0.5f );
		
		Transform continueButton = transform.FindChild("ContinueButton");
		continueButton.GetComponent<SpriteRenderer>().enabled = true;
	}

	protected void CreateButton(string configKey, string spriteNameKey, string description, Vector3 position)
	{
		GameObject buttonHolder = new GameObject();
		buttonHolder.transform.parent = this.transform;
		buttonHolder.transform.localPosition = position;

		buttonHolder.name = configKey;
		buttonHolder.layer = this.gameObject.layer;

		buttonHolder.AddComponent<BoxCollider>();

		DowngradeButton button = buttonHolder.AddComponent<DowngradeButton>();
		button.description = description;
		button.configKey = configKey;

		button.activeSprite = GetSpriteForKey(spriteNameKey, true);
		button.inactiveSprite = GetSpriteForKey(spriteNameKey, false);

		SpriteRenderer rend = buttonHolder.AddComponent<SpriteRenderer>();
		rend.sprite = button.inactiveSprite;

		if( LugusConfig.use.User.GetBool(configKey, false) )
		{
			button.Activate();
		}
	}

	protected Sprite GetSpriteForKey(string spriteNameKey, bool active)
	{
		string activeString = "Hover";
		if( !active )
			activeString = "Ready";

		foreach( Sprite sprite in buttons )
		{
			if( sprite.name.Contains(spriteNameKey) && sprite.name.Contains(activeString) )
				return sprite;
		}

		Debug.LogError ("NO SPRITE FOUND " + spriteNameKey + " // " + activeString);
		return null;
	}

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( description == null )
			description = transform.FindChild ("Description").GetComponent<TextMesh>();
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script

		float x = -5.010963f;
		float y = 1.493114f; 

		float xIncrement = Mathf.Abs( -1.635475f - x );
		float yIncrement = Mathf.Abs( -1.594175f - y );

		/*
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.lava");
		
		LugusConfig.use.User.GUIBoolInput("downgrade.support.weapons");
		LugusConfig.use.User.GUIBoolInput("downgrade.support.health");
		
		LugusConfig.use.User.GUIBoolInput("downgrade.electric.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.electric.lightning");
		
		LugusConfig.use.User.GUIBoolInput("downgrade.damage.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.damage.grizzly");
		
		
		LugusConfig.use.User.GUIBoolInput("downgrade.ending.freemove");
		*/

		CreateButton( 
		             "downgrade.fire.shield",
		    		 "Fire_Shield",
		             "Removes the fire shield,\nmaking you twice as vulnerable to Lava",
		             new Vector3(x,y,0) );

		CreateButton( 
		             "downgrade.fire.lava",
		             "Fire_Lava",
		             "Transforms all water into molten Lava!",
		             new Vector3(x,y - yIncrement,0) );

		x += xIncrement;
		
		CreateButton( 
		             "downgrade.electric.shield",
		             "Lightning_Shield",
		             "Removes the electric shield, \nmaking you twice as vulnerable to Lightning",
		             new Vector3(x,y,0) );
		
		CreateButton( 
		             "downgrade.electric.lightning",
		             "Lightning_Lightning",
		             "Spawns massive bolts of lightning \nin thunderzones!",
		             new Vector3(x,y - yIncrement,0) );

		
		x += xIncrement;
		
		CreateButton( 
		             "downgrade.damage.shield",
		             "Damage_Shield",
		             "Removes the melee shield, \nmaking you twice as vulnerable to direct attack",
		             new Vector3(x,y,0) );
		
		CreateButton( 
		             "downgrade.damage.grizzly",
		             "Damage_Grizzly",
		             "Turns the cute teddybears \ninto gruesome grizzly bears!",
		             new Vector3(x,y - yIncrement,0) );
		
		x += xIncrement;
		
		CreateButton( 
		             "downgrade.support.weapons",
		             "Health_Weapons",
		             "Removes your flying minions of death",
		             new Vector3(x,y,0) );
		
		CreateButton( 
		             "downgrade.support.health",
		             "Health_Health",
		             "Reduces your health by half",
		             new Vector3(x,y - yIncrement,0) );

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
		Transform continueButton = transform.FindChild("ContinueButton");

		Transform hit = LugusInput.use.RayCastFromMouse(LugusCamera.ui);

		if( hit == this.transform.FindChild("Background") )
		{
			if( this.chosenDowngrade != null )
				this.description.text = this.chosenDowngrade.description;
			else
				this.description.text = "";
			
			continueButton.GetComponent<SpriteRenderer>().sprite = continueButtonNormal;

		}
		else if( hit == continueButton )
		{
			continueButton.GetComponent<SpriteRenderer>().sprite = continueButtonHover;

			if( LugusInput.use.down )
			{
				if( chosenDowngrade != null )
				{
					StartCoroutine( ContinueRoutine() );
				}
			}
		}
	}

	protected IEnumerator ContinueRoutine()
	{
		Debug.Log ("CONTINUE with upgrade " + chosenDowngrade.configKey);

		LugusConfig.use.User.SetBool( chosenDowngrade.configKey, true, true );
		LugusConfig.use.SaveProfiles();

		foreach( Transform child in this.transform )
		{
			if( child.name != "Background" )
				child.gameObject.MoveTo( child.transform.position.xAdd( 20.0f ) ).Time (1.0f).Execute();
		}

		SoundManager.use.PlaySFX( SoundManager.use.continueButton );

		yield return new WaitForSeconds(1.0f);


		Application.LoadLevel( Application.loadedLevelName );
	}
}
