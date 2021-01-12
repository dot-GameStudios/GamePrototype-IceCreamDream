using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static bool Paused = false;
    
    public int waveMaximum;

    public GameObject[] menus; //an array is needed because you can't use Find functions to grab inactive objects
    public GameObject rewardText;
    public GameObject waveCounterText;
    public void ChangeScene(string SceneName) //scene name that you want to change to
    { 
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Paused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
    }

    public void OpenMenu(string menuName) //menuName to find it in the Menu array
    {
        FindMenu(menuName).SetActive(true);
    }

    public void CloseMenu(string menuName) //menuName to find it in the Menu array
    {
        FindMenu(menuName).SetActive(false);
    }

    public GameObject FindMenu(string menuName) //menuName to find it in the Menu array
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(menus[i].name == menuName)
            {
                return menus[i];
            }
        }
        return null;
    }

    public void SetRewardText(int rewardValue) //rewardValue is for displaying points obtained in game
    {
        rewardText.GetComponent<TextMeshProUGUI>().text = rewardValue.ToString() + " Puppy Farts!";
    }

    public void SetWaveMaximum(int newValue) //newValue you want waveMaximum to be
    {
        waveMaximum = newValue;
    }

    public void SetWaveCounter(int newValue) //newValue you want the wave counter to say
    {
        waveCounterText.GetComponent<TextMeshProUGUI>().text = newValue.ToString() + "/" + waveMaximum.ToString();
    }
}
