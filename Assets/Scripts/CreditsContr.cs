using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsContr : MonoBehaviour
{
	public Text txCredits;
	public Text txMain;
	
    // Start is called before the first frame update
    void Start()
    {
		
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txCredits.text = "Создатели";
			txMain.text = "Программист:\nМальцев Артём\n\nVK: vk.com/soonwknd\nDiscord: Playmick#5577\nE-mail: \nuralfansoft@gmail.com\n\n\nХудожник:\nНикита Максимов\n\nDiscord: Nik10Poky#6290\nE-mail:\nnik10poky@gmail.com";
		}
		else
		{
			txCredits.text = "Credits";
			txMain.text = "Programmer:\nMaltsev Artyom\n\nVK: vk.com/soonwknd\nDiscord: Playmick#5577\nE-mail: \nuralfansoft@gmail.com\n\n\nArtist:\nNikita Maksimov\n\nDiscord: Nik10Poky#6290\nE-mail:\nnik10poky@gmail.com";
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
