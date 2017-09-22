using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevel : MonoBehaviour {
    
    void OnTriggerEnter(Collider other) {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerGrapple>().overrideNoGrapple = true;
    }
    
}
