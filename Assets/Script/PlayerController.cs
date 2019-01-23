using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody2D;
    public float forceValue;
    public float maxSpeed;
    public float decreasingSpeed;
    public ParticleSystem playerKillEffect;

    public AudioSource playerKillAudio;

    public UnityEvent playerKilledEvent;

    public void Reset()
    {
        this.transform.localPosition = new Vector3(2, 0, 0);
        this.gameObject.SetActive(true);
        playerKillEffect.Stop();
        playerKillEffect.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerKillAudio.Play();
        this.gameObject.SetActive(false);
        playerKillEffect.transform.position = this.transform.position;
        playerKillEffect.gameObject.SetActive(true);

        if(playerKilledEvent != null)
        {
            playerKilledEvent.Invoke();
        }
    }

    // Use this for initialization
    void Start () {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //根據input計算力量方向
        Vector2 force = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            force += new Vector2(0, forceValue);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            force += new Vector2(0, -forceValue);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            force += new Vector2(forceValue,0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            force += new Vector2(-forceValue,0);
        }

        if(force != Vector2.zero)
        {
            rigidbody2D.AddForce(force);
            float speed = rigidbody2D.velocity.magnitude; //回傳速度vector length
            if(speed > maxSpeed) //確保速度不超過最大速度
            {
                speed = maxSpeed;
            }
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }
        else //無input時減速
        {
            float speed = rigidbody2D.velocity.magnitude;
            speed -= decreasingSpeed * Time.deltaTime;
            if (speed < 0)
            {
                speed = 0;
            }
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }

        //計算方塊面對方向
        if(rigidbody2D.velocity == Vector2.zero || force == Vector2.zero)
        {
            this.transform.eulerAngles = Vector3.zero; //eulerAngles，歐拉角
        }
        else
        {
            float rotationZ = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
            //atan2是正切函數的變種，atan2(y,x)代表的意義是以原點為起點，指向坐標(x,y)連成的線與x軸正方向之間的角度
            //Rad2Deg，Radians-to-degrees conversion constant，This is equal to 360 / (PI * 2).
            this.transform.eulerAngles = new Vector3(0, 0, rotationZ);
        }
    }
}
