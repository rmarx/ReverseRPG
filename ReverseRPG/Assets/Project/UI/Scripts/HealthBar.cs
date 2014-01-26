using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour 
{
	public GameObject greenBar = null;
	public GameObject redBar = null;

	public Vector3 originalScale = Vector3.zero;

	public TextMesh timer = null;
	public TextMesh previousTimer = null;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( greenBar == null )
			greenBar = GameObject.Find ("GreenBar");
		
		if( redBar == null )
			redBar = GameObject.Find ("RedBar");

		originalScale = greenBar.transform.localScale;
		redBar.transform.localScale = originalScale;

		timer.text = "";
		float prevTime = LugusConfig.use.User.GetFloat("level.previousTime", 0.0f);
		if( prevTime != 0.0f )
		{
			previousTimer.text = "Previous: " + prevTime + "s";
		}
		else
			previousTimer.text = "";
	}

	public void SetHealth(float health, float maxHealth, float delay)
	{
		LugusCoroutines.use.StartRoutine( SetHealthRoutine(health, maxHealth, delay) );
	}

	protected IEnumerator SetHealthRoutine(float health, float maxHealth, float delay )
	{
		yield return new WaitForSeconds( delay );

		DataRange healthRange = new DataRange(0, maxHealth);
		DataRange scaleRange = new DataRange(0, originalScale.x);
		
		float healthPercentage = healthRange.PercentageInInterval(health);
		float newX = scaleRange.ValueFromPercentage( healthPercentage );
		
		
		//Debug.Log ("HEALTHBAR SET HEALTH " + health + " / " + maxHealth + " // " + healthPercentage + " @ " + newX );
		
		greenBar.transform.localScale = greenBar.transform.localScale.x ( newX );
	}

	public void Reset( float oldMaxHealth, float maxHealth )
	{

		DataRange healthRange = new DataRange(0, oldMaxHealth);
		DataRange scaleRange = new DataRange(0, originalScale.x);
		
		float healthPercentage = healthRange.PercentageInInterval(maxHealth);
		float newX = scaleRange.ValueFromPercentage( healthPercentage );
		
		
		Debug.Log ("HEALTHBAR RESET HEALTH " + maxHealth + " / " + oldMaxHealth + " // " + healthPercentage + " @ " + newX );
		
		greenBar.transform.localScale = greenBar.transform.localScale.x ( newX );
		redBar.transform.localScale = redBar.transform.localScale.x ( newX );

		originalScale = greenBar.transform.localScale;
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
		redBar.transform.localScale = Vector3.Lerp( redBar.transform.localScale, greenBar.transform.localScale, Time.deltaTime * 5.0f );

		if( CharacterInteraction.use.timeCounting )
		{
			float time = (Time.time - CharacterInteraction.use.startTime);
			time *= 1000.0f;
			int t = (int) time;
			time = ( (float) t ) / 1000.0f;

			timer.text = "" + time + "s";
		}
	}
}
