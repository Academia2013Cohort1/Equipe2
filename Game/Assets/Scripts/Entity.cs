using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
	protected bool onWall = false;
	protected Dictionary<string, bool> tagsCollided = new Dictionary<string, bool>();
	
	// Use this for initialization
	public void Start () {
		tagsCollided.Add("Red", false);
		tagsCollided.Add("Blue", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnCollisionEnter(Collision c) {
		tagsCollided[c.gameObject.tag] = true;
		
		bool horizontal = false;
		foreach(ContactPoint p in c.contacts) {
			if(Mathf.Abs(p.normal.y) > Mathf.Abs(p.normal.x) && p.point.y < transform.position.y) {
				horizontal = true;
				break;
			}
		}
		if(!horizontal) {
			onWall = true;
		}
	}
	
	void OnCollisionExit(Collision c) {
		tagsCollided[c.gameObject.tag] = false;
		onWall = false;
	}
}
