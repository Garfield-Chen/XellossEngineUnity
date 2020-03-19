using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoPlayer 
{
	public class Position
	{
		public int MapPosX = 0;
		public int MapPosY = 0;
		public int PlayerPosX = 0;
		public int PlayerPosY = 0;
	}

	public enum PlayerAction
	{
		_move_up = 0x0,
		_move_down,
		_move_left,
		_move_right,
		_move_stop,
		_null,
	}

	public enum PlayerRoleType
	{
		_main = 0x0,
		_enemy ,
		_partner ,
		_observer,
		_null,
	}


	tyoSprite playerSprite = null;
	
	public int moveSpeed = 12;
	float moveDelay = 30.0f;
	public tyoStructure.tyoPointInt moveOffset = new tyoStructure.tyoPointInt();

	float currentMoveDt = 0;
	int playerDepth = 0;
	
	public tyoStructure.tyoPointInt currentPosition = new tyoStructure.tyoPointInt();

	public PlayerAction currentAction = PlayerAction._null;
	public PlayerAction lastAction = PlayerAction._null;

	public PlayerRoleType currentRoleType = PlayerRoleType._null;

	tyoPerfebAnimation perfebAniamtionObject = null;
	BoxCollider2D boxCollider2DObject = null;
	Rigidbody2D rigidbody2DObject = null;

	public uint playerUIDInMap = 0;



	public tyoPlayer()
	{

	}

	public tyoPerfebAnimation GetPerfebAnimationObject()
	{
		return perfebAniamtionObject;
	}

	public void InitPlayerSprite(string _perfebName,PlayerRoleType _roleType,PlayerAction _action)
	{
		GameObject _perfeb = tyoCore.resources.GetPerfebByName(_perfebName);

		if (_perfeb)
		{
			playerSprite = new tyoSprite(_perfeb);

			perfebAniamtionObject = playerSprite.renderNodeObject.GetComponent<tyoPerfebAnimation>();
			boxCollider2DObject = playerSprite.renderNodeObject.AddComponent<BoxCollider2D>();

			if (_roleType == PlayerRoleType._main)
			{
				rigidbody2DObject = playerSprite.renderNodeObject.AddComponent<Rigidbody2D>();
				//rigidbody2DObject.isKinematic = true;
				rigidbody2DObject.freezeRotation = true;
			}

			currentRoleType = _roleType;
			currentAction = _action;

		}
	}

	public tyoSprite GetPlayerSprite()
	{
		return playerSprite;
	}

	public void InitPosition(int _x = 0, int _y = 0)
	{
		currentPosition.X = _x;
		currentPosition.Y = _y;
	}

	public void Update(float _dt)
	{
		if (currentRoleType == PlayerRoleType._main)
		{
			
		}
		
		boxCollider2DObject.size = playerSprite.GetSpriteTextureRectSize();
		currentMoveDt += _dt;
	}

	public bool CheckMoveDt()
	{
		if ( currentMoveDt > moveDelay)
		{
			currentMoveDt = 0.0f;
			return true;
		}

		return false;
	}

}
