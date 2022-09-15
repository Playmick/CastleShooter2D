using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Gameplay.SkillsInteractor;
using Gameplay;
using Architecture;

public class Shop : MonoBehaviour
{
	public Text txShop;
	
	public Text txCoins;
	
	public Text txNameBonus;
	public Text txDescriptionBonus;
	
	public Text txWarning;
	
	public GameObject warningWindow;//окно предупреждения
	public GameObject UIShop;//остальной UI магазина

    public Text txHeart;
    public Text txPlSpeed;
    public Text txOverheat;
    public Text txAttack;
    public Text txBulletSpeed;
    public Text txShield;

    private SkillsInteractor _skill;
    
    ShopState currentState = ShopState.shop; 
    ShopState nextState = ShopState.shop; 
	
	Skill selectedSkill = Skill.none; 
	
	
	// Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ru") == 1)
        {
            txShop.text = "Магазин";
            txNameBonus.text = "Здесь будет название бонуса.";
            txDescriptionBonus.text = "Выберите один из бонусов, чтобы тут появилось его описание.";
            txWarning.text = "У вас остались не потраченные монеты, вы действительно хотите продолжить?";
        }
        else
        {
            txShop.text = "Shop";
            txNameBonus.text = "Here will be the name of the bonus.";
            txDescriptionBonus.text = "Select one of the bonuses and its description will appear here.";
            txWarning.text = "Not all coins are spent, do you really want to continue?";
        }

        //записать рекорд
        RecordRank();

        txCoins.text = Bank.coins.ToString();
        if (!Game.isRun)
            Game.OnGameInitializedEvent += OnSkillInitialize;
        else
            OnSkillInitialize();

