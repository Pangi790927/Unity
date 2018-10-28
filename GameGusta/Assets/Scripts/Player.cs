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
	public Camera cam;
	public Rigidbody bullet;
	public float gunSpread;

	private bool onGround = true;

	void Start () {
		
	}

	void Update () {
		bool runs = false;
		bool jumps = false;

		if (Input.GetKey("left shift"))
			runs = true;

		if (Input.GetKey("space") && onGround)
			jumps = true;

		VelocityUpdate(runs, jumps);
		AimUpdate();

		if (Input.GetButton("Fire1"))
			Shoot();
	}

	void OnCollisionEnter() {
		onGround = true;
	}

	void OnCollisionExit() {
		onGround = false;
	}

	void OnCollisionStay() {
		onGround = true;
	}

	void Shoot() {
        // random var 3 must be modifyed to mean the distance from the player at wich the bullet
        // must spawn
        System.Random randFloat = new System.Random();
		Quaternion randRot = Quaternion.Euler(
				(float)randFloat.NextDouble() * gunSpread,
				(float)randFloat.NextDouble() * gunSpread,
				(float)randFloat.NextDouble() * gunSpread);
		var instance = Instantiate(bullet, transform.position + transform.forward * 3,
					transform.rotation);
		instance.velocity = BulletSpeed * (randRot * transform.forward);
	}

	void VelocityUpdate(bool runs, bool jumps) {
		float realSpeed = speed;

		if (runs)
			realSpeed *= speedMult;

		if (jumps)
			GetComponent<Rigidbody>().velocity = new Vector3(0, JumpSpeed, 0);

		var cameraRotate = cam.GetComponent<CameraScript>().rot;
		GetComponent<Rigidbody>().velocity = cameraRotate * new Vector3(
			Input.GetAxis("Horizontal") * Time.deltaTime * realSpeed,
			GetComponent<Rigidbody>().velocity.y,
			Input.GetAxis("Vertical") * Time.deltaTime * realSpeed
		);
	}

	void AimUpdate() {
		RaycastHit hit = new RaycastHit();
		var ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance: MaxRayDistance)) {
			var desired = Quaternion.LookRotation((hit.point - transform.position).normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, 
							rotSpeed * Time.deltaTime);
		}
	}
}
