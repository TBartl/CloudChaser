using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct IntroSequenceSettings {
    public float spaceSpeedUpMultiplier;
    public float flowerToGirlTime;
}

public class GameManager : MonoBehaviour {

    public static GameManager S;
    public IntroSequenceSettings intro;
    


    public float remainingTime = 30f;

    void Awake() {
        S = this;
        StartCoroutine(RunGame());
    }

    public void FailLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RunGame() {
        Transform flower = GameObject.FindGameObjectWithTag("Flower").transform;
        Transform girl = GameObject.FindGameObjectWithTag("Girl").transform;
        Transform player = GameObject.FindGameObjectWithTag("Girl").transform;
        player.gameObject.SetActive(false);
        for (float t = 0; t < intro.flowerToGirlTime; t += (Input.GetKeyDown(KeyCode.Space) ? Time.deltaTime : Time.deltaTime * intro.spaceSpeedUpMultiplier)) {
            float p = Mathf.Sin((t / intro.flowerToGirlTime) * Mathf.PI) *.5f + .5f;
            Camera.main.transform.position = Vector3.Lerp(flower.position, girl.position, t);
            yield return null;
        }

        player.gameObject.SetActive(false);
    }

}
