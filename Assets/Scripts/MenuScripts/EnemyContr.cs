using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContr : MonoBehaviour
{
	public GameObject GameManager;
	private GameObject[] Pnts;
	private GameObject player;
	int PntNum;
	public int HP;
	public float speed = 30;
	bool PlayerTrack;
	bool Attack;
	int tmAttack;
	float ang;
	int r;//рандом
	int Strong;
	public GameObject CoinObj;
	
	private Animator anim;
	
	Rigidbody2D rb;
	
	public Behavior script;
	
	Vector2 targetDir;
	
	bool OneDeath;
	
    void Start()
    {
		HP = 30;
		Strong = 1;
		
		OneDeath = true;
		speed *= 100;
		PlayerTrack = false;
		rb = GetComponent<Rigidbody2D>();
		
		anim = GetComponent<Animator>();
		
		player = GameObject.Find("Player");
		GameManager = GameObject.Find("GM");
        Pnts = GameObject.FindGameObjectsWithTag("Pnt");
		PntNum = Random.Range( 0, 6 );
		tmAttack = 0;
		script = GameManager.GetComponent<Behavior>();
		
		if(script.Score>200)
			HP = 30 + (script.Score/150)*10;//счёт поделить на 150 нацело и умножить на 10;
		if(script.Score>300)
			Strong = 1 + (script.Score/300);//счёт поделить на 150 нацело и умножить на 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(HP>=1)
		{
			if(!PlayerTrack)
			{
				targetDir = Pnts[PntNum].transform.position - transform.position ;
			}
			if(PlayerTrack)
				targetDir = player.transform.position - transform.position;
			
			if(targetDir.magnitude<10f && PlayerTrack && !script.uiPause.activeSelf)
			{
				targetDir = Vector2.zero;
				if(!Attack && HP>=1 && script.HP>0)
				{
					
					//отнимаем хп игрока
					script.HP -= Strong;
					
					//включаем таймер для второй атаки
					Attack = true;
				}
			}
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//анимка
			
				ang = Vector2.SignedAngle(targetDir, Vector2.up);
				//Debug.Log(vision.transform.position);
				
				
				if (ang<0)
					ang = 360f+ang;
				
				//Debug.Log(ang);
				
				
				if (ang>315 || ang<45)//если мышка сверху
					if(targetDir.magnitude<10f || script.HP<=0)//если персонаж не двигается
						anim.SetInteger("ENanim", 10);//смотрим ввверх
													  //в противном случае
					else anim.SetInteger("ENanim", 20);//идём вверх
				
				if (ang>45f && ang<135)//если мышка справа
					if(targetDir.magnitude<10f || script.HP<=0)//если персонаж не двигается
						anim.SetInteger("ENanim", 13);//смотрим вправо
													  //в противном случае
					else anim.SetInteger("ENanim", 23);//движение вправо
				
				if (ang>135 && ang<225)//если мышка снизу
					if(targetDir.magnitude<10f || script.HP<=0)//если персонаж не двигается
						anim.SetInteger("ENanim", 16);//смотрим вниз
													  //в противном случае
					else anim.SetInteger("ENanim", 26);//идём вниз
				
				if (ang>225 && ang<315)//если мышка слева
					if(targetDir.magnitude<10f || script.HP<=0)//если персонаж не двигается
						anim.SetInteger("ENanim", 19);//смотрим вниз
													  //в противном случае
					else anim.SetInteger("ENanim", 29);//идём вниз
				
				
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		else//смэрть
		{
			targetDir = Vector2.zero;
			if(OneDeath)
			{
				StartCoroutine(Death());
				OneDeath = false;
			}
		}
		//при условии что игрок жив
		if(script.HP>0 && !script.uiPause.activeSelf)
			rb.velocity = targetDir.normalized * speed  * Time.fixedDeltaTime;//нормализовать вектор идущий от противника к точке
		else
			rb.velocity = Vector2.zero;
			
		
		
		if(targetDir.magnitude<30f && !PlayerTrack)//расстояние до точки меньше чем...
		{
			PlayerTrack = true;
		}
		
		
		if(Attack)
		{
			tmAttack++;
		}else
			tmAttack = 0;
		
		if (tmAttack>=100)
		{
			tmAttack = 0;
			Attack = false;
		}
    }
	
	IEnumerator Death()
	{
		anim.SetInteger("ENanim", 30);//анимация смерти
		//шанс 13% заспавнить монетку
		r = Random.Range( 1, 100 );
		if((r<=15 + (PlayerPrefs.GetInt("Shield")*2)) && (PlayerPrefs.GetInt("coins")<60))
		{
			Instantiate(CoinObj, transform.position, Quaternion.identity);
		}
		//Debug.Log(r);
		
		script.Score += 6 + (script.Score/150);
		yield return new WaitForSeconds(5);
		
		Destroy(this.gameObject);
	}
}
