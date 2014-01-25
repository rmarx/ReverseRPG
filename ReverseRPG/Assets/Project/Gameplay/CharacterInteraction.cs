using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG
{
	public enum DamageType
	{
		NONE = -1,

		Fire = 1,
		Electric = 2,
		Melee = 3
	}
}

public class CharacterInteraction : MonoBehaviour 
{
	public float health = 100000.0f;
	public float maxHealth = 100000.0f;

	public ForceField[] forceFields;
	public OrbitProjectile[] projectiles;

	public GameObject scoreTextPrefab = null;

	public void ChangeHealth(float amount, EnemyTarget enemy)
	{
		health += amount;
		float duration = 0.8f;

		GameObject scoreText = (GameObject) GameObject.Instantiate( scoreTextPrefab );

		Vector2 screenPos = LugusCamera.game.WorldToScreenPoint( this.transform.position );
		Vector3 scorePosition = LugusCamera.ui.ScreenToWorldPoint( screenPos.v3 ().z(5.0f) );

		scoreText.transform.parent = LugusCamera.ui.transform.parent;
		scoreText.transform.position = scorePosition.z( 0.0f ).xAdd( new DataRange(-1.0f, 1.0f).Random() );  ;

		Color color = Color.white;
		if( enemy != null )
		{
			if( enemy.damageType == RPG.DamageType.Melee )
				color = Color.blue;
			else if( enemy.damageType == RPG.DamageType.Fire )
				color = Color.red;
			else
				color = Color.yellow;
		}

		color = color.a (0.7f);

		scoreText.GetComponent<TextMesh>().text = "" + amount;
		scoreText.GetComponent<TextMesh>().color = color;

		scoreText.transform.localScale = Vector3.zero;
		scoreText.ScaleTo( Vector3.one * 2.0f ).Time ( duration * 0.2f ).EaseType(iTween.EaseType.easeOutBack).Execute();
		scoreText.ScaleTo( Vector3.one ).Time ( duration * 0.2f ).Delay( duration * 0.2f).EaseType(iTween.EaseType.easeOutSine).Execute();

		scoreText.MoveTo( UIController.use.healthBar.transform.position ).Time ( duration * 0.8f ).Delay( duration * 0.2f) .EaseType(iTween.EaseType.easeInBack).Execute(); 
		UIController.use.healthBar.SetHealth( health, maxHealth, duration );

		GameObject.Destroy(scoreText, duration);

		/*
		if( sound != null )
			LugusAudio.use.SFX().Play( sound );
		*/
	}


	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( forceFields == null || forceFields.Length == 0 )
		{
			forceFields = transform.GetComponentsInChildren<ForceField>();
		}

		if ( projectiles == null || projectiles.Length == 0 )
		{
			projectiles = transform.GetComponentsInChildren<OrbitProjectile>();
		}
		
		if( scoreTextPrefab == null )
		{
			scoreTextPrefab = GameObject.Find("ScoreText");
		}
		
		if( scoreTextPrefab == null )
		{
			Debug.LogError(name + " : no ScoreTextPrefab found!");
		}
	}

	public void OnTriggerEnter(Collider collider)
	{

		EnemyTarget enemy = collider.GetComponent<EnemyTarget>();

		if( enemy == null )
			return;

		enemy.OnInteractionDone(); 

		Debug.Log (" CHARACTER HIT ENEMY " + collider.transform.Path() );

		bool shieldActive = false;
		foreach( ForceField field in forceFields )
		{
			if( field.damageType == enemy.damageType )
			{
				shieldActive = true;
				field.OnHit();
			}
		}

		float damage = maxHealth / 10.0f;
		if( shieldActive )
			damage = 0;

		damage = Random.Range( 1000, 10000 );

		ChangeHealth( -1.0f * damage, enemy );
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
	
	}
}
