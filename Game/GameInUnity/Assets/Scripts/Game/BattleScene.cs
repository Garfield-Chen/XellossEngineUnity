using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : tyoScene.tyoSceneNode
{
    tyoMap battleMap = null;

    tyoPlayer mainPlayer = null;

    tyoPlayer testPlayer = null;

    tyoSprite controlPadUI = null;

    const float inputControlGridSize = 170.0f;

	UnityEngine.Rect leftControl_rect = new Rect(50.0f /* + 100.0f * 0*/,Screen.height - inputControlGridSize * 2 - 50.0f, inputControlGridSize, inputControlGridSize);
		//right
	UnityEngine.Rect rightControl_rect = new Rect(50.0f + inputControlGridSize * 2,Screen.height -inputControlGridSize* 2 - 50.0f, inputControlGridSize, inputControlGridSize);
	//up
	UnityEngine.Rect upControl_rect = new Rect(50.0f + inputControlGridSize * 1,Screen.height - inputControlGridSize * 3 - 50.0f, inputControlGridSize, inputControlGridSize);
	//down
	UnityEngine.Rect downControl_rect = new Rect(50.0f + inputControlGridSize * 1,Screen.height - inputControlGridSize * 1 - 50.0f, inputControlGridSize, inputControlGridSize);
		
    public BattleScene(string _name) : base(_name)
    {
		
    }
	
    public override void SceneUpdate(float _dt)
    {
        
        ControlUpdate();

        
        battleMap.UpdateMap(_dt);
        battleMap.RenderMap(); 
		
        base.SceneUpdate(_dt);
    }

    void ControlUpdate()
    {
        Vector2 _touchPos = new Vector2(-1.0f,-1.0f);

        if ( tyoCore.input.IsTouch() )
        {
            _touchPos = tyoCore.input.GetCurMousePos();
        }

        if ( Input.GetKey( KeyCode.A ) || leftControl_rect.Contains(_touchPos))
        {
            if ( mainPlayer.currentAction != tyoPlayer.PlayerAction._move_left && mainPlayer.moveOffset.X == 0 && mainPlayer.moveOffset.Y == 0)
            {
                mainPlayer.currentAction = tyoPlayer.PlayerAction._move_left;

                if ( mainPlayer.GetPerfebAnimationObject() != null )
                {
                    mainPlayer.GetPerfebAnimationObject().ChangeAnimation("left");
                }

                controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("left");
            }
        }
        else if ( Input.GetKey( KeyCode.D ) || rightControl_rect.Contains(_touchPos))
        {
            if ( mainPlayer.currentAction != tyoPlayer.PlayerAction._move_right && mainPlayer.moveOffset.X == 0 && mainPlayer.moveOffset.Y == 0)
            {
                mainPlayer.currentAction = tyoPlayer.PlayerAction._move_right;

                if ( mainPlayer.GetPerfebAnimationObject() != null )
                {
                    mainPlayer.GetPerfebAnimationObject().ChangeAnimation("right");
                }

                controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("right");
            }
            
        }
        else if ( Input.GetKey( KeyCode.W ) || upControl_rect.Contains(_touchPos))
        {
            if ( mainPlayer.currentAction != tyoPlayer.PlayerAction._move_up && mainPlayer.moveOffset.X == 0 && mainPlayer.moveOffset.Y == 0)
            {
                mainPlayer.currentAction = tyoPlayer.PlayerAction._move_up;

                if ( mainPlayer.GetPerfebAnimationObject() != null )
                {
                    mainPlayer.GetPerfebAnimationObject().ChangeAnimation("up");
                }
                
                controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("up");
            }
            
        }
        else if ( Input.GetKey( KeyCode.S ) || downControl_rect.Contains(_touchPos))
        {
            if ( mainPlayer.currentAction != tyoPlayer.PlayerAction._move_down && mainPlayer.moveOffset.X == 0 && mainPlayer.moveOffset.Y == 0)
            {
                mainPlayer.currentAction = tyoPlayer.PlayerAction._move_down;

                if ( mainPlayer.GetPerfebAnimationObject() != null )
                {
                    mainPlayer.GetPerfebAnimationObject().ChangeAnimation("down");
                }

                controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("down");
            }
        }
        else
        {
            if ( mainPlayer.moveOffset.X == 0 && mainPlayer.moveOffset.Y == 0 )
            {
                mainPlayer.currentAction = tyoPlayer.PlayerAction._move_stop;
                controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("null");
            }
            
        }
    }

	public override void EnterScene()
    {
        
        if ( battleMap != null )
        {
            battleMap.Clear();
        }
        else
        {
            battleMap = new tyoMap();
        }

        battleMap.LoadMap("Map\\castle",this);

        mainPlayer = new tyoPlayer();
        mainPlayer.InitPlayerSprite("Player2",tyoPlayer.PlayerRoleType._main,tyoPlayer.PlayerAction._move_stop);
        mainPlayer.GetPlayerSprite().name = "Main Player";
        mainPlayer.GetPlayerSprite().AddSceneRender(this);

        testPlayer = new tyoPlayer();
        testPlayer.InitPlayerSprite("Player1",tyoPlayer.PlayerRoleType._partner,tyoPlayer.PlayerAction._move_stop);
        testPlayer.GetPlayerSprite().name = "Test Player";
        testPlayer.GetPlayerSprite().AddSceneRender(this);

        battleMap.AddMapPlayer(mainPlayer);
        battleMap.SetPlayerRandomPos(mainPlayer.playerUIDInMap);

        battleMap.AddMapPlayer(testPlayer);
        battleMap.SetPlayerRandomPos(testPlayer.playerUIDInMap);

        GameObject _perfeb = tyoCore.resources.GetPerfebByName("ControlPad");

		if (_perfeb)
		{
			controlPadUI = new tyoSprite(_perfeb);

			if ( controlPadUI != null )
			{
				controlPadUI.name = "control pad";
				controlPadUI.AddSceneRender(this);

				controlPadUI.SetPosition((50.0f + 170.0f + 170.0f / 2) - controlPadUI.GetTextureWidthInScreen() / 2 + 40.0f, 
                                (Screen.height - 170.0f * 2 - 50.0f + 170.0f / 2) - controlPadUI.GetTextureHeightInScreen() / 2 + 50.0f,
                                 -100.0f);
			}
		}

		base.EnterScene();
	}

	public override void LeaveScene()
	{
 		base.DestroyAllRenderNode();	 
		base.LeaveScene();
	}
}
