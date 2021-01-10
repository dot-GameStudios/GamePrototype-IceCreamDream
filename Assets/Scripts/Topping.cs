using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topping : MonoBehaviour
{
    public EmotionType currentEmotion;
 
    public SpriteRenderer emotionIconSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEmotion(EmotionType emotion) //Emotion Enum used to set the emotion icon of the topping
    {
        currentEmotion = emotion;
    }
    
    public void SetEmotionWordSprite(Sprite emotionWord)  //Emotion Enum used to set the emotion word of the topping
    {
        emotionIconSprite.sprite = emotionWord;
    }

}
