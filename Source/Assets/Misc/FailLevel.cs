﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailLevel : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.S.FailLevel();
        }
    }
}