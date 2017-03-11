﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrapple : MonoBehaviour {

    PlayerMovement movement;

    Camera mainCam;

    LineRenderer grappleLine;
    public Transform muzzle;
    Vector3 grapplePoint;
    public float maxDist = 20f;
    public float grappleSpeed = 10f;
    public float grappleAcceleration = 5f;
    public float detatchDistance = 1.5f;
    public float verticalOffset;

    bool grappling = false;
    
    public float fovSpeed = 20f;
    public Vector2 fovLimits;

    public float reloadTime;
    bool reloaded = true;

    Image chargeBar;

    public Color colReloading;
    public Color colCanGrapple;
    public Color colCantGrapple;
    Image crossHair;

    // Use this for initialization
    void Awake () {
        movement = transform.parent.GetComponentInChildren<PlayerMovement>();
        grappleLine = transform.GetComponentInChildren<LineRenderer>();
        grappleLine.enabled = false;
        mainCam = Camera.main;
        chargeBar = GameObject.Find("ChargeBar").GetComponent<Image>();
        crossHair = GameObject.Find("CrossHair").GetComponent<Image>();
	}

    void Update() {
        if (Input.GetMouseButtonDown(0) && !grappling && reloaded && 
            Vector3.Dot(transform.forward, (grapplePoint - this.transform.position).normalized) > .8f &&
            Vector3.Distance(this.transform.position, grapplePoint) > (detatchDistance + .5f)
            ) {
            StartCoroutine(Grapple());
        }

        bool canGrappleThisFrame = false;
        if (!grappling) {
            RaycastHit hit = GetGrapplePoint();
            if (hit.collider != null && Vector3.Dot(hit.normal, Vector3.up) > -.9f && hit.distance > (detatchDistance + 1)) {
                grapplePoint = hit.point;
                canGrappleThisFrame = true;
            }
        }

        if (!reloaded)
            crossHair.color = colReloading;
        else if (canGrappleThisFrame)
            crossHair.color = colCanGrapple;
        else
            crossHair.color = colCantGrapple;
    }

    // Update is called once per frame
    void LateUpdate() {
        grappleLine.SetPosition(0, muzzle.position);
        grappleLine.SetPosition(1, grapplePoint);

        if (grappling)
            mainCam.fieldOfView = Mathf.Min(mainCam.fieldOfView + fovSpeed * Time.deltaTime, fovLimits.y);
        else
            mainCam.fieldOfView = Mathf.Max(mainCam.fieldOfView - fovSpeed * Time.deltaTime, fovLimits.x);
    }

    IEnumerator Grapple() {
        grappling = true;

        grappleLine.enabled = true;
        float speed = Mathf.Max(movement.rigid.velocity.magnitude, grappleSpeed);
        while (Input.GetMouseButton(0) && Vector3.Distance(this.transform.position, grapplePoint) > detatchDistance) {
            movement.rigid.velocity = (grapplePoint + Vector3.up * verticalOffset - transform.position).normalized * speed;
            speed += grappleAcceleration * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ReloadGrapple());
        EndGrapple();
    }
	
    void EndGrapple() {
        grappling = false;
        grappleLine.enabled = false;
    }

    IEnumerator ReloadGrapple() {
        reloaded = false;
        chargeBar.color = Color.gray;
        for (float t = 0; t < reloadTime; t+= Time.deltaTime) {
            chargeBar.transform.localScale = new Vector3(t / reloadTime, 1, 1);
            yield return null;
        }
        chargeBar.transform.localScale = Vector3.one;
        chargeBar.color = Color.white;
        reloaded = true;
    }
	

    RaycastHit GetGrapplePoint() {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, transform.forward, out hit, maxDist, 1 << 8);
        return hit;
    }
    
}