using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRendererOnAwake : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        this.GetComponent<Renderer>().enabled = false;
	}
}
