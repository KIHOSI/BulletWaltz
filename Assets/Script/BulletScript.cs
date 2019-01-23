using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRender;

    //速度調慢一點
    private float speed = 2;

    const float flashDuration = 0.1f;
    float flashCounter = 0;

    public void InitAndShoot(Vector2 direction)
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRender = this.GetComponent<SpriteRenderer>();

        spriteRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        rigidbody2D.velocity = speed * direction;

        flashCounter = flashDuration;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D.velocity == Vector2.zero)
        {
            //確保沒人停下來
            rigidbody2D.velocity = new Vector2(Random.Range(0, 1.0f), Random.Range(0, 1.0f)).normalized * speed;
        }
        else 
        {
            //確保碰撞後速度不變
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }

        float rotationZ = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
        Debug.Log(rotationZ);
        this.transform.eulerAngles = new Vector3(0, 0, rotationZ);

        if(flashCounter > 0) 
        {
            flashCounter -= Time.deltaTime;
            spriteRender.color = Color.white;
        }
        else {
            spriteRender.color = Color.green;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        flashCounter = flashDuration;
    }
}
