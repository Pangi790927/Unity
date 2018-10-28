using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	static int count = 0; // should it be atomic? 
		
	public int maxCount;

	private int id; // we will asume a maximum of 2^31 bulets in a game

	// Use this for initialization
	void Start () {
		id = count++;
	}
	
	// Update is called once per frame
	void Update () {
		if (id < count - maxCount) {
			Destroy(gameObject);
		}
	}
}
