using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( Helper ) )]
public class HelperEditor : Editor {
	
	public void OnSceneGUI() {
		Event e = Event.current;
		if (e.type == EventType.MouseDown) {
			Ray ray = HandleUtility.GUIPointToWorldRay( Event.current.mousePosition );
			RaycastHit hit;
			int mask = 1 << 0;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {
				Helper helper = GameObject.Find ("Helper").GetComponent<Helper> ();
				GameObject prefab = helper.prefab;
				GameObject obj = Instantiate (prefab, hit.point, Quaternion.identity);
				Vector3 direction = obj.transform.position - helper.planet.transform.position;
				obj.transform.up = direction.normalized;
				obj.name = prefab.name;
			}
			e.Use ();
		}
		HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
	}
}
