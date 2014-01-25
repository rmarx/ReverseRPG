using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceField : MonoBehaviour 
{
	public float rotationSpeed = 20.0f;
	public float rotationDirection = 1.0f; // 1.0f or -1.0f

	protected Transform second = null;

	public RPG.DamageType damageType = RPG.DamageType.Melee;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
		if( second == null )
			second = this.transform.FindChild("second");
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
		transform.Rotate( new Vector3(0, 0, rotationDirection * rotationSpeed * Time.deltaTime), Space.Self );

		if( Input.GetKeyDown(KeyCode.H) )
		{
			OnHit();
		}
	}

	public void OnHit()
	{
		LugusCoroutines.use.StartRoutine( OnHitRoutine() );
	}

	protected IEnumerator OnHitRoutine()
	{
		float duration = 0.3f;

		SpriteRenderer srenderer = this.GetComponent<SpriteRenderer>();
		SpriteRenderer secondrenderer = second.GetComponent<SpriteRenderer>();
		Color originalColor = Color.white.a (0.5f);
		Color secondOriginalColor = Color.white.a (0.0f);

		
		// we want the running animation to start playing before the invulnerability (red blinking) is done
		//LugusCoroutines.use.StartRoutine( HitAnimationRoutine (0.3f) );
		
		//float partDuration = duration / (float) blinkCount;


		float percentage = 0.0f;
		float startTime = Time.time;
		bool rising = true;
		Color newColor = new Color();
		Color secondNewColor = new Color();

		Color targetColor = Color.white;

		while( rising )
		{
			percentage = (Time.time - startTime) / (duration / 2.0f);
			newColor = originalColor.Lerp (targetColor, percentage);
			secondNewColor = secondOriginalColor.Lerp ( targetColor, percentage ); 
			
			srenderer.color = newColor;
			secondrenderer.color = secondNewColor;
			
			if( percentage >= 1.0f )
				rising = false;
			
			yield return null;
		}
		
		percentage = 0.0f;
		startTime = Time.time;
		
		while( !rising )
		{
			//Debug.Log ("TRANS DOWN " + percentage + " originalColor " + originalColor + " // " + newColor);
			percentage = (Time.time - startTime) / (duration / 2.0f);
			newColor = targetColor.Lerp (originalColor, percentage);
			secondNewColor = targetColor.Lerp ( secondOriginalColor, percentage ); 
			
			srenderer.color = newColor;
			secondrenderer.color = secondNewColor;
			
			if( percentage >= 1.0f )
				rising = true;
			
			yield return null;
		}


		/*
		iTween.Stop( this.gameObject );

		float halfDuration = 0.3f;
		Color originalColor = Color.white.a ( 125.0f );

		Hashtable tween = new Hashtable();
		tween.Add( "time", halfDuration );
		tween.Add ("color", Color.white );

		iTween.ColorTo( this.gameObject, tween );

		yield return new WaitForSeconds( halfDuration );

		tween = new Hashtable();
		tween.Add( "time", halfDuration );
		tween.Add ("color", originalColor );
		
		yield return new WaitForSeconds( halfDuration );
		*/
	}
}
