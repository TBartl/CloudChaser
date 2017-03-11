using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public AnimationCurve progress;
    public Vector3 startPos;
    public Vector3 endPos;
    public float cycleTime = 5.0f;
    Vector3 originalPos;

	// Use this for initialization
	void Start () {
        originalPos = transform.position;
        startPos = originalPos;
        endPos = originalPos + Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
        float t = (Time.timeSinceLevelLoad % cycleTime) / cycleTime;
        transform.position = Vector3.Lerp(startPos, endPos, progress.Evaluate(t));
	}
}
