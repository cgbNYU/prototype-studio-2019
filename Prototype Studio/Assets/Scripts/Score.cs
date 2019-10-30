using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float _score;

    public static Score Instance;

    public Text ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        //singleton
        //Set up the singleton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        //reset score
        _score = 0;
        
        //score text
        ScoreText.text = _score.ToString();
    }

    //call from other functions to add score
    public void AddScore(float newScore)
    {
        _score += newScore;
        ScoreText.text = _score.ToString();
    }
}
