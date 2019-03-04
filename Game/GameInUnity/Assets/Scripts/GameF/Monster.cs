using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour 
{
	public GameObject HP;
	ShareData.MonsterProperty curProperty = null;
	float curHP = 0;
	float hpScale = 0.0f;
	public void InitMonster(ShareData.MonsterProperty _property)
	{
		curProperty = _property;
		curHP = curProperty.maxHP;

		hpScale = HP.transform.localScale.x;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		if ( curHP <= 0 )
		{
			MonsterDie();
		}
		
	}

	void OnCollisionEnter2D(Collision2D _collision)
    {
		//tyoCore.Log("OnTriggerEnter2D");
        if ( _collision.gameObject.name == "bullet" )
		{
			curHP -= 1;
			//tyoCore.Log("cur :" + curHP.ToString());
			_collision.gameObject.SetActive(false);

			float _new = hpScale * (curHP / curProperty.maxHP);
			print(_new.ToString());
			HP.transform.localScale = new Vector3(hpScale * (curHP / curProperty.maxHP),HP.transform.localScale.y,HP.transform.localScale.z);
		}
		else if ( _collision.gameObject.name == "player" )
		{
			MonsterDie();
			tyoCore.Log("player conllision");
		}
    }

	void MonsterDie()
	{
		this.gameObject.SetActive(false);
	}

}
