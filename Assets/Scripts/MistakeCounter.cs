using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeCounter : MonoBehaviour
{
    private int mistakeIconIndex;

    public GameObject[] mistakeIcons;

    // Start is called before the first frame update
    void Start()
    {
        mistakeIconIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMistake()
    {
        Animator XAnimator = mistakeIcons[mistakeIconIndex].GetComponent<Animator>();
        XAnimator.SetBool("isSolid", true);
        
        if (mistakeIconIndex < 2)
        {
            mistakeIconIndex++;
        }
    }
}
