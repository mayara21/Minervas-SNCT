using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour {

	#region Events
	public event Action OnGetCoin;
	#endregion

	#region Components
	private Rigidbody2D myRigidBody2D;
	private BoxCollider2D myCollider;
	private Animator myAnimator;
	#endregion

	#region Números Mágicos
	[SerializeField] private float speed = 5f;
	[SerializeField] private float jumpThrust = 15f;
	[SerializeField] private float stunTime = 2f;
	[SerializeField] private int playerID;

	#endregion

	private bool canMove = true;
	
	private void Start ()
	{
		myRigidBody2D = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<BoxCollider2D>();
		myAnimator = GetComponent<Animator>();
	}
	
	private void FixedUpdate ()
	{
		if(canMove) Move();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"+playerID) && myCollider.IsTouchingLayers(LayerMask.GetMask("Floor")) && canMove) Jump();

	}

	private void Move()
	{
		myRigidBody2D.velocity = new Vector2(speed, myRigidBody2D.velocity.y);
	}

	private void Jump()
	{
		myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpThrust);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//print(other.gameObject.name);
		if (other.gameObject.CompareTag("Item"))
		{
			Destroy(other.gameObject);
			if(OnGetCoin != null)
				OnGetCoin();
		} 
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Obstacle"))
		{
			StartCoroutine(HitObstacle(other));
		}
	}

	private IEnumerator HitObstacle(Collision2D other)
	{
		canMove = false;
		Destroy(other.gameObject);
		yield return new WaitForSeconds(stunTime);
		canMove = true;
	}
}
