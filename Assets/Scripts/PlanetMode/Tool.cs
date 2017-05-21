using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
	
	public abstract void Use ();

	public abstract void Stop ();

	public virtual void Update () {
		Use ();
	}

}
