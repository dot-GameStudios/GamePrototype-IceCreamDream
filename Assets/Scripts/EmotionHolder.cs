using System;
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

[Serializable]
public class EmotionState
{
    public EmotionType emotion; 
    public Sprite emotionIcon;
    public Sprite emotionWord;
}

public class EmotionHolder : MonoBehaviour
{
    public EmotionState[] emotions; //Holds the corresponding, enum, and sprites for each emotion

    public EmotionState GetEmotion(EmotionType emotionName) //Enum of what emotion you are looking for
    {
        for (int i = 0; i < emotions.Length; i++)
        {
            if (emotions[i].emotion == emotionName)
            {
                return emotions[i]; //returns emotionState when it has been found
            }
        }
        return null; //return nothing if the attempt failed
    }

    public Sprite GetEmotionIcon(EmotionType emotionName) //Enum of what emotion you are looking for
    {
        return GetEmotion(emotionName).emotionIcon; //uses GetEmotion() method to get reference of the emotion and then returns the Icon that corresponds
    }

    public Sprite GetEmotionWord(EmotionType emotionName) //Enum of what emotion you are looking for
    {
        return GetEmotion(emotionName).emotionWord; //uses GetEmotion() method to get reference of the emotion and then returns the Icon that corresponds
    }
}
