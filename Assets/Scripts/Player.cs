using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] CharacterController2D controller;
    [SerializeField] float movSpeed = 5f;
    [SerializeField] float verticalSpeed = 3f;
    [SerializeField] Vector2 deathKnockback = new Vector2(0, 1f);
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;
    float horizontalMove = 0f;
    bool jump = false;
    float startingGravity;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        startingGravity = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            controller.Move(horizontalMove, false, jump);
            jump = false;
        }
    }

    void Update()
    {
        Movement();
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = startingGravity;
            return;
        }
        else
        {
            float verticalMove = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
            Vector2 climbVel = new Vector2(myRigidBody.velocity.x, verticalMove);
            myRigidBody.velocity = climbVel;
            myRigidBody.gravityScale = 0f;
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
    }

    private void Movement()
    {
        horizontalMove = Input.GetAxis("Horizontal") * movSpeed * Time.fixedDeltaTime;
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    IEnumerator Die()
    {
        myAnimator.SetTrigger("Die");
        isAlive = false;
        myRigidBody.velocity = deathKnockback;
        yield return new WaitForSeconds(2);
        myRigidBody.velocity = new Vector2(0, 0);
        FindObjectOfType<GameSession>().PlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.transform.tag == "Enemy" || myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) && isAlive)
        {
            StartCoroutine(Die());
        }
    }

}
