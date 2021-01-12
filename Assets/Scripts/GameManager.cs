using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Data gameData;
    private List<GameObject> toppings = new List<GameObject>();
    private GameObject iceCreamCone;
    private bool toppingsCanSpawn;
    private int waveCounter;

    [Header("Important Nodes")]
    public string scoreNode;
    public string mistakeNode;
    public string numberOfWaves;
    public string gameLostNode;
    public string gameFinishedNode;
    public string maxNumberOfToppings;

    [Header("Toppings")]
    public GameObject GummyBearGreen;
    public GameObject GummyBearYellow;
    public GameObject GummyBearRed;
    public GameObject Cherry;
    public GameObject spawnZone;

    [Header("Emotion Information")]
    public EmotionType targetEmotion;
    public Image targetEmotionIcon;
    public Image targetEmotionWord;

    [Header("Events")]
    public UnityEvent OnLoss; //2 seperate events for winning or losing, its not useful here but could be useful for more specific situations
    public UnityEvent OnFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        gameData = GetComponent<Data>();
        
        targetEmotion = (EmotionType)Random.Range(1, 10);

        toppingsCanSpawn = true;

        waveCounter = 0;
        FindObjectOfType<Menu>().SetWaveMaximum(gameData.GetInt(numberOfWaves).Value);

        SetEmotionUI();
        SetIceCreamEmotion();
        StartNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameData.GetBool(gameLostNode).Value) //checks for lose condition
        {
            LossScreen();
        }
        
        ToppingListCheck();
   
        if(toppings.Count <= 0 && toppingsCanSpawn == true) //checks if there are no active toppings in the scene
        {
            waveCounter++;
            
            WinCheck();
            
            if (gameData.GetBool(gameFinishedNode).Value) //if win condition is met, 
            {
                WinScreen();
            }
            else
            {
                StartNewWave();
            }

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
    {   //for loop that randomly gives all active toppings a random emotion
        for( int i=0; i < toppings.Count; i++)
        {
            EmotionType randomEmotion = (EmotionType)Random.Range(1, 10);
            toppings[i].GetComponent<Topping>().SetEmotion(randomEmotion);
            toppings[i].GetComponent<Topping>().SetEmotionWordSprite(GetComponent<EmotionHolder>().GetEmotionIcon(randomEmotion));
        }
    }

    public void StartNewWave()
    {
        for(int i =0; i < gameData.GetInt(maxNumberOfToppings).Value; i++)
        {
            int randomPrefabSelect = Random.Range(1, 5); //random int to find out which prefab to spawn
            
            //a float that gets a random x position based on the boxCollider2D of spawnZone
            float randomXSpawnPoint = Random.Range(spawnZone.GetComponent<BoxCollider2D>().bounds.min.x, spawnZone.GetComponent<BoxCollider2D>().bounds.max.x + 1); 
            
            Vector2 spawnPosition = new Vector2(randomXSpawnPoint, spawnZone.GetComponent<Transform>().position.y); //random spawn position vector
            
            switch (randomPrefabSelect)
            {
                case 1:
                    Instantiate(Cherry, spawnPosition, Cherry.GetComponent<Transform>().rotation);
                    break;
                case 2:
                    Instantiate(GummyBearRed, spawnPosition, GummyBearRed.GetComponent<Transform>().rotation);
                    break;
                case 3:
                    Instantiate(GummyBearYellow, spawnPosition, GummyBearYellow.GetComponent<Transform>().rotation);
                    break;
                case 4:
                    Instantiate(GummyBearGreen, spawnPosition, GummyBearGreen.GetComponent<Transform>().rotation);
                    break;
            }
        }

        toppings = GameObject.FindGameObjectsWithTag("Topping").ToList();

        FindObjectOfType<Menu>().SetWaveCounter(waveCounter);
        
        SetToppingEmotion();
    }

    public void LossCheck()
    {
        if(gameData.GetInt(mistakeNode).Value >= 3)
        {
            gameData.GetBool(gameLostNode).Value = true;
            toppingsCanSpawn = false;
        }
    }

    public void WinCheck()
    {
        if (gameData.GetInt(numberOfWaves).Value == waveCounter)
        {
            gameData.GetBool(gameFinishedNode).Value = true;
            toppingsCanSpawn = false;
        }
    }

    public void ToppingListCheck()
    {
        //A loop that checks if any item in an index is null, if it is, remove from the list
        for(int i =0; i < toppings.Count; i++)
        {
            if(toppings[i] == null)
            {
                toppings.RemoveAt(i);
            }
        }
    }

    public void LossScreen()
    {
        OnLoss.Invoke();
    }

    public void WinScreen()
    {
        OnFinished.Invoke();
    }

    public void ShowResults()
    {
        FindObjectOfType<Menu>().SetRewardText(gameData.GetInt(scoreNode).Value);
    }

    
}
