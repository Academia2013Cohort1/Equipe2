using UnityEngine;
using System.Collections;

public class Character : Entity {
	public float speed = 4f;
	public float runSpeed = 8f;
	public float jumpStrenght = 1f;
	private float curSpeed;
	
	
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.A) && !onWall) {
			rigidbody.velocity = new Vector3(-curSpeed, rigidbody.velocity.y, 0);	
		} else if(Input.GetKey(KeyCode.D) && !onWall) {
			rigidbody.velocity = new Vector3(curSpeed, rigidbody.velocity.y, 0);	
		} else {
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);	
		}
		
		if(Input.GetKey(KeyCode.LeftShift)) {
			curSpeed = runSpeed;
		} else {
			curSpeed = speed;	
		}
		
		Debug.Log(tagsCollided["Red"]);
		
		if(Input.GetKey(KeyCode.Space) && tagsCollided["Red"] && !onWall) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpStrenght, 0);	
		} 
	}
}
