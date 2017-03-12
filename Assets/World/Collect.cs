using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
    bool collected = false;
    bool turnedIn = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !collected)
        {
            this.transform.SetParent(other.transform);
            StartCoroutine(BounceToHead());
            collected = true;
        }

        if (other.tag == "GirlInteractions") {
            this.transform.SetParent(other.transform);
            StartCoroutine(BounceToGirl());
            turnedIn = false;
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

    IEnumerator BounceToGirl() {
        Vector3 from = this.transform.localPosition;
        Vector3 to = Vector3.right * .3f;
        float length = .5f;
        for (float t = 0; t < length; t += Time.deltaTime) {
            this.transform.localPosition = Vector3.Lerp(from, to, t / length);
            yield return null;
        }
        this.transform.localPosition = to;
    }
}
