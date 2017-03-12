using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactons : MonoBehaviour {
    public Image words;

    public Sprite whereFlower;
    public Sprite love;

    [HideInInspector] public bool hasBeenConfusedAboutFlower = false;

    bool recievedFlower = false;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!recievedFlower) {
                if (hasBeenConfusedAboutFlower == false)
                    AudioManager.S.wheresTheFlower.Play();
                words.sprite = whereFlower;
                hasBeenConfusedAboutFlower = true;
            }
        }

        if (other.tag == "Flower") {
            words.sprite = love;
            recievedFlower = true;
            GameManager.S.WinLevel();
        }
    }
}