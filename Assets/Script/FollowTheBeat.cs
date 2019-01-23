using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBeat : MonoBehaviour {

    //節奏時間距離
    private const float beatPeriod = 1.485f;

    //拍點提前0.2s旋轉
    private float rotateCounter = 0.2f;

    //前面3次拍點都不做事，延遲0.5秒發射
    private float shootCounter = -0.5f - beatPeriod*3;
    private TurrentManagerScript turrent;

    public void Reset()
    {
        shootCounter = -0.5f - beatPeriod * 3;
        rotateCounter = 0.2f;
    }

    // Use this for initialization
    void Start () {
        turrent = this.GetComponent<TurrentManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        rotateCounter += Time.deltaTime;
        shootCounter += Time.deltaTime;

        if(rotateCounter > beatPeriod)
        {
            turrent.PlayerRotateAnimation();
            rotateCounter -= beatPeriod;
        }
        if(shootCounter > beatPeriod)
        {
            turrent.PlayShootAnimation();
            shootCounter -= beatPeriod;
        }
    }
}
