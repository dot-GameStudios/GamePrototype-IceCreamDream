using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Data gameData;
    private GameObject[] toppings;
    private GameObject iceCreamCone;
    
    public string scoreNode;
    public string mistakeNode;
    public string gameLostNode;

    public EmotionType targetEmotion;
    
    public Image targetEmotionIcon;
    public Image targetEmotionWord;

    public UnityEvent OnLoss;
    
    // Start is called before the first frame update
    void Start()
    {
        gameData = GetComponent<Data>();
        
        targetEmotion = (EmotionType)Random.Range(1, 10);
        
        Time.timeScale = 1;

        SetEmotionUI();
        SetIceCreamEmotion();
        SetToppingEmotion();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameData.GetBool(gameLostNode).Value)
        {
            LossScreen();
        }
    }

    public void ToppingCollision()
    {
        MatchCheck(iceCreamCone.GetComponent<IceCreamController>().CompareEmotion());
    }

    public void MatchCheck(bool correctMatch) //a bool for whether the player has made a correct match or not
    {
        if (correctMatch)
        {
            AddScore(100);
        }
        else
        {
            AddScore(-50);
            AddMistake();
            FindObjectOfType<MistakeCounter>().GetComponent<MistakeCounter>().AddMistake();
        }
    }

    public void AddMistake() //string to find the mistakeNode in the gameData script
    {
        gameData.GetInt(mistakeNode).Value++;
        LossCheck();
    }

    public void AddScore(int addedScore) //an int value of how much you want to add to the score
    {
        gameData.GetInt(scoreNode).Value += addedScore;

        if(gameData.GetInt(scoreNode).Value < 0)
        {
            gameData.GetInt(scoreNode).Value = 0;
        }
    }

    public void SetEmotionUI()
    {
        targetEmotionIcon.sprite = GetComponent<EmotionHolder>().GetEmotionIcon(targetEmotion);
        targetEmotionWord.sprite = GetComponent<EmotionHolder>().GetEmotionWord(targetEmotion);
    }

    public void SetIceCreamEmotion()
    {
        //finds IceCreamCone, sets the target emotion and its corresponding word
        iceCreamCone = GameObject.FindGameObjectWithTag("Player");
        iceCreamCone.GetComponent<IceCreamController>().SetEmotion(targetEmotion);
        iceCreamCone.GetComponentInChildren<IceCreamController>().SetEmotionWordSprite(GetComponent<EmotionHolder>().GetEmotionWord(targetEmotion));
    }

    public void SetToppingEmotion()
    {
        toppings = GameObject.FindGameObjectsWithTag("Topping");
        
        for( int i=0; i < toppings.Length; i++)
        {
            EmotionType randomEmotion = (EmotionType)Random.Range(1, 10);
            toppings[i].GetComponent<Topping>().SetEmotion(randomEmotion);
            toppings[i].GetComponent<Topping>().SetEmotionWordSprite(GetComponent<EmotionHolder>().GetEmotionIcon(randomEmotion));
        }
    }

    public void LossCheck()
    {
        if(gameData.GetInt(mistakeNode).Value >= 3)
        {
            gameData.GetBool(gameLostNode).Value = true;
        }
    }

    public void LossScreen()
    {
        OnLoss.Invoke();
    }
}
