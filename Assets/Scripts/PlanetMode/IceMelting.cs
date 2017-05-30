using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMelting : MonoBehaviour {
    private bool melting;

    public float speed;
    public Collider collider;
    public float scaleLimit;
    public GameObject particleEnd;
    public GameObject explosionAnchor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (melting) {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * speed, transform.localScale.y - Time.deltaTime * speed, 1);
            if (transform.localScale.y <= scaleLimit) {
                GameObject explosion = Instantiate(particleEnd, explosionAnchor.transform.position, explosionAnchor.transform.rotation);
                explosion.transform.localScale = new Vector3(7, 7, 7);
                Destroy (gameObject);
            }
            collider.enabled = false;
            collider.enabled = true;
            melting = false;
        }
	}

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.name == "Flamethrower") {
            melting = true;
        }
    }

}
