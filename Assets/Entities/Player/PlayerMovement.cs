using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    PlayerView view;
    public float groundMaxHorizontalSpeed = 5f;
    public AnimationCurve groundAccelerationBySpeed;
    public float airMaxHorizontalSpeed = 20f;
    public AnimationCurve airAccelerationBySpeed;

    public float jumpPower;


    [HideInInspector] public Rigidbody rigid;

    bool grounded = true;
    bool wasGrounded = true;

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
        inputDirection = view.transform.TransformVector(inputDirection).normalized;
        inputDirection.y = 0;
        inputDirection = inputDirection.normalized;

        float maxHorizontalSpeed = grounded ? groundMaxHorizontalSpeed : airMaxHorizontalSpeed;
        AnimationCurve accelerationBySpeed = grounded ? groundAccelerationBySpeed : airAccelerationBySpeed;
        Vector3 targetDirection = (inputDirection - groundVelocity / maxHorizontalSpeed).normalized;
        float acceleration = accelerationBySpeed.Evaluate((targetDirection - groundVelocity / maxHorizontalSpeed).magnitude);
        rigid.velocity += targetDirection * acceleration * Time.deltaTime;
        if (inputDirection.magnitude == 0 && grounded && groundVelocity.magnitude < .5f) {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        }

        wasGrounded = grounded;
        CheckGrounded();
        if (!wasGrounded && grounded)
            AudioManager.S.land.Play();
    }

    void Update() {
        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            rigid.velocity += Vector3.up * jumpPower;
            grounded = false;
            AudioManager.S.jump.Play();
        }
    }
    

    void CheckGrounded() {
        RaycastHit hit;
        Physics.SphereCast(this.transform.position, .25f, Vector3.down, out hit, .8f, 1 << 8);
        grounded = (hit.collider != null);
    }
}
