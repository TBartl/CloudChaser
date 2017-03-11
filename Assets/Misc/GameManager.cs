using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager S;
    float startSequenceTime;


    public float remainingTime = 30f;

    void Awake() {
        S = this;
    }

    public void FailLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RunGame() {
        Transform flower;
        Transform girl;
        Transform player;
        yield return null;
    }

}
