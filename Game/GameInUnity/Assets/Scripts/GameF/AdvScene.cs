using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvScene : tyoScene.tyoSceneNode
{
	tyoSprite playerSprite = null;
	List<tyoSprite> playerBulletList = new List<tyoSprite>();
	tyoSprite bgASprite = null;
	tyoSprite bgBSprite = null;
	tyoSprite bgCSprite = null;
	List<tyoSprite> monsterList = new List<tyoSprite>();

	int leftPlayerPos = 0;
	int rightPlayerPos = 0;
	float playerPosX = 0.0f;

	int playerSpeed = 30;
	int playerAttackSpeed = 200;
	float playerAttackDt = 0.0f;

	int bgMoveSpeed = 0;
	float bgMoveDt = 0.0f;
	int moveDistance = 0;
	int moveLen = 5;

	int spwanMonsterInterval = 0;
	float monsterSpwanDt = 0;
    public AdvScene(string _name) : base(_name)
    {
		
    }
	
    public override void SceneUpdate(float _dt)
    {	
		if ( Input.GetKey( KeyCode.A ))
		{
			float _posx = playerSprite.GetX() - playerSpeed;

			if ( _posx < leftPlayerPos )
			{
				_posx = (float)leftPlayerPos;
			}

			playerSprite.SetX(_posx);
		}
		else if ( Input.GetKey( KeyCode.D ))
		{
			float _posx = playerSprite.GetX() + playerSpeed;

			if ( _posx > rightPlayerPos )
			{
				_posx = (float)rightPlayerPos;
			}

			playerSprite.SetX(_posx);
		}

		if ( tyoCore.input.IsTouch() )
		{
			if ( tyoCore.input.GetCurTouchPos().x < (playerSprite.GetPosition().x - playerSprite.GetTextureWidthInScreen() / 2) )
			{
				float _posx = playerSprite.GetX() - playerSpeed;

				if ( _posx < leftPlayerPos )
				{
					_posx = (float)leftPlayerPos;
				}

				playerSprite.SetX(_posx);
			}
			else if ( tyoCore.input.GetCurTouchPos().x > (playerSprite.GetPosition().x + playerSprite.GetTextureWidthInScreen() / 2) )
			{
				float _posx = playerSprite.GetX() + playerSpeed;

				if ( _posx > rightPlayerPos )
				{
					_posx = (float)rightPlayerPos;
				}

				playerSprite.SetX(_posx);
			}
		}

		playerAttackDt += _dt;

		if ( playerAttackDt > playerAttackSpeed )
		{
			playerAttackDt = 0.0f;
			SpawnBullet();
		}

		for ( int i = 0; i < playerBulletList.Count; ++i )
		{
			if ( !playerBulletList[i].renderNodeObject.activeSelf ||
				 playerBulletList[i].GetY() < (-1 * playerBulletList[i].GetTextureHeightInScreen() - 50.0f) )
			{
				playerBulletList[i].RemoveSceneRender(this);
				playerBulletList[i] = null;
				playerBulletList.RemoveAt(i);
			}
			else
			{
				playerBulletList[i].SetY( playerBulletList[i].GetY() - 20.0f );
			}
		}

		BackgroundUpdate(_dt);
		MonsterUpdate(_dt);
		
        base.SceneUpdate(_dt);
    }

	void SpawnBullet()
	{
		GameObject _perfeb = tyoCore.resources.GetPerfebByName("PlayerABullet");

		if (_perfeb)
		{
			tyoSprite _bullet = new tyoSprite(_perfeb);

			if ( _bullet != null )
			{
				_bullet.name = "bullet";
				_bullet.AddSceneRender(this);
				_bullet.SetPosition( playerSprite.GetX(), playerSprite.GetY() - playerSprite.GetTextureHeightInScreen() / 2 - _bullet.GetTextureHeightInScreen() );
				//playerSprite.SetPosition(Screen.width / 2, Screen.height / 2,10.0f);
				//playerSprite.SetPosition(Screen.width / 2, Screen.height - playerSprite.GetTextureHeightInScreen() - 30, 10.0f);

				playerBulletList.Add(_bullet);
			}
		}
	}

	void MonsterUpdate(float _dt)
	{
		monsterSpwanDt += _dt;

		for ( int i = 0; i < monsterList.Count; ++i )
		{
			if ( !monsterList[i].renderNodeObject.activeSelf || 
				 monsterList[i].GetY() > (Screen.height + 2 * monsterList[i].GetTextureHeightInScreen()) )
			{
				monsterList[i].RemoveSceneRender(this);
				monsterList[i] = null;
				monsterList.RemoveAt(i);
			}
			else
			{
				monsterList[i].SetY( monsterList[i].GetY() + 10.0f );
			}
		}

		if ( monsterSpwanDt > spwanMonsterInterval)
		{
			monsterSpwanDt = 0;
			spwanMonsterInterval = Random.Range(500,3000);
			SpwanMonster();
		}
	}

	void SpwanMonster()
	{
		int _pos = Random.Range(1,5);

		GameObject _perfeb = tyoCore.resources.GetPerfebByName("MonsterA");

		if (_perfeb)
		{
			tyoSprite _monster = new tyoSprite(_perfeb);

			if ( _monster != null )
			{
				_monster.name = "monster";
				_monster.AddSceneRender(this);
				_monster.SetPosition( (Screen.width - _monster.GetTextureWidthInScreen()) / 5 * _pos , -2 * _monster.GetTextureHeightInScreen() );
				//playerSprite.SetPosition(Screen.width / 2, Screen.height / 2,10.0f);
				//playerSprite.SetPosition(Screen.width / 2, Screen.height - playerSprite.GetTextureHeightInScreen() - 30, 10.0f);

				ShareData.MonsterProperty _property = new ShareData.MonsterProperty();
				_property.maxHP = Random.Range(5,15);
				_monster.renderNodeObject.GetComponent<Monster>().InitMonster(_property);

				monsterList.Add(_monster);
			}
		}
	}

	void BackgroundUpdate(float _dt)
	{
		bgMoveDt += _dt;

		if ( bgMoveDt > bgMoveSpeed )
		{
			bgMoveDt = 0.0f;
			
			moveDistance += moveLen;

			bgASprite.SetY(bgASprite.GetY() + moveLen);
			bgBSprite.SetY(bgBSprite.GetY() + moveLen);
			bgCSprite.SetY(bgCSprite.GetY() + moveLen);

			if ( bgASprite.GetY() > (2 * Screen.height) )
			{
				bgASprite.SetY(bgCSprite.GetY() - Screen.height);
			}
			
			if ( bgBSprite.GetY() > (2 * Screen.height) )
			{
				bgBSprite.SetY(bgASprite.GetY() - Screen.height);
			}
			
			if ( bgCSprite.GetY() > (2 * Screen.height) )
			{
				bgCSprite.SetY(bgBSprite.GetY() - Screen.height);
			}
		}
		
	}

	public override void EnterScene()
    {
		GameObject _perfeb = tyoCore.resources.GetPerfebByName("PlayerA");

		if (_perfeb)
		{
			playerSprite = new tyoSprite(_perfeb);

			if ( playerSprite != null )
			{
				playerSprite.name = "player";
				playerSprite.AddSceneRender(this);
				//playerSprite.SetPosition(Screen.width / 2, Screen.height / 2,10.0f);
				playerSprite.SetPosition(Screen.width / 2, Screen.height - playerSprite.GetTextureHeightInScreen() - 30, 10.0f);
			}

			leftPlayerPos = (int)playerSprite.GetTextureWidthInScreen() + 30;
			rightPlayerPos = Screen.width - (int)playerSprite.GetTextureWidthInScreen() - 30;
		}

		Sprite _spr = tyoCore.resources.GetSpriteByName("bg01");

		if ( _spr != null )
		{
			bgASprite = new tyoSprite(_spr);
			bgASprite.name = "background a";
			bgASprite.AddSceneRender(this);
			bgASprite.ScaleToFullScreen();
			//bgSprite.ScaleToFullWidth();
			bgASprite.SetPosition(Screen.width / 2, Screen.height / 2,20.0f);

			bgBSprite = new tyoSprite(_spr);
			bgBSprite.name = "background b";
			bgBSprite.AddSceneRender(this);
			bgBSprite.ScaleToFullScreen();
			//bgSprite.ScaleToFullWidth();
			bgBSprite.SetPosition(Screen.width / 2, -1 * Screen.height + (Screen.height / 2) ,20.0f);

			bgCSprite = new tyoSprite(_spr);
			bgCSprite.name = "background c";
			bgCSprite.AddSceneRender(this);
			bgCSprite.ScaleToFullScreen();
			//bgSprite.ScaleToFullWidth();
			bgCSprite.SetPosition(Screen.width / 2, -2 * Screen.height + (Screen.height / 2) ,20.0f);
		}

		spwanMonsterInterval = Random.Range(500,3000);

		base.EnterScene();
	}

	public override void LeaveScene()
	{
 		base.DestroyAllRenderNode();	 
		base.LeaveScene();
	}
}
