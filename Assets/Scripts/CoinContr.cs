using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinContr : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll) {    
		if (coll.gameObject.tag == "Player") { 
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")+1 );
			Destroy(this.gameObject);
		}
	}
}
