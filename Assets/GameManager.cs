using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance{get{ return instance;}}

    //which index is using
    public int currentSkinIndex = 0;
    public int currency = 0;
    public int skinAvailability = 1;


	// Use this for initialization
	private void Awake()
    {
        instance = this;
        //gameObject Don't destroy what we are useing
        //point to Game Manager object 
        DontDestroyOnLoad(gameObject);
        //Play this before and save data? (boolean value)

        if (PlayerPrefs.HasKey ("CurrentSkin"))
        {
            //We had a previous sessions
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            currency = PlayerPrefs.GetInt("Currency");
            skinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        }
        else
        {
            //We load new game
            //PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
            //PlayerPrefs.SetInt("Currency", currency);
            //PlayerPrefs.SetInt("SkinAvailability", skinAvailability);
            Save();
        }
	}

    //make the skin save seen
    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("SkinAvailability", skinAvailability);
    }
}
