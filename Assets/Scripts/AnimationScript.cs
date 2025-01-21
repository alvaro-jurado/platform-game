using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;

    public float wallOffset = 0.1f;

    private Vector3 originalPosition;
    private bool positionAdjusted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();

        originalPosition = transform.localPosition;
    }

    void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);

        if (move.wallGrab || move.wallSlide)
        {
            AdjustWallPosition();
        }
        else if (positionAdjusted)
        {
            RestoreOriginalPosition();
        }
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        if (move.wallGrab || move.wallSlide)
        {
            sr.flipX = (coll.onRightWall) ? false : true;
            return;
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }

    private void AdjustWallPosition()
    {
        if (!positionAdjusted)
        {
            float offset = coll.onRightWall ? wallOffset : -wallOffset;

            transform.localPosition = new Vector3(
                originalPosition.x + offset,
                originalPosition.y,
                originalPosition.z
            );

            positionAdjusted = true;
        }
    }

    private void RestoreOriginalPosition()
    {
        transform.localPosition = originalPosition;
        positionAdjusted = false;
    }
}
