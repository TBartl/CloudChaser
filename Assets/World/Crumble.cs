using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour {
    public float delay = 2.0f;
    public float respawn = 5f;
    BoxCollider boxCollider;
    GameObject particleSyst;

    bool beingDestroyed = false;

    void Awake() {
        boxCollider = this.GetComponent<BoxCollider>();
        particleSyst = transform.GetComponentInChildren<ParticleSystem>().gameObject;

    }
	
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            if (!beingDestroyed)
                StartCoroutine(DestroyAndRespawn());
        }
    }

    IEnumerator DestroyAndRespawn() {
        beingDestroyed = true;
        yield return new WaitForSeconds(delay);
        particleSyst.SetActive(false);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(respawn);
        particleSyst.SetActive(true);
        boxCollider.enabled = true;

        beingDestroyed = false;
    }

}
