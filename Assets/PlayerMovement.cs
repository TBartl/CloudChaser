using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    PlayerView view;
    public float maxHorizontalSpeed = 5f;
    public AnimationCurve accelerationBySpeed;

    [HideInInspector] public Rigidbody rigid;

	// Use this for initialization
	void Awake () {
        view = this.GetComponentInChildren<PlayerView>();
        rigid = this.GetComponent<Rigidbody>();		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 groundVelocity = rigid.velocity;
        groundVelocity.y = 0;

        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        inputDirection = view.transform.TransformVector(inputDirection).normalized; // TODO if we want to use a controller and have it be smooth
        inputDirection.y = 0; // we'll need to rotate this manually with angles and shit
        inputDirection = inputDirection.normalized; 
        Vector3 targetDirection = (inputDirection - groundVelocity / maxHorizontalSpeed).normalized;
        float acceleration = accelerationBySpeed.Evaluate((targetDirection - groundVelocity / maxHorizontalSpeed).magnitude);
        rigid.velocity += targetDirection * acceleration * Time.deltaTime;
        CheckOutOfBounds();
    }

    void CheckOutOfBounds() {
        if (transform.position.y < -30) {
            this.transform.position = Vector3.zero;
            rigid.velocity = Vector3.zero;
        }
    }
}
