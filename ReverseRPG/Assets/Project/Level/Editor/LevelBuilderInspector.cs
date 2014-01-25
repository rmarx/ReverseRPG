using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderInspector : Editor 
{
	protected bool showDefault = true;
	
	public override void OnInspectorGUI() 
	{
		LevelBuilder subject = (LevelBuilder) target;
		
		//EditorGUIUtility.LookLikeInspector(); 
		
		//showDefault = EditorGUILayout.Foldout(showDefault, "Show original");
		//if( showDefault )
		//{
			DrawDefaultInspector();
		//}




		
		LevelSection firstSection = subject.transform.GetChild(0).GetComponent<LevelSection>();
		
		float singleLength = 102.37631f;

		bool arrange = false;
		if( GUILayout.Button("Aansluiten") )
		{	
			arrange = true;
		}
		if( GUILayout.Button ("Scheiden") )
		{
			arrange = true;
			singleLength += (singleLength * 0.2f);
		}

		if( arrange )
		{
			List<LevelSection> sections = new List<LevelSection>();
			sections.AddRange( subject.transform.GetComponentsInChildren<LevelSection>() );

			sections.Remove( firstSection );

			List<LevelSection> sectionsSorted = new List<LevelSection>();
			
			float minDistance = float.MaxValue;
			LevelSection nextToAdd = null;
			while( sections.Count > 0 )
			{
				minDistance = float.MaxValue;
				
				foreach( LevelSection section in sections )
				{
					float distance = Mathf.Abs(section.transform.position.z - firstSection.transform.position.z);
					
					if( distance < minDistance )
					{
						minDistance = distance;
						nextToAdd = section;
					}
				}
				
				sections.Remove( nextToAdd );
				Debug.LogWarning(" Adding " + nextToAdd.name + " with dist " + minDistance);
				sectionsSorted.Add ( nextToAdd );
			}

			int count = 1;
			foreach( LevelSection section in sectionsSorted )
			{
				section.transform.position = section.transform.position.z( firstSection.transform.position.z + (count * singleLength) );

				count += section.length;
				//Debug.LogError( section.transform.Path() );
			}

		}


		/*
		EditorGUILayout.LabelField("------------"); 
		
		if( GUILayout.Button("Set shown") )
		{
			subject.shownPosition = subject.transform.position;
		}
		if( GUILayout.Button ("Move to shown") )
		{
			subject.transform.position = subject.shownPosition;
		}
		*/
		
	}
}
