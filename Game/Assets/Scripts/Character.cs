using UnityEngine;
using System.Collections;

public class Character : Entity {
	public CoinDisplay cDisp;
	public float speed = 4f;
	public float runSpeed = 8f;
	public float jumpStrenght = 1f;
	public int maxHealth = 3;
	private float curSpeed;
	private int health;
	
	
	// Use this for initialization
	void Start () {
		base.Start();
		health = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//movment
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
		
		bool canJump = false;
		if(tagsCollided["Red"] && !onWall)
			canJump = true;
		if(tagsCollided["Blue"] && !onWall)
			canJump = true;
		if(Input.GetKey(KeyCode.Space) && canJump) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpStrenght, 0);	
		}
	}
	
	public int GetHealth() {
		return health;
	}
	
	public int GetMaxHealth() {
		return maxHealth;	
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.tag == "Green") {
			Destroy(c.gameObject);
			cDisp.AddCoins(1);
		}
    }
}
