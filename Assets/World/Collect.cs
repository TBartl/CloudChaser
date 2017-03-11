using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
    bool collected = false;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !collected)
        {
            this.transform.SetParent(other.transform);
            StartCoroutine(BounceToHead());
            collected = true;
        }
        
    }

    IEnumerator BounceToHead() {
        Vector3 from = this.transform.localPosition;
        Vector3 to = Vector3.up;
        float length = .5f;
        for (float t = 0; t < length; t += Time.deltaTime) {
            this.transform.localPosition = Vector3.Lerp(from, to, t/length);
            yield return null;
        }
        this.transform.localPosition = Vector3.up;
    }
}
