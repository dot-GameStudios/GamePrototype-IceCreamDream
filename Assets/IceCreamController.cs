﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamController : MonoBehaviour
{
    private MonoBehaviour gameManagerScript;
    private Rigidbody2DTrigger iceCreamRB2DTrigger;
    public EmotionType currentEmotion;
    public SpriteRenderer emotionWordSprite;
    // Start is called before the first frame update
    void Start()
    {
        iceCreamRB2DTrigger = GetComponent<Rigidbody2DTrigger>();
        gameManagerScript = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEmotion(EmotionType emotion)
    {
        currentEmotion = emotion;
    }

    public void SetEmotionWordSprite(Sprite emotionWord)
    {
        emotionWordSprite.sprite = emotionWord;
    }

    public bool CompareEmotion()
    {
        if(iceCreamRB2DTrigger.Collider.gameObject.GetComponent<Topping>().currentEmotion == currentEmotion)
        {
            Destroy(iceCreamRB2DTrigger.Collider.gameObject);
            return true;
            
        }
        else
        {
            Destroy(iceCreamRB2DTrigger.Collider.gameObject);
            return false;
        }
    }
}
