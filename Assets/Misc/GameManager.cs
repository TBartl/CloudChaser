using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct IntroSequenceSettings {
    public float spaceSpeedUpMultiplier;
    public float flowerToGirlTime;
    public AnimationCurve flowerToGirlCurve;
    
    public float girlToPlayerTimeTime;
    public AnimationCurve girlToPlayerTimeCurve;

    public Vector3 flowerRotation;
    public Vector3 girlRotation;

    public List<Image> playerImages;
}

public class GameManager : MonoBehaviour {

    public static GameManager S;
    public IntroSequenceSettings intro;
    public string introText;
    public string finishText;

    Text barText;
    public float levelEndTime = 5f;

    public float remainingTime = 30f;

    void Start() {
        S = this;
        barText = GameObject.Find("BarText").GetComponent<Text>() ;
        StartCoroutine(RunGame());
    }

    public void FailLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinLevel() {
        StartCoroutine(FinishLevel());
    }

    IEnumerator RunGame() {
        barText.text = introText;
        Transform flower = GameObject.FindGameObjectWithTag("Flower").transform;
        Transform girl = GameObject.FindGameObjectWithTag("Girl").transform;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 offset = Vector3.back * 1.5f + Vector3.up * 1.5f;
        player.gameObject.SetActive(false);
        foreach (Image i in intro.playerImages)
            i.enabled = false;
        for (float t = 0; t < intro.flowerToGirlTime; t += (Input.GetKey(KeyCode.Space) ? Time.deltaTime * intro.spaceSpeedUpMultiplier : Time.deltaTime)) {
            float p = (t / intro.flowerToGirlTime);
            p = intro.flowerToGirlCurve.Evaluate(p);
            Camera.main.transform.position = Vector3.Lerp(flower.position + offset + Vector3.up, girl.position + offset, p);
            Camera.main.transform.rotation = Quaternion.Slerp(Quaternion.Euler(intro.flowerRotation), Quaternion.Euler(intro.girlRotation), p);
            yield return null;
        }

        for (float t = 0; t < intro.girlToPlayerTimeTime; t += (Input.GetKey(KeyCode.Space) ? Time.deltaTime * intro.spaceSpeedUpMultiplier : Time.deltaTime)) {
            float p = (t / intro.girlToPlayerTimeTime);
            p = intro.girlToPlayerTimeCurve.Evaluate(p);
            Camera.main.transform.position = Vector3.Lerp(girl.position + offset, player.transform.position, p);
            Camera.main.transform.rotation = Quaternion.Slerp(Quaternion.Euler(intro.girlRotation), Quaternion.Euler(Vector3.zero), p);
            yield return null;
        }
        
        foreach (Image i in intro.playerImages)
            i.enabled = true;
        player.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        barText.transform.parent.parent.gameObject.SetActive(false);
    }

    IEnumerator FinishLevel() {
        barText.transform.parent.parent.gameObject.SetActive(true);
        barText.text = finishText;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

}
