using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : LugusSingletonExisting<LevelBuilder> 
{
	public Sprite[] treeSpritesGood;
	public Sprite[] treeSpritesEvil;

	public int downgradeCount = 0;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		
		LugusConfig.use.ReloadDefaultProfiles();

	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script

		//int downgradeCount = 0;

		if( LugusConfig.use.User.GetBool("downgrade.fire.shield", false) )
		{
			ForceField[] fields = CharacterInteraction.use.forceFields;

			foreach( ForceField field in fields )
			{
				if( field.damageType == RPG.DamageType.Fire )
				{
					GameObject.DestroyImmediate( field.gameObject );
					break;
				}
			}

			// force rebuilding of the forceFields array
			CharacterInteraction.use.FetchForceFields();

			++downgradeCount;
		}

		if( LugusConfig.use.User.GetBool ("downgrade.fire.lava", false) )
		{
			LavaWater[] lavas = GameObject.FindObjectsOfType<LavaWater>();
			
			foreach( LavaWater lava in lavas )
			{
				lava.Switch ();
			}

			++downgradeCount;
		}
		
		if( LugusConfig.use.User.GetBool ("downgrade.support.weapons", false) )
		{
			OrbitProjectile[] projectiles = CharacterInteraction.use.projectiles;
			
			foreach( OrbitProjectile projectile in projectiles )
			{
				GameObject.DestroyImmediate( projectile.gameObject );
			}
			
			// force rebuilding of the forceFields array
			CharacterInteraction.use.FetchProjectiles();

			++downgradeCount;
		}
		
		if( LugusConfig.use.User.GetBool ("downgrade.support.health", false) )
		{
			CharacterInteraction.use.SetMaxHealth( CharacterInteraction.use.maxHealth / 2.0f );
			
			++downgradeCount;
		}
		
		if( LugusConfig.use.User.GetBool("downgrade.electric.shield", false) )
		{
			ForceField[] fields = CharacterInteraction.use.forceFields;
			
			foreach( ForceField field in fields )
			{
				if( field.damageType == RPG.DamageType.Electric )
				{
					GameObject.DestroyImmediate( field.gameObject );
					break;
				}
			}
			
			// force rebuilding of the forceFields array
			CharacterInteraction.use.FetchForceFields();
			
			++downgradeCount;
		}

		// a bit different here: if lightning is NOT enabled we need to disable it of course
		if( !LugusConfig.use.User.GetBool ("downgrade.electric.lightning", false) )
		{
			// TODO!!!

			LightningStrike[] strikes = GameObject.FindObjectsOfType<LightningStrike>();
			
			foreach( LightningStrike strike in strikes )
			{
				GameObject.Destroy(strike.gameObject);
			}

		}
		else
		{
			
			++downgradeCount;

		}

		
		if( LugusConfig.use.User.GetBool("downgrade.damage.shield", false) )
		{
			ForceField[] fields = CharacterInteraction.use.forceFields;
			
			foreach( ForceField field in fields )
			{
				if( field.damageType == RPG.DamageType.Melee )
				{
					GameObject.DestroyImmediate( field.gameObject );
					break;
				}
			}
			
			// force rebuilding of the forceFields array
			CharacterInteraction.use.FetchForceFields();
			
			++downgradeCount;
		}

		if( LugusConfig.use.User.GetBool("downgrade.damage.grizzly", false) )
		{
			TeddyGrizzlies[] teddies = GameObject.FindObjectsOfType<TeddyGrizzlies>();
			foreach( TeddyGrizzlies tg in teddies )
			{
				tg.grizzlies.SetActive(true);
				tg.teddys.SetActive(false);
				GameObject.Destroy( tg.teddys.gameObject );
			}

			++downgradeCount;
		}

		if( LugusConfig.use.User.GetBool("downgrade.ending.freemove", false) )
		{
			CharacterInteraction.use.GetComponent<IsometricMovement>().freeMove = true;

			++downgradeCount;
		}

		ChangeWorld( downgradeCount );
	}

	protected void ChangeWorld(int downgradeCounter )
	{
		SoundManager.use.LoadForProgress( downgradeCounter );

		// 4 = world chagne
		// 6 = slave // mss eerder vanaf health downgrade?
		// 8 = kleinere slave?

		Debug.Log ("Changing world " + downgradeCounter);

		if(  downgradeCounter < 6 )
		{
			//Debug.Log ("Shrinking boss");

			
			Transform slave = CharacterInteraction.use.transform.FindChild("AnimationSlave");
			GameObject.Destroy( slave.gameObject );

			//Transform boss = CharacterInteraction.use.transform.FindChild("AnimationBoss");
			//boss.localScale *= 0.7f;
		}

		GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
		GameObject[] ground = GameObject.FindGameObjectsWithTag ("Ground");
		CloudBobbing[] clouds = GameObject.FindObjectsOfType<CloudBobbing>();

		if( downgradeCounter > 2 )
		{
			foreach( GameObject tree in trees )
			{
				tree.GetComponent<SpriteRenderer>().sprite = treeSpritesEvil[ Random.Range(0, treeSpritesEvil.Length) ];
			}
		}
		else
		{
			foreach( GameObject tree in trees )
			{
				tree.GetComponent<SpriteRenderer>().sprite = treeSpritesGood[ Random.Range(0, treeSpritesGood.Length) ];
			}
		}

		Color groundColor = Color.white;
		Color cloudColor = Color.white;
		if( downgradeCounter < 2 )
		{
			groundColor = new Color(255 / 255.0f, 150 / 255.0f, 255 / 226.0f);
			cloudColor = new Color(214 / 255.0f, 245 / 255.0f, 255 / 255.0f);

		}
		else if( downgradeCounter >= 6 )
		{
			groundColor = new Color(126 / 255.0f, 2 / 255.0f, 2 / 255.0f);
			cloudColor = Color.black;
		}

		foreach( GameObject groundPiece in ground )
		{
			groundPiece.GetComponent<SpriteRenderer>().color = groundColor;
		}
		
		LugusCamera.game.backgroundColor = cloudColor;
		foreach( CloudBobbing cloud in clouds )
		{
			cloud.GetComponent<SpriteRenderer>().color = cloudColor;
		}


		if( downgradeCounter >= 6 )
		{
			Transform boss = CharacterInteraction.use.transform.FindChild("AnimationBoss");
			GameObject.Destroy( boss.gameObject );
		}


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
		if( LugusInput.use.KeyDown(KeyCode.M) )
			DowngradeMenu.use.Show( 25 );
		
		if( LugusInput.use.KeyDown(KeyCode.Escape) )
			Application.LoadLevel("Menu");
	}

	public void OnGUI()
	{
		if( !LugusDebug.debug )
			return;

		GUILayout.BeginArea( new Rect(0, 0, 350, 500), GUI.skin.box );
		
		LugusConfigUtil.GUIAutoSave();
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.lava");

		LugusConfig.use.User.GUIBoolInput("downgrade.support.weapons");
		LugusConfig.use.User.GUIBoolInput("downgrade.support.health");

		LugusConfig.use.User.GUIBoolInput("downgrade.electric.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.electric.lightning");

		LugusConfig.use.User.GUIBoolInput("downgrade.damage.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.damage.grizzly");

		
		LugusConfig.use.User.GUIBoolInput("downgrade.ending.freemove");


		GUILayout.EndArea();
	}

	public void OnDisable()
	{
		if( LugusConfigUtil.autosave )
		{
			LugusConfig.use.SaveProfiles();
		}
	}
	
	public void OnApplicationQuit()
	{
		if( LugusConfigUtil.autosave )
		{
			LugusConfig.use.SaveProfiles();
		}
	}
	
	public void OnApplicationPause()
	{
		if( LugusConfigUtil.autosave )
		{
			LugusConfig.use.SaveProfiles();
		}
	}
}
