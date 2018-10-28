using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can we use some better method than this to have a camera?
public class CameraScript : MonoBehaviour {
	public Transform player;
	public Vector3 offset;
	public Quaternion rot { get; set; }
	public float cameraRotateSpeed;

	private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		rot = Quaternion.identity;
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) {
			var toRotate = Quaternion.Euler(0, -90, 0);
			rot = toRotate * rot;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			var toRotate = Quaternion.Euler(0, 90, 0);
			rot = toRotate * rot;
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation,
					rot * originalRotation, cameraRotateSpeed * Time.deltaTime);

		var transformedOffset = Quaternion.Euler(0,
				XZAngleBetween(new Vector3(0, 0, 1), transform.forward), 0) * offset;

		transform.position = player.position + transformedOffset;
	}

	private float XZAngleBetween(Vector3 a, Vector3 b) {
		var angleA = Mathf.Atan2(a.x, a.z) * Mathf.Rad2Deg;
		var angleB = Mathf.Atan2(b.x, b.z) * Mathf.Rad2Deg;

		return Mathf.DeltaAngle(angleA, angleB);
	}
}