        //выключить окно подтверждения
        warningWindow.SetActive(false);
    }

    private void OnSkillInitialize()
    {
        _skill = Game.GetInteractor<SkillsInteractor>();

        txHeart.text = _skill.heart.ToString();
        txPlSpeed.text = _skill.playerSpeed.ToString();
        txOverheat.text = _skill.overheat.ToString();
        txAttack.text = _skill.attack.ToString();
        txBulletSpeed.text = _skill.bulletSpeed.ToString();
        txShield.text = _skill.luck.ToString();

        if (_skill.IsMax())
            SceneManager.LoadScene("Leaderboard");
    }

    void Update()
	{
		//если следующее состояние не совпадает с текущим
		if(currentState != nextState)
		{
			//если след состояние магазин
			if(nextState == ShopState.shop)
			{
				//включить магаз
				UIShop.SetActive(true);
				
				//выключить окно подтверждения
				warningWindow.SetActive(false);
			}
			
			//если след состояние хотите играть?
			if(nextState == ShopState.doYouWantPlay)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				warningWindow.SetActive(true);
				
				//пишем надпись "монеты не кончились"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "У вас остались не потраченные монеты, вы действительно хотите продолжить?";
				else
					txWarning.text = "Not all coins are spent, do you really want to continue?";
			}
			
			//если след состояние хотите выйти?
			if(nextState == ShopState.doYouWantExit)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				warningWindow.SetActive(true);
				
				//пишем надпись "Вы потеряете все достижения, ваш текущий счёт останется в меню score"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "Вы действительно хотите выйти? Вы потеряете все монеты, а ваш текущий счёт будет записан в рекордах.";
				else
					txWarning.text = "Do you really want to leave? You will lose all the coins, your current score will remain in the score menu.";
			}
			
			//если след состояние отменить улучшения?
			if(nextState == ShopState.doYouWantResetChanges)
			{
				//выключаем магазин
				UIShop.SetActive(false);
				//включаем окно предупреждения
				warningWindow.SetActive(true);
				
				//пишем надпись "Все выбранные улучшения обнулятся, а потраченные монеты вернуться к вам на счёт, вы уверены что хотите этого?"
				if(PlayerPrefs.GetInt("ru") == 1)
					txWarning.text = "Все выбранные улучшения будут отменены, а потраченные монеты вернуться к вам на счёт, вы уверены что хотите этого?";
				else
					txWarning.text = "All current changes will be reset to zero, and the spent coins will be returned to your account, are you sure you want to do this?";
			}
			
			//приравниваем состояния
			currentState = nextState;
		}
	}
	
	//кнопка в меню
	public void OnClickMainMenu()
	{
		nextState = ShopState.doYouWantExit;
	}
	
	//сбросить бонусы
	public void OnClickCancelBonuses()
	{
		nextState = ShopState.doYouWantResetChanges;
	}
	
	//отмена предупреждающего окна
	public void OnClickCancel()
	{
		nextState = ShopState.shop;
	}
	
	//принять предупреждающее окно
	public void OnClickConfirm()
	{
		//если тек состояние хотите играть?
		if(currentState == ShopState.doYouWantPlay)
		{
			//запускаем игру
			SceneManager.LoadScene("Survival");
		}
		
		//если тек состояние хотите выйти?
		if(currentState == ShopState.doYouWantExit)
		{
			SceneManager.LoadScene("MainMenu");
		}
		
		//если тек состояние отменить улучшения?
		if(currentState == ShopState.doYouWantResetChanges)
		{
			txCoins.text = _skill.ResetChanges().ToString();
			
			//включить магаз
			UIShop.SetActive(true);
				
			//выключить окно подтверждения
			warningWindow.SetActive(false);
			
			currentState = ShopState.shop;
			nextState = ShopState.shop;
		}
	}
	
	//кнопка в меню
	public void OnClickPlay()
	{
		
		
		//если остались монетки
		if(Bank.coins>0)
		    nextState = ShopState.doYouWantPlay;
		
		//если монеток ноль запускаем игру
		if(Bank.coins == 0)
			SceneManager.LoadScene("Survival");
	}
	
    public void OnClickAD()
	{
		
	}
	
	public void OnClickUpHeart()
	{
		selectedSkill = Skill.heart;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Очки здоровья";
			txDescriptionBonus.text = "Этот бонус даёт дополнительные очки здоровья. Повысь свою живучесть и сможешь выдерживать больше ударов судьбы, ну... или ножа.";
		}
		else
		{
			txNameBonus.text = "Health points";
			txDescriptionBonus.text = "This bonus gives you an extra health point.";
		}
	}
	
	public void OnClickUpPlSpeed()
	{
		selectedSkill = Skill.playerSpeed;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Скорость движения";
			txDescriptionBonus.text = "Вы будете бегать шустрее.";
		}
		else
		{
			txNameBonus.text = "Player speed";
			txDescriptionBonus.text = "This bonus makes you faster.";
		}
	}
	
	public void OnClickUpOverheat()
	{
		selectedSkill = Skill.overheat;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Снижение перегрева";
			txDescriptionBonus.text = "Оружие будет охлаждаться быстрее.";
		}
		else
		{
			txNameBonus.text = "Reduction of overheating";
			txDescriptionBonus.text = "Your weapon will cool faster.";
		}
	}
	
	public void OnClickUpAttack()
	{
		selectedSkill = Skill.attack;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Сила атаки";
			txDescriptionBonus.text = "Выстрелы будут наносить чуточку больше урона.";
		}
		else
		{
			txNameBonus.text = "Attack power";
			txDescriptionBonus.text = "Your bullets will do a little more damage.";
		}
	}
	
	public void OnClickUpBulletSpeed()
	{
		selectedSkill = Skill.bulletSpeed;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Скорость снарядов";
			txDescriptionBonus.text = "Снаряды будут летать быстрее! Словно у них появился джетпак...";
		}
		else
		{
			txNameBonus.text = "Bullet speed";
			txDescriptionBonus.text = "Your bullets will fly faster! As if they have a jetpack...";
		}
	}
	
	public void OnClickUpShield()
	{
		selectedSkill = Skill.luck;
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txNameBonus.text = "Удача!";
			txDescriptionBonus.text = "Монеты будут попадаться чаще! Кто не хочет быть богатым?!";
		}
		else
		{
			txNameBonus.text = "Luck!";
			txDescriptionBonus.text = "txCoins will drop out more often!";
		}
	}
	
	public void OnClickUpgrade()
	{
		if(Bank.IsEnoughtCoins(1))
		{
            if(selectedSkill == Skill.none)
            {
                //написать что  вы не выбрали бонус
                if (PlayerPrefs.GetInt("ru") == 1)
                {
                    txNameBonus.text = "Упс..!";
                    txDescriptionBonus.text = "Похоже, вы не выбрали бонус, сначала нужно кликнуть на один из бонусов выше.";
                }
                else
                {
                    txNameBonus.text = "Oops..!";
                    txDescriptionBonus.text = "Looks like you didn't choose a bonus, you should first click on one of the buttons above.";
                }
            }
            else if (!_skill.AddSkill(selectedSkill))
            {
                //Этот параметр достиг максимума, его больше нельзя улучшать.
                if (PlayerPrefs.GetInt("ru") == 1)
                {
                    txNameBonus.text = "Максимальное значение";
                    txDescriptionBonus.text = "Этот параметр достиг максимума. К сожалению, его больше нельзя улучшать.";
                }
                else
                {
                    txNameBonus.text = "Maximum value";
                    txDescriptionBonus.text = "This parameter has reached the maximum, it can no longer be improved.";
                }
                Bank.Spend(this, 1);

                //убавить одну монетку на UI
                txCoins.text = Bank.coins.ToString();
            }
            
		}
		else
		{
			if(PlayerPrefs.GetInt("ru") == 1)
			{
				txNameBonus.text = "Не хватает монет.";
				txDescriptionBonus.text = "Похоже, у тебя нет монет... Видимо, пора вернуться в бой! Или посмотреть рекламу...";
			}
			else
			{
				txNameBonus.text = "Not enough coins.";
				txDescriptionBonus.text = "Looks like you're out of coins... It's time to get back into the fight! Or watch an ad...";
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
