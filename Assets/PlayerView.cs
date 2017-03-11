using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

    Transform mainCamera;
    Vector3 rotation;
    public float sensitivity = 20f;

    void Start() {
        mainCamera = Camera.main.transform;
        UpdateCameraPosition();
    }

	// Update is called once per frame
	void LateUpdate () {
        rotation += new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0) * sensitivity; // Don't use time.deltaTime
        rotation.x = Mathf.Clamp(rotation.x, -90, 90);
        this.transform.rotation = Quaternion.Euler(rotation);
        UpdateCameraPosition();
	}

    void UpdateCameraPosition() {
        mainCamera.transform.position = this.transform.position;
        mainCamera.transform.rotation = this.transform.rotation;
    }
}
