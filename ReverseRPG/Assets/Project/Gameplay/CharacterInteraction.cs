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
		Melee = 3,
		InstaKill = 4,
		ExtraHealth = 5
	}
}

public class CharacterInteraction : LugusSingletonExisting<CharacterInteraction> 
{
	public float health = 100000.0f;
	public float maxHealth = 100000.0f;

	public ForceField[] forceFields;
	public OrbitProjectile[] projectiles;

	public GameObject scoreTextPrefab = null;

	public void SetMaxHealth(float newMaxHealth)
	{
		float oldMaxHealth = maxHealth;

		maxHealth = newMaxHealth;
		health = Mathf.Clamp( health, 0, maxHealth);

		UIController.use.healthBar.Reset( oldMaxHealth, maxHealth );
	}

	public void ChangeHealth(float amount, EnemyTarget enemy)
	{
		SoundManager.use.PlaySFX( SoundManager.use.playerHit );

		health += amount;
		float duration = 0.8f;

		if( health <= 0.0f )
			LugusCoroutines.use.StartRoutine( DieRoutine () );

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
			else if( enemy.damageType == RPG.DamageType.Electric )
				color = Color.yellow;
			else if( enemy.damageType == RPG.DamageType.InstaKill )
				color = Color.white;
			else if( enemy.damageType == RPG.DamageType.ExtraHealth )
				color = Color.green;
		}

		color = color.a (0.7f);

		if( amount < 0 )
			scoreText.GetComponent<TextMesh>().text = "" + amount;
		else
			scoreText.GetComponent<TextMesh>().text = "+" + amount;

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

	public IEnumerator DieRoutine()
	{
		timeCounting = false;

		
		SoundManager.use.footLoop.Stop();
		SoundManager.use.PlaySFX( SoundManager.use.death );

		Debug.LogError("I'M MELTING!");

		this.GetComponent<IsometricMovement>().moveEnabled = false;
		this.GetComponent<ProjectileController>().enabled = false;

		foreach( Transform child in this.transform )
		{
			child.gameObject.SetActive( false );
		}

		EffectsManager.use.Spawn( EffectsManager.use.death, this.transform.position );

		float finalTime = Time.time - startTime;
		LugusConfig.use.User.SetFloat( "level.previousTime", finalTime, true );

		yield return new WaitForSeconds(2.0f);

		DowngradeMenu.use.Show( finalTime );
	}


	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( forceFields == null || forceFields.Length == 0 )
		{
			FetchForceFields();
		}

		if ( projectiles == null || projectiles.Length == 0 )
		{
			FetchProjectiles();
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

	public void FetchForceFields()
	{
		
		forceFields = transform.GetComponentsInChildren<ForceField>();
	}

	public void FetchProjectiles()
	{
		projectiles = transform.GetComponentsInChildren<OrbitProjectile>();
	}

	public void OnTriggerEnter(Collider collider)
	{

		EnemyTarget enemy = collider.GetComponent<EnemyTarget>();

		if( enemy == null || enemy.enabled == false )
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

		float damage = 0.0f;
		if( enemy.damage.from == enemy.damage.to && enemy.damage.from == 0 )
			damage = Random.Range( 1000, 10000 );
		else
			damage = enemy.damage.Random();

		if( shieldActive )
			damage /= 2.0f;

		if( enemy.damageType != RPG.DamageType.ExtraHealth )
			damage *= -1.0f;
		else
		{
			damage = this.maxHealth / 10.0f;
			GameObject.Destroy( enemy.gameObject );
		}

		ChangeHealth( damage, enemy );
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script
		LugusCoroutines.use.StartRoutine( StartDelayRoutine() );
	}

	protected IEnumerator StartDelayRoutine()
	{
		yield return new WaitForSeconds(1.0f);

		GetComponent<IsometricMovement>().StartMoving();

		startTime = Time.time;
		timeCounting = true;

		SoundManager.use.footLoop.Play();
	}
	
	protected void Awake()
	{
		SetupLocal();
	}

	protected void Start () 
	{
		SetupGlobal();
	}

	public float startTime = 0.0f;
	public bool timeCounting = false;
	
	protected void Update () 
	{
		
	}
}
