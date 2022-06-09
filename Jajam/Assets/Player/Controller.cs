using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public SpriteRenderer rend;
    public CapsuleCollider2D col;
    public Animator anim;
    
    public float Speed = 5;
    public float JumpSpeed = 15;
    public float Life;
    public bool CanDoubleJump;

    public bool IsJumping;
    public bool IsGrounded;
    public bool IsRunning;
    public float VelocityY;

    public bool HasRainHability;
    public bool HasJumpHability;
    public bool IsRainOn;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    

    // Start is called before the first frame update

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        IsGrounded = true;
        IsJumping = false;
        IsRunning = false;
        CanDoubleJump = false;
        HasRainHability = false;
        HasJumpHability = false;
        IsRainOn = false;
        Life = 100;

    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovements();
        Animation();


    }
    void Animation()
    {
        float h = Input.GetAxis("Horizontal") * Speed;
        rb2d.velocity = new Vector2(h, rb2d.velocity.y);
        VelocityY = rb2d.velocity.y;
        //Set des variables pour les animations du personnage
        anim.SetBool("isGrounded", IsGrounded);
        anim.SetFloat("VelocityY", rb2d.velocity.y);
        if(h == 0){
            anim.SetBool("isRunning", false);
                    IsRunning = false;
        }
        else{
            anim.SetBool("isRunning", true);
                IsRunning = true;
        }

        //Flip du sprite quand le player va à gauche ou à droite
        if(h > 0){
            rend.flipX = false;
        }
        else if(h < 0){
            rend.flipX = true;
        }

        //Animations du player

        if(rb2d.velocity.y != 0){
            IsJumping = true;
        }
        else{
            IsJumping = false;
        }
    }
    void PlayerMovements()
    {
        IsGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if(Input.GetButtonDown("Jump")){
            if(IsGrounded){
                rb2d.AddForce(transform.up * JumpSpeed, ForceMode2D.Impulse);
                if(HasJumpHability){
                    CanDoubleJump = true;
                }
                
            }else{
                if(CanDoubleJump){
                    rb2d.AddForce(transform.up * JumpSpeed, ForceMode2D.Impulse);
                    CanDoubleJump = false;
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            RainHability();
        }
    }
    public void die(){
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }
    void RainHability()
    {
        IEnumerator ActiveRain()
        {
            IsRainOn = true;
            Debug.Log("Rain is on");
            GameObject.Find("Rain").GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = true;
            GameObject.Find("filtre 1").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("filtre 2").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("UI/Canvas/Buttons/UpWaterButton").GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 0.5f);
            yield return new WaitForSeconds(3);
            Debug.Log("Rain is off");
            IsRainOn = false;
            GameObject.Find("Rain").GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = false;
            GameObject.Find("filtre 1").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("filtre 2").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("UI/Canvas/Buttons/UpWaterButton").GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 1);
        }
        if(HasRainHability == true){
            if(IsRainOn == false){
                StartCoroutine(ActiveRain());
                ActiveRain();
            }
        }else{
            Debug.Log("No Rain!!");
        }
    }


}