using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpContr : MonoBehaviour
{
	public Text txHelp;
	public Text txMainHelp;
	public Text txHealth;
	public Text txEnemy1;
	public Text txEnemy2;
	public Text txMoney;
	public Text txOverhead;
	public Text txPlayer;
	
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("ru") == 1)
		{
			txHelp.text = "Помощь";
			txMainHelp.text  = "Вы играете за парня, который провалился во времени. В руках у него оказалось энергетическое оружие, а вдалеке виднелись разъярённые рыцари. Основная цель игры - прожить как можно дольше. После покупки всех бонусов и поражения игра заканчивается, а ваши очки переносятся на доску лидеров, открыть которую можно из главного меню игры.";
			txHealth.text  = "Это ваши очки здоровья. Если они кончатся, то вы проиграете.";
			txEnemy1.text  = "В процессе игры вас будут атаковать враждебные рыцари.";
			txEnemy2.text  = "Если рыцарь ударит персонажа, то у вас уменьшится количество очков здоровья. Чтобы нанести им урон, нужно выстрелить в них из оружия. Побеждая рыцарей вы получаете очки, показывающие ваши достижения в игре. Со временем враги становятся сильнее и быстрее, так что маловероятно, что игра продлится долго.";
			txMoney.text  = "Иногда от поверженных врагов остаются монеты, которые герой может собирать если наступит на них. После проигрыша игрок может потратить накопленные монеты в магазине, получая за них бонусы, повышающие живучесть персонажа.";
			txOverhead.text  = "Во время стрельбы заполняется шкала перегрева в правой части экрана, если она заполнится, то ваше оружие некоторое время не сможет стрелять, так что стоит позаботиться о паузах в стрельбе.";
			txPlayer.text  = "Это ваш персонаж, для его передвижения передвигайте левый наэкранный стик, который появится во время игры. Для стрельбы соответственно используется правый стик.";
		}
		else
		{
			txHelp.text  = "Help";
			txMainHelp.text  = "You play as a guy who fell through in time. He had an energy weapon in his hands, and the furious knights could be seen in the distance. The main goal of the game is to live as long as possible. After buying all the bonuses and losing, the game ends, and your points are transferred to the leaderboard, which can be opened from the main menu of the game.";
			txHealth.text  = "These are your health points. If they run out, then you will lose.";
			txEnemy1.text  = "During the game, you will be attacked by hostile knights.";
			txEnemy2.text  = "If the knight hits the character, then your number of health points will decrease. To damage them, you need to shoot them with a weapon. By defeating the knights, you get points showing your achievements in the game. Over time, the enemies become stronger and faster, so it is unlikely that the game will last long.";
			txMoney.text  = "Sometimes there are coins left from defeated enemies, which the hero can collect if he steps on them. After losing, the player can spend the accumulated coins in the store, receiving bonuses for them that increase the survivability of the character.";
			txOverhead.text  = "During the shooting, the overheating scale on the right side of the screen is filled, if it is filled, then your weapon will not be able to shoot for a while, so you should take care of pauses in shooting.";
			txPlayer.text  = "This is your character, to move it, move the left on-screen stick that will appear during the game. For shooting, the right stick is used accordingly.";
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
