using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelFallZone : MonoBehaviour {
    public string partingMessage1;
    public string partingMessage2;
    public float waitTime = 5f;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(FinishGame());
        }
    }

    IEnumerator FinishGame() {
        yield return new WaitForSeconds(waitTime);
        GameManager.S.EnableText(partingMessage1);
        yield return new WaitForSeconds(waitTime);
        GameManager.S.EnableText(partingMessage2);
        yield return new WaitForSeconds(waitTime);
        GameManager.S.WinLevel();
    }
}
