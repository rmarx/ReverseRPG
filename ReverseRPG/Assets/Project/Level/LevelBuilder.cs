using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour 
{
	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		
		LugusConfig.use.ReloadDefaultProfiles();

	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script
		
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
		}

		if( LugusConfig.use.User.GetBool ("downgrade.fire.lava", false) )
		{
			LavaWater[] lavas = GameObject.FindObjectsOfType<LavaWater>();
			
			foreach( LavaWater lava in lavas )
			{
				lava.Switch ();
			}
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
		}
		
		if( LugusConfig.use.User.GetBool ("downgrade.support.health", false) )
		{
			CharacterInteraction.use.SetMaxHealth( CharacterInteraction.use.maxHealth / 2.0f );
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
		}
		
		if( LugusConfig.use.User.GetBool ("downgrade.electric.lightning", false) )
		{
			// TODO!!!
			/*
			LavaWater[] lavas = GameObject.FindObjectsOfType<LavaWater>();
			
			foreach( LavaWater lava in lavas )
			{
				lava.Switch ();
			}
			*/
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
		}

		if( LugusConfig.use.User.GetBool("downgrade.damage.grizzly", false) )
		{
			// TODO:!!!
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
	
	}

	public void OnGUI()
	{
		if( !LugusDebug.debug )
			return;

		GUILayout.BeginArea( new Rect(0, 0, 350, 500) );
		
		LugusConfigUtil.GUIAutoSave();
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.fire.lava");

		LugusConfig.use.User.GUIBoolInput("downgrade.support.weapons");
		LugusConfig.use.User.GUIBoolInput("downgrade.support.health");

		LugusConfig.use.User.GUIBoolInput("downgrade.electric.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.electric.lightning");

		LugusConfig.use.User.GUIBoolInput("downgrade.damage.shield");
		LugusConfig.use.User.GUIBoolInput("downgrade.damage.grizzly");


		GUILayout.EndArea();
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
