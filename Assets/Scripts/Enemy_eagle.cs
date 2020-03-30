using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{

    private bool upward = true;//判断是否往上飞

    private Rigidbody2D rb;
    public Transform upPoint, downPoint;
    public float upSpeed = 5;
    private float upy, downy;
    //private Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        upy = upPoint.position.y;
        downy = downPoint.position.y;
        float freezeX = transform.position.x;
        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (upward)
        {
            if (rb.position.y > upy)
            {
                upward = false;
                return;
            }
                
            rb.velocity = new Vector2(transform.position.x, upSpeed);
            
        }
        else
        {
            if (rb.position.y < downy)
            {
                upward = true;
                return;
            }
            rb.velocity = new Vector2(transform.position.x, -upSpeed);
        }
    }

   

}
