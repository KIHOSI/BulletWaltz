using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private int _score = 0;
    private Text _uiText;

    public void Reset()
    {
        _score = 0;
        _uiText.text = _score.ToString();
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiText.text = _score.ToString();
    }

    // Use this for initialization
    void Start () {
		_uiText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
