using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContr : MonoBehaviour
{
	public int Damage = 1;
	public float speed;
	private Transform player;
	Vector2 targetDir;
	Vector2 target;

    EnemyContr script;
	Beh GMscript;
	
    // Start is called before the first frame update
    void Start()
    {
		GMscript = GameObject.Find("GM").GetComponent<Beh>();
		
		Damage *= 10;
		Damage += 5 * PlayerPrefs.GetInt("Attack");
		
		player = GameObject.Find("Player").transform;
		if(GMscript.Mobile)
		{
			targetDir = GMscript.JST2Scr.LastMovementVector;
		}
		else
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);/*new Vector2(Input.mousePosition.x - (Screen.width/2), Input.mousePosition.y - (Screen.height/2));*/
			target = new Vector2(target.x+10f, target.y-10f);
			Vector2 PlPos = new Vector2(player.position.x, player.position.y);
			targetDir = target - PlPos;/*Vector2.zero;*/
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(GMscript.uiPause.activeSelf)
			speed = 0f;
		else
			speed = 3f + (PlayerPrefs.GetInt("BulletSpeed")/5f);
		
		transform.Translate(targetDir.normalized * speed);
	   //Debug.Log(target);
    }
	
	void OnTriggerEnter2D(Collider2D coll) {    
		if (coll.gameObject.name == "wall") { 
			Destroy(gameObject);
		}
		if (coll.gameObject.tag == "Enemy") {
			script = coll.gameObject.GetComponent<EnemyContr>();
			if(script.hp>0)
			{
				if(Damage - script.hp>0)
				{
					//пуля убивает противника и должна лететь дальше
					Damage -= script.hp;
					script.hp = 0;
				}
				else
				{
					//у противника хп больше чем атака пули
					script.hp -= Damage;
					Destroy(gameObject);
				}
				
			}
		}
	}
	
}
