using UnityEngine;
using System.Collections;

public class CameraMovment : MonoBehaviour {
	public Transform toFollow;
	public float distance = 16;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = toFollow.position + new Vector3(0, 0, -distance);
	}
}
