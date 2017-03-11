using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour {

    PlayerMovement movement;
    PlayerView view;

    Camera mainCam;

    LineRenderer grappleLine;
    public Transform muzzle;
    Vector3 grapplePoint;
    public float maxDist = 20f;
    public float grappleSpeed = 10f;
    public float detatchDistance = 1.5f;
    public float verticalOffset;

    bool grappling = false;

    public GameObject hitPoint;

    public float fovSpeed = 20f;
    public Vector2 fovLimits;

	// Use this for initialization
	void Awake () {
        movement = transform.parent.GetComponentInChildren<PlayerMovement>();
        view = transform.parent.GetComponentInChildren<PlayerView>();
        grappleLine = transform.GetComponentInChildren<LineRenderer>();
        grappleLine.enabled = false;
        mainCam = Camera.main;
	}

    void Update() {
        if (Input.GetMouseButtonDown(0) && !grappling) {
            StartCoroutine(Grapple());
        }

        if (!grappling) {
            RaycastHit hit = GetGrapplePoint();
            if (hit.collider == null) {
                hitPoint.gameObject.SetActive(false);
            }
            else {
                hitPoint.gameObject.SetActive(true);
                hitPoint.transform.position = hit.point;
            }
        }
        else {
            hitPoint.gameObject.SetActive(false);
        }
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

        RaycastHit hit = GetGrapplePoint();

        if (hit.collider == null) {
            grappling = false;
            yield break;
        }

        grapplePoint = hit.point;
        grappleLine.enabled = true;

        while (Input.GetMouseButton(0) && Vector3.Distance(this.transform.position, grapplePoint) > detatchDistance) {
            movement.rigid.velocity = (grapplePoint + Vector3.up * verticalOffset - transform.position).normalized * grappleSpeed;
            yield return null;
        }

        grappleLine.enabled = false;
        grappling = false;
    }
	
	

    RaycastHit GetGrapplePoint() {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, transform.forward, out hit, maxDist, 1 << 8);
        return hit;
    }
    
}
