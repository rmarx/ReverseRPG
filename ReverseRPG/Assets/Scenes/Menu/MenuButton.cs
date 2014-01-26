using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuButton : MonoBehaviour 
{
	public bool newGameButton = true;

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
		if( LugusInput.use.RayCastFromMouse(LugusCamera.ui) == this.transform )
		{
			this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

			if( LugusInput.use.down )
			{
				if( newGameButton )
				{
					Application.LoadLevel("Game");
				}
				else
				{
					#if UNITY_STANDALONE_WIN
					System.Diagnostics.Process.GetCurrentProcess().Kill();
#else
					Application.Quit();
#endif

				}
			}
		}
		else
		{
			this.transform.localScale = Vector3.one;
		}
	}
}
