using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public SpriteRenderer rend;
    public CapsuleCollider2D col;
    public float Speed = 5;
    public float JumpSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * Speed;
        rb2d.velocity = new Vector2(h, rb2d.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0")){
            rb2d.AddForce(transform.up * JumpSpeed, ForceMode2D.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if(h > 0){
            rend.flipX = false;
        }
        else if(h < 0){
            rend.flipX = true;
        }
    }
}
