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
	
	public int moveSpeed = 24;
	float moveDelay = 70.0f;
	public tyoStructure.tyoPointInt moveOffset = new tyoStructure.tyoPointInt();

	float currentMoveDt = 0;

	int playerDepth = 0;
	
	public tyoStructure.tyoPointInt currentPosition = new tyoStructure.tyoPointInt();

	public PlayerAction currentAction = PlayerAction._null;
	public PlayerAction lastAction = PlayerAction._null;

	public PlayerRoleType currentRoleType = PlayerRoleType._null;

	tyoPerfebAnimation perfebAniamtionObject = null;

	public uint playerUIDInMap = 0;

	const float inputControlGridSize = 170.0f;

	UnityEngine.Rect leftControl_rect = new Rect(50.0f /* + 100.0f * 0*/,Screen.height - inputControlGridSize * 2 - 50.0f, inputControlGridSize, inputControlGridSize);
		//right
	UnityEngine.Rect rightControl_rect = new Rect(50.0f + inputControlGridSize * 2,Screen.height -inputControlGridSize* 2 - 50.0f, inputControlGridSize, inputControlGridSize);
	//up
	UnityEngine.Rect upControl_rect = new Rect(50.0f + inputControlGridSize * 1,Screen.height - inputControlGridSize * 3 - 50.0f, inputControlGridSize, inputControlGridSize);
	//down
	UnityEngine.Rect downControl_rect = new Rect(50.0f + inputControlGridSize * 1,Screen.height - inputControlGridSize * 1 - 50.0f, inputControlGridSize, inputControlGridSize);

	public tyoPlayer()
	{

	}

	public void InitPlayerSprite(string _perfebName)
	{
		GameObject _perfeb = tyoCore.resources.GetPerfebByName(_perfebName);

		if (_perfeb)
		{
			playerSprite = new tyoSprite(_perfeb);

			perfebAniamtionObject = playerSprite.renderNodeObject.GetComponent<tyoPerfebAnimation>();
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
		Vector2 _touchPos = new Vector2(-1.0f,-1.0f);

		if ( tyoCore.input.IsTouch() )
		{
			_touchPos = tyoCore.input.GetCurMousePos();
		}

		if ( Input.GetKey( KeyCode.A ) || leftControl_rect.Contains(_touchPos))
		{
			if ( currentAction != PlayerAction._move_left && moveOffset.X == 0 && moveOffset.Y == 0)
			{
				currentAction = PlayerAction._move_left;

				if ( perfebAniamtionObject != null )
				{
					perfebAniamtionObject.ChangeAnimation("left");
				}
			}
		}
		else if ( Input.GetKey( KeyCode.D ) || rightControl_rect.Contains(_touchPos))
		{
			if ( currentAction != PlayerAction._move_right && moveOffset.X == 0 && moveOffset.Y == 0)
			{
				currentAction = PlayerAction._move_right;

				if ( perfebAniamtionObject != null )
				{
					perfebAniamtionObject.ChangeAnimation("right");
				}
			}
			
		}
		else if ( Input.GetKey( KeyCode.W ) || upControl_rect.Contains(_touchPos))
		{
			if ( currentAction != PlayerAction._move_up && moveOffset.X == 0 && moveOffset.Y == 0)
			{
				currentAction = PlayerAction._move_up;

				if ( perfebAniamtionObject != null )
				{
					perfebAniamtionObject.ChangeAnimation("up");
				}
			}
			
		}
		else if ( Input.GetKey( KeyCode.S ) || downControl_rect.Contains(_touchPos))
		{
			if ( currentAction != PlayerAction._move_down && moveOffset.X == 0 && moveOffset.Y == 0)
			{
				currentAction = PlayerAction._move_down;

				if ( perfebAniamtionObject != null )
				{
					perfebAniamtionObject.ChangeAnimation("down");
				}
			}
		}
		else
		{
			if ( moveOffset.X == 0 && moveOffset.Y == 0 )
			{
				currentAction = PlayerAction._move_stop;
			}
			
		}

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
