using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct IntroSequenceSettings {
    public float spaceSpeedUpMultiplier;
    public float flowerToGirlTime;
    public AnimationCurve flowerToGirlCurve;

    public Vector3 flowerRotation;
    public Vector3 girlRotation;
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
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 offset = Vector3.back * 1.5f + Vector3.up * 1.5f;
        player.gameObject.SetActive(false);
        for (float t = 0; t < intro.flowerToGirlTime; t += (Input.GetKey(KeyCode.Space) ? Time.deltaTime * intro.spaceSpeedUpMultiplier : Time.deltaTime)) {
            float p = (t / intro.flowerToGirlTime);
            p = intro.flowerToGirlCurve.Evaluate(p);
            Camera.main.transform.position = Vector3.Lerp(flower.position + offset, girl.position + offset, p);
            Camera.main.transform.rotation = Quaternion.Slerp(Quaternion.Euler(intro.flowerRotation), Quaternion.Euler(intro.girlRotation), p);
            yield return null;
        }

        player.gameObject.SetActive(true);
    }

}
