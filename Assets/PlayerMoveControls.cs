using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private GatherInput gatherInput;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private int direction = 1; // to right-hand side

    public float jumpForce;

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = false;

    public bool knockBack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }
    private void FixedUpdate()
    {
        CheckStatus();

        if(knockBack) return;

        Move();
        JumpPlayer();
    }
    private void Move() 
    {
        Flip();
        rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.velocity.y);
    }
    private void Flip() 
    { 
        if(gatherInput.valueX * direction < 0) 
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
    private void SetAnimatorValues() 
    {
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }
    private void JumpPlayer() 
    {
        if (gatherInput.jumpInput && grounded)
        {
            rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }
    private void CheckStatus() {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;
    }

    public IEnumerator KnockBack(float forceX, float forceY,float duration, Transform otherObject)
    {
        int knockBackDirection;
        if(transform.position.x < otherObject.position.x)
        {
            knockBackDirection = -1;
        }
        else
        {
            knockBackDirection = 1;
        }

        knockBack = true;
        rigidbody2D.velocity = Vector2.zero;
        Vector2 theForce = new Vector2(forceX * knockBackDirection, forceY);
        rigidbody2D.AddForce(theForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);
        knockBack = false;
        rigidbody2D.velocity = Vector2.zero;
    }
}
