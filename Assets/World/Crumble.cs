using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour {
    public float delay = 2.0f;
	
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            Destroy(this.gameObject, delay);
        }
    }

}
