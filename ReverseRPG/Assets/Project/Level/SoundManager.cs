using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : LugusSingletonExisting<SoundManager> 
{
	public AudioClip[] levelAudio;

	public AudioClip death;
	public AudioClip continueButton;
	public AudioClip playerHit;

	public AudioClip runLoop;

	public ILugusAudioTrack footLoop = null;

	public AudioClip thunder = null;
	public AudioClip enemyHit = null;

	public void PlaySFX( AudioClip sfx )
	{
		LugusAudio.use.SFX().Play( sfx, false, new LugusAudioTrackSettings().Volume(1.7f) );
	}

	public void LoadForProgress( int downgradeCount )
	{
		AudioClip clip = null;

		if( downgradeCount <= 2 )
			clip = levelAudio[0];

		if( downgradeCount > 2 && downgradeCount <= 4 )
			clip = levelAudio[1];
		
		if( downgradeCount > 4 && downgradeCount <= 6 )
			clip = levelAudio[2];

		
		if( downgradeCount > 6 )
			clip = levelAudio[3];

		LugusAudio.use.Music().Play( clip, false, new LugusAudioTrackSettings().Loop (true).Volume(0.1f) );

		footLoop = LugusAudio.use.Music ().Play ( runLoop, false, new LugusAudioTrackSettings().Loop (true).Volume(0.25f) ); 
		footLoop.Claim();
		footLoop.Pause();
	}

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
	
	}
}
