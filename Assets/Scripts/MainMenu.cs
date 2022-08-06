using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public AudioSource audio;
	
	public Text txPlay;
	public Text txScore;
	public Text txHelp;
	public Text txCredits;
	public Text txExit;
	public GameObject txGT;
	
	public Sprite btRus;
	public Sprite btEng;
	public Image btLang;
	
	void Start()
	{
		audio = GetComponent<AudioSource>();
		
		PlayerPrefs.SetInt("coins", 0);
		PlayerPrefs.SetInt("Heart", 0);
		PlayerPrefs.SetInt("PlSpeed", 0);
		PlayerPrefs.SetInt("Overheat", 0);
		PlayerPrefs.SetInt("Attack", 0);
		PlayerPrefs.SetInt("BulletSpeed", 0);
		PlayerPrefs.SetInt("Shield", 0);
		
		if (!PlayerPrefs.HasKey("Rank1"))
			PlayerPrefs.SetInt("Rank1", 0);
		
		if (!PlayerPrefs.HasKey("Rank2"))
			PlayerPrefs.SetInt("Rank2", 0);
		
		if (!PlayerPrefs.HasKey("Rank3"))
			PlayerPrefs.SetInt("Rank3", 0);
		
		if (!PlayerPrefs.HasKey("Rank4"))
			PlayerPrefs.SetInt("Rank4", 0);
		
		if (!PlayerPrefs.HasKey("Rank5"))
			PlayerPrefs.SetInt("Rank5", 0);
		
		if (!PlayerPrefs.HasKey("Rank6"))
			PlayerPrefs.SetInt("Rank6", 0);
		
		if (!PlayerPrefs.HasKey("Rank7"))
			PlayerPrefs.SetInt("Rank7", 0);
		
		if (!PlayerPrefs.HasKey("Rank8"))
			PlayerPrefs.SetInt("Rank8", 0);
		
		if (!PlayerPrefs.HasKey("Rank9"))
			PlayerPrefs.SetInt("Rank9", 0);
		
		if (!PlayerPrefs.HasKey("Rank10"))
			PlayerPrefs.SetInt("Rank10", 0);
		
		LangChange();
		
	}
    public void OnClickPlay()
	{
		
		SceneManager.LoadScene("Survival");
	}
	
    public void OnClickLeaderboard()
	{
		
		SceneManager.LoadScene("Leaderboard");
	}
	
	public void OnClickHelp()
	{
		
		SceneManager.LoadScene("Help");
	}
	
	public void OnClickCredits()
	{
		
		SceneManager.LoadScene("Credits");
	}
	
	public void OnClickOptions()
	{
		
		SceneManager.LoadScene("Options");
	}
	
	public void OnClickExit()
	{
		
		Application.Quit();
	}
	
	public void OnClickDeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}
	
	public void OnClickBack()
	{
		
		SceneManager.LoadScene("MainMenu");
	}
	
	public void OnClickLanguage()
	{
		audio.Play();
		if(PlayerPrefs.GetInt("ru") == 0)
			PlayerPrefs.SetInt("ru", 1);
		else
			PlayerPrefs.SetInt("ru", 0);
			
		LangChange();
	}
	
	void LangChange()
	{
		if(!PlayerPrefs.HasKey("ru"))
		{
			PlayerPrefs.SetInt("ru", 0);//0 инглиш, 1 руссиш
		}
		
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txPlay.text = "Играть";
			txScore.text = "Рекорд";
			txHelp.text = "Помощь";
			txCredits.text = "Создатели";
			txExit.text = "Выход";
			txGT.SetActive(false);
			btLang.sprite = btEng;
		}
		else
		{
			txPlay.text = "Play";
			txScore.text = "Score";
			txHelp.text = "Help";
			txCredits.text = "Credits";
			txExit.text = "Exit";
			txGT.SetActive(true);
			btLang.sprite = btRus;
		}
	}
	
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Home) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Menu))
		{
			switch (SceneManager.GetActiveScene().buildIndex)
			{
			  case 0:
				  Application.Quit();
				  break;
			  default:
				  SceneManager.LoadScene("MainMenu");
				  break;
			}
		}
		
		
	}
	
	
}
