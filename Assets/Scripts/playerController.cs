using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public float jumpforce = 20, speed = 10, hurtforce = 10;

    public Transform groundCheck;
    public LayerMask ground;//碰撞体过滤 

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    private int cherryNum;
    private int gemNum;

    //UI
    public Text cherryNumText;
    public Text gemNumberText;

    //state
    private bool isHurt;//default:false


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        cherryNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        
    }

    //每秒固定执行的操作，与设备无关
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);//判断是否在地面
        if (!isHurt)
        {
            GroundMovement();
        }
       // GroundMovement();
        SwitchAnim();
        Jump();
    }

    //根据输入控制水平方向的移动和以及方向变化
    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");//只有0 -1 1 三个数字
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);//控制翻转
        }
       
    }

    void Jump()
    {
        
        if (isGround)
        {
            jumpCount = 2;//在地面的时候就恢复可跳跃次数
            isJump = false;
        }

        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x*Time.deltaTime, jumpforce);
            jumpCount--;
            jumpPressed = false;//每0.02秒检测按键是否松开
            
        }

        else if (jumpPressed && jumpCount > 0 && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x * Time.deltaTime, jumpforce);
            jumpCount--;
            jumpPressed = false;
            
        }

    }

    //控制动画转移
    void SwitchAnim()
    {
        //判断是否跑
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        //判断下落还是上跳
        anim.SetFloat("jumping", rb.velocity.y);


        if (isHurt)
        {
            anim.SetBool("isHurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                isHurt = false;
                anim.SetBool("isHurt", false);
            }
        }

        if (isGround)
        {
            anim.SetBool("idle", true);
        }
        else
            anim.SetBool("idle", false);

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cherry")
        {
            Destroy(collision.gameObject);
            cherryNum++;
            cherryNumText.text = cherryNum.ToString();
        }


        if (collision.tag == "gem")
        {
            Destroy(collision.gameObject);
            gemNum++;
            gemNumberText.text=gemNum.ToString();

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "frog")
        {
            //下落时碰撞
            if (rb.velocity.y<0&&Mathf.Abs(rb.velocity.x)<2)
            {
                //Destroy(collision.collider);

                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x * Time.deltaTime, jumpforce);
            }
            //向右时碰撞
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                isHurt = true;
            }

            //向左时碰撞
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                isHurt = true;
            }
        }
    }



}
