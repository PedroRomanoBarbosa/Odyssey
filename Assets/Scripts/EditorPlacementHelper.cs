using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorPlacementHelper : MonoBehaviour {
	public Transform planet;
	private GameObject selected;
	public bool enable;

	void Update () {
		if(enable) {
			selected = Selection.activeTransform.gameObject;
			Vector3 direction = selected.transform.position - planet.transform.position;
			Debug.DrawRay (planet.transform.position, direction, Color.red);
			selected.transform.up = direction;
		}
	}
}
