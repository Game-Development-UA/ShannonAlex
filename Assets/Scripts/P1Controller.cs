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
    public int jump_counter = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizonatal_mvmt = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        charac.velocity = new Vector2(speed * horizonatal_mvmt, charac.velocity.y);

        //RaycastHit hit;
        if (Input.GetButtonDown("Jump"))
        {
            /*if (jump1 == false)
            {
                charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                jump1 = true;
            }
            //charac.position = charac.position;
            else
                if (jump2 == false)
                {
                    charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    jump2 = true;
                }
                else
                {
                    charac.position = charac.position;
                    jump1 = false;
                    jump2 = false; //raycasting
            }
        */
            /*if (jump_counter == 0 || jump_counter == 1)
            {
                if (Physics.Raycast(transform.position, Vector3.down, 100))
                {
                    charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    jump_counter += 1;
                }
                else
                {
                    jump_counter += 1;
                    if (Physics.Raycast(transform.position, transform.forward, 1))
                    {
                        jump_counter = 0;
                    }
                }
            }*/
            if(charac.position.y < 8f)
            {
                charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            }

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

        if (moreLife != null){life -= moreLife.lifeIncrease; Destroy(moreLife.gameObject);}

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
