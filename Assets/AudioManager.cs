using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager S;
    public AudioSource grappleConnect;
    public AudioSource grappleDisconnect;
    public AudioSource grappleReload;

    public AudioSource jump;
    public AudioSource land;

    public AudioSource flowerCollect;
    public AudioSource flowerTurnIn;

    public AudioSource cloudCrumble;

    public AudioSource levelStart;

    public AudioSource wheresTheFlower;

    void Awake() {
        if (S != null) {
            Destroy(this.gameObject);
        }else {
            S = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
