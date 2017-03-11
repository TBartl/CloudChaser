using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.transform.SetParent(other.transform);
            this.transform.localPosition = Vector3.up;
        }
    }
}
