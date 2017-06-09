using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour {
	public GameVariables.Artifact artifact;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			GameVariables.artifacts [(int)artifact] = true;
            CatchArtifact(artifact);
            Destroy (gameObject);
		}
	}

    void CatchArtifact(GameVariables.Artifact a)
    {
        if(a.ToString() == "Cuboid")
        {
            FindObjectOfType<GemDisplay>().SetCatchCuboid(true);
        }
        if (a.ToString() == "Star")
        {
            FindObjectOfType<GemDisplay>().SetCatchStar(true);
        }
        if (a.ToString() == "Penthagon")
        {
            FindObjectOfType<GemDisplay>().SetCatchPenta(true);
        }
        if (a.ToString() == "Spiral")
        {
            FindObjectOfType<GemDisplay>().SetCatchSpiral(true);
        }
    }
}
