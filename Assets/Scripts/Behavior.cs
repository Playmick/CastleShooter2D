using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Behavior : MonoBehaviour
{
    public Text txPause;
    public Text txResume;
    public Text txExit;
    public Text txConfirm;

    public Sprite ruOverheat;
    public Sprite enOverheat;

    public GameObject JST1;//движение
    public GameObject JST2;//стрельба
    public bool Mobile = false;
    public SimpleTouchController JST1Scr;
    public SimpleTouchController JST2Scr;

    public GameObject uiPause;
    public GameObject uiButtons;
    public GameObject uiConfirm;

    public GameObject player;
    public GameObject bullet;

    float TimeToSpawn;
    public Transform[] SpawnPnts;
    public int CurrSpPnt;
    public GameObject Enemy;

    public GameObject Hearth0;
    public GameObject Hearth1;
    public GameObject Hearth2;
    public GameObject Hearth3;
    public GameObject Hearth4;
    public GameObject Hearth5;
    public GameObject Hearth6;
    public GameObject Hearth7;
    public GameObject Hearth8;
    public GameObject Hearth9;
    public GameObject Hearth10;
    public GameObject Hearth11;
    public GameObject Hearth12;

    public Sprite spFullHearth;
    public Sprite spEmptyHearth;
    private Image imgHearth;
    public Image imgFillOverheat;
    public Image txtOverheat;


    Camera cam;
    public Vector2 target;
    Vector2 PlPos;//позиция игрока
    Vector2 MovePlayer;
    float ang;
    bool life;
    public int HP = 3;

    public GameObject MovePnt;
    public GameObject TrackPnt;

    public float maxSpeed;
    public float GamepadX = 0;
    public float GamepadY = 0;
    public int Score = 0;


    public Animator anim;

    private Rigidbody2D rb;

    //стрельба
    bool ClickShoot = false;
    float Overheat = 0;
    bool isOverheat = false;
    public Text txtScore;
    public Text txtCoins;

    bool posblSht = true; //возможность стрельбы
    int tmNotPsblSht = 0;

    public AudioSource ShotAudioSource;
    
    void Start()
    {
        //подключаем звук, сделано чтобы мышкой не та
        ShotAudioSource = GetComponent<AudioSource>();


        TimeToSpawn = 3f;
        HP = 3 + PlayerPrefs.GetInt("Heart");

        hpGen();

        maxSpeed = maxSpeed * (100 + (PlayerPrefs.GetInt("PlSpeed") * 5));
        cam = Camera.main;
        rb = player.GetComponent<Rigidbody2D>();
        life = true;

        StartCoroutine(TrackCorut());

        SpawnPnts = GetComponentsInChildren<Transform>();

        StartCoroutine(SpawnEnemys());

        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
        }

        PlayerPrefs.SetInt("CurScore", 0);

        //Закомментил потому что проверка не работает
        Debug.Log(Application.platform);
        //мы играем на компе?
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            Mobile = false;
		else
			Mobile = true;
        
        //Mobile = true;
        if (Mobile)
        {
            JST1Scr = JST1.GetComponent<SimpleTouchController>();
            JST2Scr = JST2.GetComponent<SimpleTouchController>();
        }
        else
        {
            JST1.SetActive(false);//движение
            JST2.SetActive(false);//стрельба
        }
		
		if(PlayerPrefs.GetInt("ru") == 1)
		{
			txtOverheat.sprite = ruOverheat;
			txtScore.text = "Счёт:";
			txPause.text = "Пауза";
			txResume.text = "Вернуться";
			txExit.text = "Выход";
			txConfirm.text = "Вы действительно хотите выйти? Вы потеряете все монеты, а ваш текущий счёт будет записан в рекордах.";
		}
		else
		{
			txtOverheat.sprite = enOverheat;
			txtScore.text = "Score:";
			txPause.text = "Pause";
			txResume.text = "Resume";
			txExit.text = "Exit";
			txConfirm.text = "Do you really want to leave? You will lose all the coins, your current score will remain in the score menu.";
		}
		
		uiConfirm.SetActive(false);
		uiPause.SetActive(false);
		uiButtons.SetActive(false);
    }

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Home) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Menu))
		{
			uiButtons.SetActive(true);
			uiConfirm.SetActive(false);
			uiPause.SetActive(!uiPause.activeSelf);
		}
		txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
		GamepadX = 0;
		GamepadY = 0;
		
		ClickShoot=false;
		
		if(life && !uiPause.activeSelf)
		{
			if(Mobile)
			{
				if(JST1Scr.touchPresent)//если держим стик
				{
					GamepadX = JST1Scr.movementVector.x;
					GamepadY = JST1Scr.movementVector.y;
				}
				if(JST2Scr.touchPresent && (JST2Scr.movementVector.x!=0 || JST2Scr.movementVector.y!=0))
				{
					ClickShoot=true;
					//target2 = target;
					target = new Vector2 (JST2Scr.movementVector.x, JST2Scr.movementVector.y);
					
				}
			}
			else
			{
				if (Input.GetMouseButton(0))
				{
					ClickShoot=true;
				}
				
				if(Input.GetKey(KeyCode.A))
					GamepadX -= 1;
				
				if(Input.GetKey(KeyCode.D))
					GamepadX += 1;
				
				if(Input.GetKey(KeyCode.W))
					GamepadY += 1;
				
				if(Input.GetKey(KeyCode.S))
					GamepadY -= 1;
				
				target = new Vector2 (Input.mousePosition.x - (Screen.width/2), Input.mousePosition.y - (Screen.height/2));
			
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(HP<13 && ((3 + PlayerPrefs.GetInt("Heart"))>=13) )
		{
			imgHearth = Hearth12.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<12 && ((3 + PlayerPrefs.GetInt("Heart"))>=12) )
		{
			imgHearth = Hearth11.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<11 && ((3 + PlayerPrefs.GetInt("Heart"))>=11) )
		{
			imgHearth = Hearth10.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<10 && ((3 + PlayerPrefs.GetInt("Heart"))>=10) )
		{
			imgHearth = Hearth9.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<9 && ((3 + PlayerPrefs.GetInt("Heart"))>=9) )
		{
			imgHearth = Hearth8.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<8 && ((3 + PlayerPrefs.GetInt("Heart"))>=8) )
		{
			imgHearth = Hearth7.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<7 && ((3 + PlayerPrefs.GetInt("Heart"))>=7) )
		{
			imgHearth = Hearth6.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<6 && ((3 + PlayerPrefs.GetInt("Heart"))>=6) )
		{
			imgHearth = Hearth5.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<5 && ((3 + PlayerPrefs.GetInt("Heart"))>=5) )
		{
			imgHearth = Hearth4.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<4 && ((3 + PlayerPrefs.GetInt("Heart"))>=4) )
		{
			imgHearth = Hearth3.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<3)
		{
			imgHearth = Hearth2.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<2)
		{
			imgHearth = Hearth1.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		if(HP<1)
		{
			imgHearth = Hearth0.GetComponent<Image>();
			imgHearth.sprite = spEmptyHearth;
		}
		
        //txtHP.text = HP.ToString();
		//txtOverheat.text = Mathf.RoundToInt(Overheat).ToString();
		if(PlayerPrefs.GetInt("ru") == 1)
			txtScore.text = "Счёт: " + Score.ToString();
		else
			txtScore.text = "Score: " + Score.ToString();
		
		if(!uiPause.activeSelf)
			ShootMet();
		
		MovePlayer = new Vector2(GamepadX, GamepadY);
		
		if(Mobile)
		{
			rb.velocity = MovePlayer * maxSpeed * Time.fixedDeltaTime;
		}
		else
		{
			rb.velocity = MovePlayer.normalized * maxSpeed * Time.fixedDeltaTime;
		}
		
		if (GamepadX!=0 && GamepadY!=0)
		{
			MovePnt.transform.position = new Vector2( player.transform.position.x +(GamepadX*100f), player.transform.position.y + (GamepadY*100f));
		}
		
		MoveCameraToPlayer();
		
		AnimPlayer();
		
		if(Score>100)
		{
			//начинаем издевательство над игроком)
			if(TimeToSpawn>0 && Score<1000)
			{
				TimeToSpawn = 2f - ((Score-100f)/450f);
			}
			else
				TimeToSpawn=0f;
		}
    }
	
	void MoveCameraToPlayer()
	{
		if ((player.transform.position.x - cam.transform.position.x)>10f)
		{
			
			cam.transform.Translate(Vector2.right  * (player.transform.position.x - cam.transform.position.x)*0.01f);
			
		}
				
		if ((player.transform.position.y - cam.transform.position.y)>10f)
		{
			
			cam.transform.Translate(Vector2.up * (player.transform.position.y - cam.transform.position.y)*0.01f);
			
		}
				
		if ((player.transform.position.x - cam.transform.position.x)<-10f)
		{
			
			cam.transform.Translate(Vector2.right * (player.transform.position.x - cam.transform.position.x)*0.01f);
			
		}
		
		if ((player.transform.position.y - cam.transform.position.y)<-10f)
		{
			
			cam.transform.Translate(Vector2.up * (player.transform.position.y - cam.transform.position.y)*0.01f);
			
		}
	}
	
	void AnimPlayer()
	{
		
		PlPos = new Vector2(player.transform.position.x, player.transform.position.y);
		
		Vector2 targetDir = target - /*PlPos*/ Vector2.zero;
		
		ang = Vector2.SignedAngle(targetDir, Vector2.up);
		//Debug.Log(vision.transform.position);
		
		
		if (ang<0)
			ang = 360f+ang;
		
		//Debug.Log(ang);
		
		if(life)//если персонаж жив
		{
			if (ang>315 || ang<45)//если мышка сверху
				if(GamepadX==0 && GamepadY==0)//если персонаж не двигается
					anim.SetInteger("PLanim", 10);//смотрим ввверх
												  //в противном случае
				else anim.SetInteger("PLanim", 20);//идём вверх
			
			if (ang>45f && ang<135)//если мышка справа
				if(GamepadX==0 && GamepadY==0)//если персонаж не двигается
					anim.SetInteger("PLanim", 13);//смотрим вправо
												  //в противном случае
				else anim.SetInteger("PLanim", 23);//движение вправо
			
			if (ang>135 && ang<225)//если мышка снизу
				if(GamepadX==0 && GamepadY==0)//если персонаж не двигается
					anim.SetInteger("PLanim", 16);//смотрим вниз
												  //в противном случае
				else anim.SetInteger("PLanim", 26);//идём вниз
			
			if (ang>225 && ang<315)//если мышка слева
				if(GamepadX==0 && GamepadY==0)//если персонаж не двигается
					anim.SetInteger("PLanim", 19);//смотрим вниз
												  //в противном случае
				else anim.SetInteger("PLanim", 29);//идём вниз
			
		}
		if (HP<=0&&life)//если персонаж был жив и хп опустилось до нуля
		{
			anim.SetInteger("PLanim", 30);//анимация смерти
			life = false;//персонаж больше не жив)
			
			PlayerPrefs.SetInt("CurScore", Score);
			
			StartCoroutine(DeathCorut());
		}
	}
	
	void ShootMet()//система стрельбы
	{
		if(ClickShoot && posblSht && !isOverheat)//если мы кликнули мышкой и скорострельность позволяет и перегрев менее 100%
		{
			if (ShotAudioSource.isPlaying) //Проверяем, если звук выстрела проигрывается
				ShotAudioSource.Stop();//останавливаем
			
			ShotAudioSource.Play(); //Проигрываем
			Instantiate(bullet, player.transform.position, Quaternion.identity);//создаем пулю
			tmNotPsblSht = 25 - PlayerPrefs.GetInt("BulletSpeed");//пол секунды нельзя стрелять(метод выполняется 50 раз в секунду)
			posblSht = false;//отключаем стрельбу
		}
		if(tmNotPsblSht>0)//если время без стрельбы >0
			tmNotPsblSht--;//уменьшаем время
		
		if(tmNotPsblSht <= 0)//если время без стрельбы = 0
		{
			tmNotPsblSht = 0;//выравнивам
			posblSht = true;//включаем возможность стрельбы!
		}
		
		if(ClickShoot && !isOverheat)	//если кликаем на стрельбу и перегрев < 100%
		{
			//если разница 0.2 то 1 прокачка должна снимать 0.06 перегрева и вычитать её из нижней строки
			Overheat += 0.4f + (PlayerPrefs.GetInt("BulletSpeed")/25f) - (  (0.8f-(1f - (0.4f + (PlayerPrefs.GetInt("BulletSpeed")/25f))))/10f * PlayerPrefs.GetInt("Overheat"));//перегреваем)
		}
			
		if (Overheat>=99)//если перегрев достиг 100%
		{
			isOverheat = true;//низя стрелять)
			txtOverheat.enabled = isOverheat;//надпись "перегрев"
		}
			
		if(!ClickShoot && !isOverheat && posblSht)//если игрок не пытается стрелять, охлаждаем оружие
			Overheat -= 1f;
		
		if(Overheat<=0)//оружие охладилось
		{
			isOverheat = false;
			txtOverheat.enabled = isOverheat;
			Overheat=0;
		}
		if(isOverheat && posblSht)//случился 100% перегрев, снижаем его
			Overheat -= 0.6f;
		
		
		imgFillOverheat.fillAmount = Mathf.Round(Overheat)/100;
	}
	
	public void hpGen()
	{
		if(PlayerPrefs.GetInt("Heart")<10)
		{
			imgHearth = Hearth12.GetComponent<Image>();
			imgHearth.enabled = false;
			if(PlayerPrefs.GetInt("Heart")<9)
			{
				imgHearth = Hearth11.GetComponent<Image>();
				imgHearth.enabled = false;
				if(PlayerPrefs.GetInt("Heart")<8)
				{
					imgHearth = Hearth10.GetComponent<Image>();
					imgHearth.enabled = false;
					if(PlayerPrefs.GetInt("Heart")<7)
					{
						imgHearth = Hearth9.GetComponent<Image>();
						imgHearth.enabled = false;
						if(PlayerPrefs.GetInt("Heart")<6)
						{
							imgHearth = Hearth8.GetComponent<Image>();
							imgHearth.enabled = false;
							if(PlayerPrefs.GetInt("Heart")<5)
							{
								imgHearth = Hearth7.GetComponent<Image>();
								imgHearth.enabled = false;
								if(PlayerPrefs.GetInt("Heart")<4)
								{
									imgHearth = Hearth6.GetComponent<Image>();
									imgHearth.enabled = false;
									if(PlayerPrefs.GetInt("Heart")<3)
									{
										imgHearth = Hearth5.GetComponent<Image>();
										imgHearth.enabled = false;
										if(PlayerPrefs.GetInt("Heart")<2)
										{
											imgHearth = Hearth4.GetComponent<Image>();
											imgHearth.enabled = false;
											if(PlayerPrefs.GetInt("Heart")<1)
											{
												imgHearth = Hearth3.GetComponent<Image>();
												imgHearth.enabled = false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
	
	IEnumerator TrackCorut()
	{
		while(true)
		{
			if (GamepadX!=0 || GamepadY!=0)
			{
				TrackPnt.transform.position = new Vector2( player.transform.position.x +(-GamepadX*100f), player.transform.position.y + (-GamepadY*100f));
			}
			
			yield return new WaitForSeconds(3f);
		}
	}
	
	IEnumerator SpawnEnemys()
	{
		while(true)
		{
			if(life && !uiPause.activeSelf)
			{
				CurrSpPnt = Random.Range( 1, transform.childCount );
			
				Instantiate(Enemy, SpawnPnts[CurrSpPnt].transform.position, Quaternion.identity);
			}
			yield return new WaitForSeconds(TimeToSpawn);
		}
	}
	
	IEnumerator DeathCorut()
	{
		while(true)
		{
			yield return new WaitForSeconds(3f);
			//Debug.Log("Мы сдохли");
			//при условии что это не финальная игра
			//запустить сцену магазина
			SceneManager.LoadScene("Shop");
			
		}
	}
	
	
	public void OnClickPause()
	{
		uiPause.SetActive(true);
		uiConfirm.SetActive(false);
		uiButtons.SetActive(true);
		//включить переменную паузы
		//остановить игрока
		//остановить всех противников
		//остановить пули
		//остановить перегрев
		
	}
	
	public void OnClickResume()
	{
		uiPause.SetActive(false);
		
	}
	
	public void OnClickMenu()
	{
		uiButtons.SetActive(false);
		uiConfirm.SetActive(true);
	}
	
	public void OnClickCancel()
	{
		uiButtons.SetActive(true);
		uiConfirm.SetActive(false);
	}
	
	public void OnClickConfirm()
	{
		PlayerPrefs.SetInt("CurScore", Score); 
		RecordRank();
		SceneManager.LoadScene("MainMenu");
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
