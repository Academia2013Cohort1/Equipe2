using UnityEngine;
using System.Collections;

public class Character : Entity {
	public CoinDisplay cDisp;
	public float speed = 4f;
	public float runSpeed = 8f;
	public float jumpStrenght = 1f;
	public int maxHealth = 3;
	public int nbMaxLives = 3;
	public float immunityTime = 1f;
	public float blinkSpeed = 0.5f;
	private float curSpeed;
	private int health;
	private int nblives;
	private Vector3 startPos;
	private float immunityTimer = 0f;
	private bool immunity = false;
	private GameObject [] platforms;
	
	// Use this for initialization
	new void Start () {
		base.Start();
		health = maxHealth;
		nblives = nbMaxLives;
		
		//spawn
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Yellow")) {
			startPos = o.transform.position;
		}
		transform.position = startPos;
		
		GameObject [] nPlatforms = GameObject.FindGameObjectsWithTag("Blue");
		GameObject [] fPlatforms = GameObject.FindGameObjectsWithTag("Orange");
		platforms = new GameObject[nPlatforms.GetLongLength(0)+fPlatforms.GetLongLength(0)];
		for(int i=0 ; i<nPlatforms.GetLongLength(0) ; i++)
			platforms[i] = nPlatforms[i];
		for(int i=0 ; i<fPlatforms.GetLongLength(0) ; i++)
			platforms[nPlatforms.GetLongLength(0)+i] = fPlatforms[i];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//movment
		if(Input.GetKey(KeyCode.A)) {
			rigidbody.velocity = new Vector3(-curSpeed, rigidbody.velocity.y, 0);	
		} else if(Input.GetKey(KeyCode.D)) {
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
		if((tagsCollided["Red"] || tagsCollided["Black"]) && onGround)
			canJump = true;
		if((tagsCollided["Blue"] || tagsCollided["Orange"]) && onGround)
			canJump = true;
		if(Input.GetKey(KeyCode.Space) && canJump) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpStrenght, 0);	
		}
		
		if(tagsCollided["Black"]) {
			Hurt(100);
		}
		
		UpdateImmunity();
		
		foreach(GameObject o in platforms) {
			if(rigidbody.velocity.y>0) {
				o.collider.isTrigger = true;	
			} else if(collider.bounds.min.y >= o.collider.bounds.max.y-0.05) {
				o.collider.isTrigger = false;
			}
		}
		
		Debug.Log(tagsCollided["Blue"]);
	}
	
	public void UpdateImmunity() {
		if(immunity) {
			immunityTimer += Time.deltaTime;
			if(immunityTimer%blinkSpeed<blinkSpeed/2f) {
				renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);	
			} else {
				renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1);
			}
			if(immunityTimer >= immunityTime) {
				immunityTimer = 0;
				immunity = false;
				renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1);
			}
		}
	}
	
	public int GetHealth() {
		return health;
	}
	
	public void Hurt(int damage) {
		if(immunity) {
			return;	
		}
		health = Mathf.Max(health-damage, 0);
		if(health == 0) {
			Kill();
		} else {
			immunity = true;
		}
	}
	
	public void Kill() {
		nblives--;
		Spawn();
	}
	
	public void Spawn() {
		health = maxHealth;
		transform.position = startPos;
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