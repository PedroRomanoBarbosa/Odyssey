using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	protected float damageCounter;
	protected bool damaged;
	protected bool stop;
	protected int collisionCounter;

	public int life;
	public int damage;
	public GameObject drop;
	public SkinnedMeshRenderer thisRenderer;
	public Material defaultMaterial;
	public Material damageMaterial;
	public float damageTime;

	public virtual void Update() {
		if (damaged) {
			damageCounter += Time.deltaTime;
			if (damageCounter >= damageTime) {
				thisRenderer.material = defaultMaterial;
				stop = false;
				damaged = false;
			} else {
				stop = true;
			}
		}
	}

	public void EnterBodyCollider (Collider collider) {
		if (collider.gameObject.CompareTag("Missile")) {
			if (collisionCounter == 0) {
				MissileMovement missile = collider.gameObject.transform.parent.GetComponent<MissileMovement> ();
				DecreaseLife (missile.damage);
				collisionCounter++;
				damageCounter = 0;
				damaged = true;
			}
		} else if (collider.gameObject.CompareTag("Pick")) {
			MiningPick pick = collider.transform.parent.GetComponent<MiningPick> ();
			DecreaseLife (pick.damage);
			damageCounter = 0;
			damaged = true;
		}
	}

	public virtual void Die () {
		stop = true;
		Destroy (this.gameObject);
		if (drop != null) {
			FauxGravityAttractor attractor = GetComponent<FauxGravityBody> ().attractor;
			GameObject dropInstance = Instantiate (drop, transform.position, transform.rotation);
			dropInstance.GetComponent<FauxGravityBody> ().attractor = attractor;
		}
	}

	public void ExitBodyCollider (Collider collider) {
		if (collider.gameObject.CompareTag ("Missile") || collider.gameObject.CompareTag ("Pick")) {
			if (collisionCounter == 1) {
				collisionCounter = 0;
			}
		}
	}

	public void OnHitZoneStay (Collider collider) {
		if (collider.name == "Flamethrower") {
			Flamethrower flameThrower = collider.GetComponent<Flamethrower> ();
			DecreaseLife (flameThrower.damage);
			damageCounter = 0;
			damaged = true;
		}
	}

	public virtual void DecreaseLife (int damage) {
		life -= damage;
	}

	public virtual void OnAttackAreaStay (Collider collider) {
		
	}

	public virtual void OnAttackAreaExit (Collider collider) {

	}

}
