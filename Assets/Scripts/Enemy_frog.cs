using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    private Rigidbody2D rb;
    public Transform rightPoint, leftPoint;
    private bool faceLeft = true;
    private float leftx, rightx;
    public float speed,jumpForce;

    //private Animator anim;
    private Collider2D coll;
    public LayerMask Ground;

    // Start is called before the first frame update
    protected override void  Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        //获取值后可以消除了
        Destroy(rightPoint.gameObject);
        Destroy(leftPoint.gameObject);

        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        SwitchAnim();
    }

    void Move()
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(Ground))//判断是否在地面
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }

        else
        {

            if (coll.IsTouchingLayers(Ground))//判断是否在地面
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    
    void SwitchAnim()
    {
        base.Start();//这里为了得到Animator组件一定要写
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }

        if (coll.IsTouchingLayers(Ground)&&anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }

    }
    

}
