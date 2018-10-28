using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private const int MaxRayDistance = 100;
	private const float BulletSpeed =  70;
	private const float JumpSpeed = 10;

    public float speed;
	public float speedMult;
	public float rotSpeed;
	public new Camera camera;
	public Rigidbody bullet;

	private bool onGround = true;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float realSpeed = speed;
		
		if (Input.GetKey("left shift"))
			realSpeed *= speedMult;

		if (Input.GetKey("space") && onGround) {
			GetComponent<Rigidbody>().velocity = new Vector3(0, JumpSpeed, 0);
			Debug.Log("worl");
		}

		if (Input.GetButton("Fire1")) {
			var instance = Instantiate(bullet, transform.position + transform.forward * 3,
					transform.rotation);
			instance.velocity = BulletSpeed * transform.forward;
		}

		var cameraRotate = camera.GetComponent<CameraScript>().rot;
		GetComponent<Rigidbody>().velocity = cameraRotate * new Vector3(
			Input.GetAxis("Horizontal") * Time.deltaTime * realSpeed,
			GetComponent<Rigidbody>().velocity.y,
			Input.GetAxis("Vertical") * Time.deltaTime * realSpeed
		);

		RaycastHit hit = new RaycastHit();
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance: MaxRayDistance)) {
			// Debug.DrawLine(transform.position, hit.point);
			var desired = Quaternion.LookRotation((hit.point - transform.position).normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, 
							rotSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(/* Collision collision */) {
		onGround = true;
	}

	void OnCollisionExit() {
		onGround = false;
	}

	void OnCollisionStay() {
		onGround = true;
	}
}
