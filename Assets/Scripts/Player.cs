using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    private Animator anim;
    // Start is called before the first frame update

    public bool isJumping;
    public bool doubleJump;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        anim.SetFloat("yVelocity", rig.velocity.y);
    }

    void Move()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * Speed;

        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        if(movement > 0f){            
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(movement < 0f){
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if(movement == 0f){
            anim.SetBool("run", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
        {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
                

        }else{
            if(doubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce*1.2f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }

        }
    }
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Damage")
        {
            anim.SetTrigger("hit");
        }
        
        
    }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Damage")
    //     {
            
            

    //     }
    // }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);

        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
            anim.SetBool("jump", true);

        }
    }


   
}
