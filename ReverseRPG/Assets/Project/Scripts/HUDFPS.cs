using UnityEngine;
using System.Collections;
//using UnityEditor;


public class HUDFPS : MonoBehaviour 
{
	
	// Attach this to a GUIText to make a frames/second indicator.
	//
	// It calculates frames/second over each updateInterval,
	// so the display does not keep changing wildly.
	//
	// It is also fairly accurate at very low FPS counts (<10).
	// We do this not by simply counting frames per interval, but
	// by accumulating FPS for each frame. This way we end up with
	// correct overall FPS even if the interval renders something like
	// 5.5 frames.
	
	public  float updateInterval = 0.5F;
	
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private float fps;
	//private int drawCalls;
	public float posX = 200;
	public float posY = 10;
	public Color textCol = Color.black;
	
	void Start()
	{
		#if !UNITY_EDITOR
		Application.targetFrameRate = 60;  
		Debug.LogError("Set the framerate to 60");
		#endif
		timeleft = updateInterval;  
	}
	
	void Update()
	{
		#if UNITY_EDITOR
		Application.targetFrameRate = -1;  
		#endif
		
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
		{
			
			fps = accum/frames;
			//	DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
			//drawCalls = UnityStats.drawCalls;
		}		
	}
	
	//OverdrawPreventerInterface opi = null;
	
	void OnGUI () {
		
		if( !LugusDebug.debug)
			return;
		
		//if( opi == null )
		//	opi = OverdrawPreventerInterface.use;
		
		GUI.contentColor = textCol;
		GUI.Label(new Rect(posX, posY, 300, 20), "FPS: " + fps.ToString("F1") + " @ " + Application.targetFrameRate + " // " + Screen.width + " / " + Screen.height);
		//GUI.Label(new Rect(posX, posY+10, 100, 20), "Drawcalls: " + drawCalls);
		
	}
}