using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionType
{
    NONE,
    LOVE,
    LAUGHTER,
    HAPPY,
    KINDNESS,
    SAD,
    IAMSORRY,
    SILLY,
    GRUMPY,
    ANGRY
}

public class GameManager : MonoBehaviour
{
    private Data gameData;

    public int mistakeCounter;
    public int scoreCounter;
    public EmotionType targetEmotion;

    // Start is called before the first frame update
    void Start()
    {
        gameData = GetComponent<Data>();
        //targetEmotion = Random.Range(1, 10);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
