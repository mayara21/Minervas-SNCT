using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour {

	#region Events
	public event Action OnGetCoin;
    public event Action SaveScore;
    public event Action OnHitObstacle;
	#endregion

	#region Components
	private Rigidbody2D myRigidBody2D;
	private BoxCollider2D myCollider;
	private Animator myAnimator;
	#endregion

	#region Números Mágicos
	[SerializeField] private float speed = 8f;
	[SerializeField] private float jumpThrust = 23f;
	[SerializeField] private float stunTime = 2f;
	[SerializeField] private int playerID;
    [SerializeField] private bool patriarchyVictim;

	#endregion

	private bool canMove = true;
    private bool isJumping = false;
    private bool canSave = true;
	
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
        isJumping = Mathf.Abs(myRigidBody2D.velocity.y) > 0.0;

		myRigidBody2D.velocity = new Vector2(speed, myRigidBody2D.velocity.y);
        //if (canMove && !isJumping) print("oi");
        myAnimator.SetBool("Running", canMove && !isJumping);
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

        if (other.gameObject.CompareTag("Finish Line") && canSave) {
            canMove = false;
            canSave = false;
            if (SaveScore != null) SaveScore();
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
        if (OnHitObstacle != null) OnHitObstacle();
		yield return new WaitForSeconds(stunTime);
		canMove = true;
	}

}
