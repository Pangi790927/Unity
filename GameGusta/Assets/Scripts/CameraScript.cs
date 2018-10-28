using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can we use some better method than this to have a camera?
public class CameraScript : MonoBehaviour {
	public Transform player;
	public Vector3 offset;
	public Quaternion rot { get; set; }

	// Use this for initialization
	void Start () {
		rot = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) {
			var toRotate = Quaternion.Euler(0, -90, 0);
			rot = toRotate * rot;
			transform.rotation = toRotate * transform.rotation;
			offset = toRotate * offset;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			var toRotate = Quaternion.Euler(0, 90, 0);
			rot = toRotate * rot;
			transform.rotation = toRotate * transform.rotation;
			offset = toRotate * offset;
		}
		// var screenPlayer = GetComponent<Camera>().WorldToScreenPoint(player.position);
		// var screenMouse = Input.mousePosition;
		// Debug.DrawLine(screenPlayer, screenMouse);
		transform.position = player.position + offset;
	}
}
