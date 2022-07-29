using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresContr : MonoBehaviour
{
	
	
	public Text txScores;
	public Text txPos;
	public Text txScore;
	
	public Text Sc1;
	public Text Sc2;
	public Text Sc3;
	public Text Sc4;
	public Text Sc5;
	public Text Sc6;
	public Text Sc7;
	public Text Sc8;
	public Text Sc9;
	public Text Sc10;
	
    // Start is called before the first frame update
    void Start()
    {
		
        Sc1.text = PlayerPrefs.GetInt("Rank1").ToString();
		Sc2.text = PlayerPrefs.GetInt("Rank2").ToString();
		Sc3.text = PlayerPrefs.GetInt("Rank3").ToString();
		Sc4.text = PlayerPrefs.GetInt("Rank4").ToString();
		Sc5.text = PlayerPrefs.GetInt("Rank5").ToString();
		Sc6.text = PlayerPrefs.GetInt("Rank6").ToString();
		Sc7.text = PlayerPrefs.GetInt("Rank7").ToString();
		Sc8.text = PlayerPrefs.GetInt("Rank8").ToString();
		Sc9.text = PlayerPrefs.GetInt("Rank9").ToString();
		Sc10.text = PlayerPrefs.GetInt("Rank10").ToString();
		
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txScores.text = "Рекорд";
			txPos.text = "Поз";
			txScore.text = "Счёт";
		}
		else
		{
			txScores.text = "Scores";
			txPos.text = "Pos";
			txScore.text = "Score";
		}
    }
	
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Home) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Menu))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
	
	public void OnClickBack()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
}
