using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
	protected bool onWall = false;
	protected bool onGround = false;
	protected Dictionary<string, bool> tagsCollided = new Dictionary<string, bool>();
	
	// Use this for initialization
	public void Start () {
		tagsCollided.Add("Red", false);
		tagsCollided.Add("Blue", false);
		tagsCollided.Add("Green", false);
		tagsCollided.Add("Yellow", false);
		tagsCollided.Add("Black", false);
		tagsCollided.Add("Orange", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision c) {
		tagsCollided[c.gameObject.tag] = true;
		
		bool horizontal = IsCollisionVertical(c);
		if(!horizontal) {
			onWall = true;
		} else {
			onGround = true;
		}
	}
	
	void OnCollisionExit(Collision c) {
		tagsCollided[c.gameObject.tag] = false;
		
		bool horizontal = IsCollisionVertical(c);
		if(!horizontal) {
			onWall = false;
		} else {
			onGround = false;	
		}
	}
	
	bool IsCollisionVertical(Collision c) {
		foreach(ContactPoint p in c.contacts) {
			if(Mathf.Abs(p.normal.y) > Mathf.Abs(p.normal.x) && p.point.y < transform.position.y) {
				return true;
			}
		}
		return false;
	}
}
