using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
	public Text txShop;
	
	public Text Coins;
	
	public Text txBonus;
	public Text txDesBonus;
	
	public Text txWarning;
	
	public GameObject WarnWind;//окно предупреждения
	public GameObject UIShop;//остальной UI магазина
	
	int cur_state = 1; // состояние 1 - магазин, 2 - хотите играть? 3 - хотите выйти? 4 хотите отменить улучшения?
	int next_state = 1; // состояние 1 - магазин, 2 - хотите играть? 3 - хотите выйти? 4 хотите отменить улучшения?
	
	int selBonus = 0; //выбранный бонус, 0 - нима, 1 сердце, 2 скорость, 3 перегрев, 4 сила атаки, 5 скорострельность, 6 щит
    int spent;//сколько мы потратили монет
	
	//тут лучше конечно создать массив, но мне лень
	//записываем КУДА были потрачены монеты
	int spentHeart;
	int spentSpeed;
	int spentOverheat;
	int spentAttack;
	int spentBlSp;
	int spentShield;
	
	public Text txHeart;
	public Text txPlSpeed;
	public Text txOverheat;
	public Text txAttack;
	public Text txBulletSpeed;
	public Text txShield;
	
	// Start is called before the first frame update
    void Start()
    {
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txShop.text = "Магазин";
			txBonus.text = "Здесь будет название бонуса.";
			txDesBonus.text = "Выберите один из бонусов, чтобы тут появилось его описание.";
			txWarning.text = "У вас остались не потраченные монеты, вы действительно хотите продолжить?";
		}
		else
		{
			txShop.text = "Shop";
			txBonus.text = "Here will be the name of the bonus.";
			txDesBonus.text = "Select one of the bonuses and its description will appear here.";
			txWarning.text = "Not all coins are spent, do you really want to continue?";
		}
		
		//записать рекорд
		RecordRank();
		
		//обнуляем потраченные на перки монеты
		spentHeart = 0;
		spentSpeed = 0;
		spentOverheat = 0;
		spentAttack = 0;
		spentBlSp = 0;
		spentShield = 0;
		spent = 0;
		
		txBonus = GameObject.Find("txName").GetComponent<Text>();
		txDesBonus = GameObject.Find("txDes").GetComponent<Text>();
		
		
        Coins = GameObject.Find("txCoins").GetComponent<Text>();
		if (PlayerPrefs.HasKey("coins"))
			Coins.text = PlayerPrefs.GetInt("coins").ToString();
		else
		{
			PlayerPrefs.SetInt("coins", 0);
			Coins.text = "0";
		}
		
		if (!PlayerPrefs.HasKey("Heart"))
			PlayerPrefs.SetInt("Heart", 0);
		if (!PlayerPrefs.HasKey("PlSpeed"))
			PlayerPrefs.SetInt("PlSpeed", 0);
		if (!PlayerPrefs.HasKey("Overheat"))
			PlayerPrefs.SetInt("Overheat", 0);
		if (!PlayerPrefs.HasKey("Attack"))
			PlayerPrefs.SetInt("Attack", 0);
		if (!PlayerPrefs.HasKey("BulletSpeed"))
			PlayerPrefs.SetInt("BulletSpeed", 0);
		if (!PlayerPrefs.HasKey("Shield"))
			PlayerPrefs.SetInt("Shield", 0);
		
		txHeart.text = PlayerPrefs.GetInt("Heart").ToString();
		txPlSpeed.text = PlayerPrefs.GetInt("PlSpeed").ToString();
		txOverheat.text = PlayerPrefs.GetInt("Overheat").ToString();
		txAttack.text = PlayerPrefs.GetInt("Attack").ToString();
		txBulletSpeed.text = PlayerPrefs.GetInt("BulletSpeed").ToString();
		txShield.text = PlayerPrefs.GetInt("Shield").ToString();
		
		if(PlayerPrefs.GetInt("Heart")==10 
			&& PlayerPrefs.GetInt("PlSpeed")==10 
			&& PlayerPrefs.GetInt("Overheat") ==10
			&& PlayerPrefs.GetInt("Attack") ==10
			&& PlayerPrefs.GetInt("BulletSpeed")==10
			&& PlayerPrefs.GetInt("Shield")==10)
			SceneManager.LoadScene("Leaderboard");
		
		//выключить окно подтверждения
		WarnWind.SetActive(false);
    }
	
	void Update()
	{
		//если следующее состояние не совпадает с текущим
		if(cur_state != next_state)
		{
			//если след состояние магазин
			if(next_state == 1)
			{
				//включить магаз
				UIShop.SetActive(true);
				
				//выключить окно подтверждения
				WarnWind.SetActive(false);
			}
			
			//если след состояние хотите играть?
			if(next_state == 2)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				WarnWind.SetActive(true);
				
				//пишем надпись "монеты не кончились"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "У вас остались не потраченные монеты, вы действительно хотите продолжить?";
				else
					txWarning.text = "Not all coins are spent, do you really want to continue?";
			}
			
			//если след состояние хотите выйти?
			if(next_state == 3)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				WarnWind.SetActive(true);
				
				//пишем надпись "Вы потеряете все достижения, ваш текущий счёт останется в меню score"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "Вы действительно хотите выйти? Вы потеряете все монеты, а ваш текущий счёт будет записан в рекордах.";
				else
					txWarning.text = "Do you really want to leave? You will lose all the coins, your current score will remain in the score menu.";
			}
			
			//если след состояние отменить улучшения?
			if(next_state == 4)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				WarnWind.SetActive(true);
				
				//пишем надпись "Все выбранные улучшения обнулятся, а потраченные монеты вернуться к вам на счёт, вы уверены что хотите этого?"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "Все выбранные улучшения будут отменены, а потраченные монеты вернуться к вам на счёт, вы уверены что хотите этого?";
				else
					txWarning.text = "All current changes will be reset to zero, and the spent coins will be returned to your account, are you sure you want to do this?";
			}
			
			//приравниваем состояния
			cur_state = next_state;
		}
	}
	
	//кнопка в меню
	public void OnClickMainMenu()
	{
		next_state = 3;
	}
	
	//сбросить бонусы
	public void OnClickCancelBonuses()
	{
		next_state = 4;
	}
	
	//отмена предупреждающего окна
	public void OnClickCancel()
	{
		next_state = 1;
	}
	
	//принять предупреждающее окно
	public void OnClickConfirm()
	{
		//если тек состояние хотите играть?
		if(cur_state == 2)
		{
			//запускаем игру
			SceneManager.LoadScene("Survival");
		}
		
		//если тек состояние хотите выйти?
		if(cur_state == 3)
		{
			SceneManager.LoadScene("MainMenu");
		}
		
		//если тек состояние отменить улучшения?
		if(cur_state == 4)
		{
			//добавляем потраченные монеты к текущим
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + spent);
			
			Coins.text = PlayerPrefs.GetInt("coins").ToString();
			
			//обнуляем потраченные монеты
			spent = 0;
			
			//снимаем перки
			PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") - spentHeart);
			PlayerPrefs.SetInt("PlSpeed", PlayerPrefs.GetInt("PlSpeed") - spentSpeed);
			PlayerPrefs.SetInt("Overheat", PlayerPrefs.GetInt("Overheat") - spentOverheat);
			PlayerPrefs.SetInt("Attack", PlayerPrefs.GetInt("Attack") - spentAttack);
			PlayerPrefs.SetInt("BulletSpeed", PlayerPrefs.GetInt("BulletSpeed") - spentBlSp);
			PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") - spentShield);
			
			txHeart.text = PlayerPrefs.GetInt("Heart").ToString();
			txPlSpeed.text = PlayerPrefs.GetInt("PlSpeed").ToString();
			txOverheat.text = PlayerPrefs.GetInt("Overheat").ToString();
			txAttack.text = PlayerPrefs.GetInt("Attack").ToString();
			txBulletSpeed.text = PlayerPrefs.GetInt("BulletSpeed").ToString();
			txShield.text = PlayerPrefs.GetInt("Shield").ToString();
			
			spentHeart = 0;
			spentSpeed = 0;
			spentOverheat = 0;
			spentAttack = 0;
			spentBlSp = 0;
			spentShield = 0;
			
			//включить магаз
			UIShop.SetActive(true);
				
			//выключить окно подтверждения
			WarnWind.SetActive(false);
			
			cur_state = 1;
			next_state = 1;
		}
	}
	
	//кнопка в меню
	public void OnClickPlay()
	{
		
		
		//если остались монетки
		if(PlayerPrefs.GetInt("coins")>0)
		{
			//выводим предупреждение
			next_state = 2;
			
		}
		
		//если монеток ноль запускаем игру
		if(PlayerPrefs.GetInt("coins")==0)
			SceneManager.LoadScene("Survival");
	}
	
    public void OnClickAD()
	{
		
	}
	
	public void OnClickUpHeart()
	{
		selBonus = 1;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Очки здоровья";
			txDesBonus.text = "Этот бонус даёт дополнительные очки здоровья. Повысь свою живучесть и сможешь выдерживать больше ударов судьбы, ну... или ножа.";
		}
		else
		{
			txBonus.text = "Health points";
			txDesBonus.text = "This bonus gives you an extra health point.";
		}
	}
	
	public void OnClickUpPlSpeed()
	{
		selBonus = 2;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Скорость движения";
			txDesBonus.text = "Вы будете бегать шустрее.";
		}
		else
		{
			txBonus.text = "Player speed";
			txDesBonus.text = "This bonus makes you faster.";
		}
	}
	
	public void OnClickUpOverheat()
	{
		selBonus = 3;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Снижение перегрева";
			txDesBonus.text = "Оружие будет охлаждаться быстрее.";
		}
		else
		{
			txBonus.text = "Reduction of overheating";
			txDesBonus.text = "Your weapon will cool faster.";
		}
	}
	
	public void OnClickUpAttack()
	{
		selBonus = 4;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Сила атаки";
			txDesBonus.text = "Выстрелы будут наносить чуточку больше урона.";
		}
		else
		{
			txBonus.text = "Attack power";
			txDesBonus.text = "Your bullets will do a little more damage.";
		}
	}
	
	public void OnClickUpBulletSpeed()
	{
		selBonus = 5;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Скорость снарядов";
			txDesBonus.text = "Снаряды будут летать быстрее! Словно у них появился джетпак...";
		}
		else
		{
			txBonus.text = "Bullet speed";
			txDesBonus.text = "Your bullets will fly faster! As if they have a jetpack...";
		}
	}
	
	public void OnClickUpShield()
	{
		selBonus = 6;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txBonus.text = "Удача!";
			txDesBonus.text = "Монеты будут попадаться чаще! Кто не хочет быть богатым?!";
		}
		else
		{
			txBonus.text = "Luck!";
			txDesBonus.text = "Coins will drop out more often!";
		}
	}
	
	public void OnClickUpgrade()
	{
		if(PlayerPrefs.GetInt("coins") > 0)
		{
			//у нас достаточно монет
			
			//проверяем какой перк выбран
			//добавляем перк
			switch(selBonus)
			{
				
				case 1:
					if(PlayerPrefs.GetInt("Heart")<10)
					{
						PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart")+1);
						txHeart.text = PlayerPrefs.GetInt("Heart").ToString();
						spentHeart++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				case 2:
					if(PlayerPrefs.GetInt("PlSpeed")<10)
					{
						PlayerPrefs.SetInt("PlSpeed", PlayerPrefs.GetInt("PlSpeed")+1);
						txPlSpeed.text = PlayerPrefs.GetInt("PlSpeed").ToString();
						spentSpeed++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				case 3:
					if(PlayerPrefs.GetInt("Overheat")<10)
					{
						PlayerPrefs.SetInt("Overheat", PlayerPrefs.GetInt("Overheat")+1);
						txOverheat.text = PlayerPrefs.GetInt("Overheat").ToString();
						spentOverheat++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				case 4:
					if(PlayerPrefs.GetInt("Attack")<10)
					{
						PlayerPrefs.SetInt("Attack", PlayerPrefs.GetInt("Attack")+1);
						txAttack.text = PlayerPrefs.GetInt("Attack").ToString();
						spentAttack++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				case 5:
					if(PlayerPrefs.GetInt("BulletSpeed")<10)
					{
						PlayerPrefs.SetInt("BulletSpeed", PlayerPrefs.GetInt("BulletSpeed")+1);
						txBulletSpeed.text = PlayerPrefs.GetInt("BulletSpeed").ToString();
						spentBlSp++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				case 6:
					if(PlayerPrefs.GetInt("Shield")<10)
					{
						PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield")+1);
						txShield.text = PlayerPrefs.GetInt("Shield").ToString();
						spentShield++;
						//вычитаем одну монетку
						PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-1);
						//добавляем одну потраченную монетку
							spent++;
					}
					else
					{
						//Этот параметр достиг максимума, его больше нельзя улучшать.
						if(PlayerPrefs.GetInt("ru") == 1)
						{
							txBonus.text = "Максимальное значение";
							txDesBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
						}
						else
						{
							txBonus.text = "Maximum value";
							txDesBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
						}
					}
				break;
				default:
					
					//написать что  вы не выбрали бонус
					if(PlayerPrefs.GetInt("ru") == 1)
					{
						txBonus.text = "Упс..!";
						txDesBonus.text = "Похоже, вы не выбрали бонус, сначала нужно кликнуть на один из бонусов выше.";
					}
					else
					{
						txBonus.text = "Oops..!";
						txDesBonus.text = "Looks like you didn't choose a bonus, you should first click on one of the buttons above.";
					}
					
				break;
				
			}
			
			//убавить одну монетку на UI
			Coins.text = PlayerPrefs.GetInt("coins").ToString();
			
			
		}
		else
		{
			//если бонус выбран
			if(selBonus>0)
			{
				//написать что не хватает монет
					if(PlayerPrefs.GetInt("ru") == 1)
					{
						txBonus.text = "Не хваает монет.";
						txDesBonus.text = "Похоже, у тебя нет монет... Видимо, пора вернуться в бой! Или посмотреть рекламу...";
					}
					else
					{
						txBonus.text = "Not enough coins.";
						txDesBonus.text = "Looks like you're out of coins... It's time to get back into the fight! Or watch an ad...";
					}
				
			}
			else
			{
				//написать что  вы не выбрали бонус
				if(PlayerPrefs.GetInt("ru") == 1)
				{
					txBonus.text = "Упс..!";
					txDesBonus.text = "Похоже вы не выбрали бонус, сначала нужно кликнуть на один из бонусов выше.";
				}
				else
				{
					txBonus.text = "Oops..!";
					txDesBonus.text = "Looks like you didn't choose a bonus, you should first click on one of the buttons above.";
				}
			}
			
		}
	}
	
	public void RecordRank()
	{
		//если первый ранг меньше текущего то 10 = 9, 9 = 8 и так до первого, а первому присвоить текущий счёт
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank10"))
		{
			PlayerPrefs.SetInt("Rank10", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank9"))
		{
			PlayerPrefs.SetInt("Rank10", PlayerPrefs.GetInt("Rank9"));
			PlayerPrefs.SetInt("Rank9", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank8"))
		{
			PlayerPrefs.SetInt("Rank9", PlayerPrefs.GetInt("Rank8"));
			PlayerPrefs.SetInt("Rank8", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank7"))
		{
			PlayerPrefs.SetInt("Rank8", PlayerPrefs.GetInt("Rank7"));
			PlayerPrefs.SetInt("Rank7", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank6"))
		{
			PlayerPrefs.SetInt("Rank7", PlayerPrefs.GetInt("Rank6"));
			PlayerPrefs.SetInt("Rank6", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank5"))
		{
			PlayerPrefs.SetInt("Rank6", PlayerPrefs.GetInt("Rank5"));
			PlayerPrefs.SetInt("Rank5", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank4"))
		{
			PlayerPrefs.SetInt("Rank5", PlayerPrefs.GetInt("Rank4"));
			PlayerPrefs.SetInt("Rank4", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank3"))
		{
			PlayerPrefs.SetInt("Rank4", PlayerPrefs.GetInt("Rank3"));
			PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank2"))
		{
			PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));
			PlayerPrefs.SetInt("Rank2", PlayerPrefs.GetInt("CurScore"));
		}
		if(PlayerPrefs.GetInt("CurScore")>PlayerPrefs.GetInt("Rank1"))
		{
			PlayerPrefs.SetInt("Rank2", PlayerPrefs.GetInt("Rank1"));
			PlayerPrefs.SetInt("Rank1", PlayerPrefs.GetInt("CurScore"));
		}
	}
	
}
