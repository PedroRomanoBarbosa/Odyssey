using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class EditorPlacementHelper : MonoBehaviour {
	public Transform planet;
	private GameObject selected;
	public bool enable;

	void Update () {
		if(enable) {
			#if UNITY_EDITOR
			selected = Selection.activeTransform.gameObject;
			Vector3 direction = selected.transform.position - planet.transform.position;
			Debug.DrawRay (planet.transform.position, direction, Color.red);
			selected.transform.up = direction;
			#endif
		}
	}
}
