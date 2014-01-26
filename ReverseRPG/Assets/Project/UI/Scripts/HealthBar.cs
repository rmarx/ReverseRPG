using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour 
{
	public GameObject greenBar = null;
	public GameObject redBar = null;

	public Vector3 originalScale = Vector3.zero;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( greenBar == null )
			greenBar = GameObject.Find ("GreenBar");
		
		if( redBar == null )
			redBar = GameObject.Find ("RedBar");

		originalScale = greenBar.transform.localScale;
		redBar.transform.localScale = originalScale;
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
	}
}
