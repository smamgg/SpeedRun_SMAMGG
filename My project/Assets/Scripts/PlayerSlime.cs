using System;
using UnityEngine;



public class PlayerSlime : MonoBehaviour
{
    public enum PlayerState
    {
        Normal,
        Bouncing,
        Jumping
    }


    Rigidbody2D rigid; 

    public float speed = 2f; 
    public float accelRight = 1f; 
    public float accelLeft = 1f; 
    public float accel;

    public PlayerState currentState = PlayerState.Normal;

    public float bounceTimer = 0f;

    public float moveHorizontal;

    public bool isGrounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        UpdateLengthBySpeed();
        moveHorizontal = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        switch (currentState)
        {
            case PlayerState.Normal:
                if (moveHorizontal > 0f)
                {
                    rigid.linearVelocity = new Vector2(moveHorizontal * speed + moveHorizontal * accelRight, rigid.linearVelocity.y);
                    accelRight = Mathf.Min(accelRight + Time.deltaTime, 10f);
                    accelLeft = Mathf.Max(accelLeft - Time.deltaTime * 6, 1f);
                }
                else if (moveHorizontal < 0f)
                {
                    rigid.linearVelocity = new Vector2(moveHorizontal * speed + moveHorizontal * accelLeft, rigid.linearVelocity.y);
                    accelLeft = Mathf.Min(accelLeft + Time.deltaTime, 10f);
                    accelRight = Mathf.Max(accelRight - Time.deltaTime * 6, 1f);
                }
                else
                {
                    accelRight = Mathf.Max(accelRight - Time.deltaTime * 5f, 1f);
                    accelLeft = Mathf.Max(accelLeft - Time.deltaTime * 5f, 1f);
                }
                break;
            case PlayerState.Bouncing:

                break;
            case PlayerState.Jumping:
                if (moveHorizontal > 0f)
                {
                    rigid.linearVelocity = new Vector2(moveHorizontal * speed + moveHorizontal * accelRight, rigid.linearVelocity.y);
                }
                else if (moveHorizontal < 0f)
                {
                    rigid.linearVelocity = new Vector2(moveHorizontal * speed + moveHorizontal * accelLeft, rigid.linearVelocity.y);
                }
                else
                {
                    accelRight = Mathf.Max(accelRight - Time.deltaTime * 5f, 1f);
                    accelLeft = Mathf.Max(accelLeft - Time.deltaTime * 5f, 1f);
                }
                break;
        }
    }

    void Jump()
    {
        float jumpPower = Mathf.Clamp(accel, 5f, 10f);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
            accelLeft = Mathf.Max(accelLeft - accelLeft * 0.2f, 1f);
            accelRight = Mathf.Max(accelRight - accelRight * 0.2f, 1f);
            SetState(PlayerState.Jumping);
        }
    }

    void UpdateLengthBySpeed()
    {
        accel = Mathf.Max(accelRight, accelLeft); // which is bigger 
        float scaleFactor = Mathf.InverseLerp(1f, 5f, accel); // Normalize from 1 to 20 to 0 to 1 
        float targetLength = Mathf.Lerp(1f, 10f, scaleFactor);  // Length scale : 1times ~ 6times 

        transform.localScale = new Vector3(targetLength, 1f, 1f); 
    }

    public void SetState(PlayerState newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        switch (currentState)
        {
            case PlayerState.Normal:
                
                break;
            case PlayerState.Bouncing:
                bounceTimer = 0.5f;
                break;
            case PlayerState.Jumping:
                
                break;
            }
    }
}
