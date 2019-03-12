using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    float horizonatal_mvmt;
    //float vertical;
    public float jump;
    public float speed;
    public float life;
    public Rigidbody2D charac;
    public ParticleSystem blood;
    public Transform spawn;
    public ParticleSystem explosion;
    //public bool jump1 = false;
    //public bool jump2= false;
    public int maxJumps;
    int jump_counter;
    public Transform groundedRaycastOrigin;
    public float groundedRayLength;
    private SpriteRenderer SR;

    void Update()
    {
        horizonatal_mvmt = Input.GetAxis("Horizontal");
        charac.velocity = new Vector2(speed * horizonatal_mvmt, charac.velocity.y);

        RaycastHit2D grounded = Physics2D.Raycast( groundedRaycastOrigin.position, groundedRaycastOrigin.right, groundedRayLength );
        if( grounded.collider != null ) {
            if( jump_counter == maxJumps ) jump_counter = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if( jump_counter < maxJumps )
            {
                charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                jump_counter++;
            }
        }
        if (charac.position.y < -20)
        {
            Instantiate(explosion, spawn);
            Destroy(charac.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
        }
        GameObject appearance = GameObject.Find("Player/Appearance");//Grabs child object of player
        SR = appearance.GetComponent<SpriteRenderer>(); //Grabes sprite renderer of apparance

        //Acitvates flipx based on button pressed.
        if (Input.GetAxis("Horizontal") < 0)
        {
            SR.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            SR.flipX = false;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SpeedPowerup moreSpeed = col.gameObject.GetComponent<SpeedPowerup>();
        LifePowerUp moreLife = col.gameObject.GetComponent<LifePowerUp>();
        LifeDecrease lessLife = col.gameObject.GetComponent<LifeDecrease>();

        if (col.gameObject.tag == "endgame")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
        }

        if (moreSpeed != null){speed += moreSpeed.speedIncrease; Destroy(moreSpeed.gameObject);}

        if (moreLife != null){life += moreLife.lifeIncrease; Destroy(moreLife.gameObject);}

        if (lessLife != null)
        {
            if (life == 0 || life - lessLife.lifeDecrease<=0)
            {
                Instantiate(explosion, spawn);
                Destroy(charac.gameObject);
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
            }
            else
            {
                life -= lessLife.lifeDecrease; Destroy(lessLife.gameObject);
                if (horizonatal_mvmt > 0)
                {
                    Instantiate(blood, spawn);
                    charac.position = new Vector2(charac.position.x - 3, charac.position.y);
                }
                else
                {
                    Instantiate(blood, spawn);
                    charac.position = new Vector2(charac.position.x + 3, charac.position.y);
                }

            }
            
        }

        //FIREBALL - THROW TO DESTROY OBJECT
        //If enemy is hit, decrease life

    }

}
