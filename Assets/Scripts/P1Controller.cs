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

        if (Input.GetButtonDown("Jump"))
        {
            charac.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        SpeedPowerup moreSpeed = col.gameObject.GetComponent<SpeedPowerup>();
        LifePowerUp moreLife = col.gameObject.GetComponent<LifePowerUp>();

        if (moreSpeed != null){speed += moreSpeed.speedIncrease; Destroy(moreSpeed.gameObject);}

        if (moreLife != null){life -= moreLife.lifeIncrease; Destroy(moreLife.gameObject);}

        //FIREBALL - THROW TO DESTROY OBJECT

    }
}
