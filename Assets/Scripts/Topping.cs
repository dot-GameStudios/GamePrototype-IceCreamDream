using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topping : MonoBehaviour
{
    public EmotionType currentEmotion;
    public Rigidbody2DTrigger RB2DTrigger;
    public SpriteRenderer emotionIconSprite;

    public void SetEmotion(EmotionType emotion) //Emotion Enum used to set the emotion icon of the topping
    {
        currentEmotion = emotion;
    }
    
    public void SetEmotionWordSprite(Sprite emotionWord)  //Emotion Enum used to set the emotion word of the topping
    {
        emotionIconSprite.sprite = emotionWord;
    }

    public void DestroySelf(string targetTag) //what is the tag of the object you want to check
    {
        if(RB2DTrigger.CollTag == targetTag)
        { 
            Destroy(gameObject);
        }
    }
}
