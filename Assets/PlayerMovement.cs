using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    PlayerView view;
    public AnimationCurve accelerationBySpeed;
    public float decceleration = 5f;

    [HideInInspector] public Rigidbody rigid;

	// Use this for initialization
	void Awake () {
        view = this.GetComponentInChildren<PlayerView>();
        rigid = this.GetComponent<Rigidbody>();		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        Vector3 moveDirection = view.transform.TransformVector(inputDirection);
        moveDirection.y = 0;
        moveDirection =  moveDirection.normalized;

        Vector3 currentHorizontalVelocity = rigid.velocity;
        currentHorizontalVelocity.z = 0;
        rigid.AddForce(moveDirection * accelerationBySpeed.Evaluate(currentHorizontalVelocity.magnitude)  * Time.deltaTime);

	}
}
