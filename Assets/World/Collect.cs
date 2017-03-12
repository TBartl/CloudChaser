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
            AudioManager.S.flowerCollect.Play();
        }

        if (other.tag == "GirlInteractions" && !turnedIn) {
            this.transform.SetParent(other.transform);
            StartCoroutine(BounceToGirl());
            turnedIn = false;
            AudioManager.S.flowerTurnIn.Play();
        }
        
    }

    IEnumerator BounceToHead() {
        Vector3 from = this.transform.localPosition;
        Vector3 to = Vector3.up;
        Vector3 toScale = Vector3.one * .6f;
        float length = .5f;
        for (float t = 0; t < length; t += Time.deltaTime) {
            this.transform.localPosition = Vector3.Lerp(from, to, t/length);
            this.transform.localScale = Vector3.Lerp(Vector3.one, toScale, t / length);
            yield return null;
        }
        this.transform.localPosition = Vector3.up;
        this.transform.localScale = toScale;
    }

    IEnumerator BounceToGirl() {
        Vector3 from = this.transform.localPosition;
        Vector3 to = Vector3.right * .3f;
        Quaternion fromRot = this.transform.localRotation;
        float length = .5f;
        for (float t = 0; t < length; t += Time.deltaTime) {
            this.transform.localPosition = Vector3.Lerp(from, to, t / length);
            this.transform.localRotation = Quaternion.Lerp(fromRot, Quaternion.identity, t / length);
            yield return null;
        }
        this.transform.localPosition = to;
        this.transform.localRotation = Quaternion.identity;
    }
}
