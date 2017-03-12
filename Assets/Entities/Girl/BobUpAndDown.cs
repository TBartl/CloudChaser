using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour {

    float startY;
    public float bobAmount;
    public float bobSpeed;

	// Use this for initialization
	void Start () {
        startY = this.transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = Vector3.up * (startY + Mathf.Sin(Time.timeSinceLevelLoad * bobSpeed) * bobAmount);
	}
}
