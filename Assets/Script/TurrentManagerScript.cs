using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurrentManagerScript : MonoBehaviour {

    private Animator _animator;

    const int DirectionCount = 8;
    public Ease RotateEaseFunction;
    public float rotateDuration;

    public Camera GameCamera;
    public float CameraShakeDuration;
    public float CameraShakeStregth;

    public GameObject bulletCandidate;
    private float bulletOffest = 0.6f;

    public ScoreManager scoreManager;
    public GameLoopManager gameLoopManager;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayShootAnimation();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    PlayerRotateAnimation();
        //}
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger("Shoot");
        GameCamera.transform.DOShakePosition(CameraShakeDuration, CameraShakeStregth);

        scoreManager.AddScore(1);

        GameObject bulletobj = GameObject.Instantiate(bulletCandidate);
        BulletScript bulletScript = bulletobj.GetComponent<BulletScript>();
        bulletScript.transform.position = this.transform.position + bulletOffest * this.gameObject.transform.right;
        bulletScript.transform.rotation = this.transform.rotation;
        Vector3 shootDirection3D = this.gameObject.transform.right;
        Vector2 shootDirection2D = new Vector2(shootDirection3D.x, shootDirection3D.y);
        bulletScript.InitAndShoot(shootDirection2D);

        gameLoopManager.bullets.Add(bulletScript);
    }

    public void PlayerRotateAnimation()
    {
        float targetDegree = 360.0f / DirectionCount * Random.Range(0, DirectionCount);
        this.transform.DORotate(new Vector3(0, 0, targetDegree), rotateDuration);
    }
}
