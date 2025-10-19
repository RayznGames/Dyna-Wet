
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimeHandles))]
public class CustomInspector : Editor
{
	// Start is called before the first frame update
	public override void OnInspectorGUI()
	{				
		EditorGUILayout.LabelField("This allows for full control over the sun's position in relation to the hours");
		TimeHandles tHandler = (TimeHandles)target;
		DrawDefaultInspector();		

		if (GUILayout.Button("Bake Environment Time"))
		{
			tHandler.SetTime();
		}
	}
}
