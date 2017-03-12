using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactons : MonoBehaviour {
    public Image words;

    public Sprite whereFlower;
    public Sprite love;

    bool recievedFlower = false;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!recievedFlower)
                words.sprite = whereFlower;
        }

        if (other.tag == "Flower") {
            words.sprite = love;
            recievedFlower = true;
        }
    }
}