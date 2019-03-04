using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : tyoScene.tyoSceneNode
{
    tyoMap battleMap = null;

    tyoPlayer mainPlayer = null;

    tyoSprite controlPadUI = null;
		
    public BattleScene(string _name) : base(_name)
    {
		
    }
	
    public override void SceneUpdate(float _dt)
    {
        	
        if (mainPlayer.currentAction == tyoPlayer.PlayerAction._move_stop )
        {
            controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("null");
        }
        else if (mainPlayer.currentAction == tyoPlayer.PlayerAction._move_left )
        {
            controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("left");
        }
        else if (mainPlayer.currentAction == tyoPlayer.PlayerAction._move_right )
        {
            controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("right");
        }
        else if (mainPlayer.currentAction == tyoPlayer.PlayerAction._move_up )
        {
            controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("up");
        }
        else if (mainPlayer.currentAction == tyoPlayer.PlayerAction._move_down )
        {
            controlPadUI.renderNodeObject.GetComponent<tyoPerfebAnimation>().ChangeAnimation("down");
        }

        battleMap.UpdateMap(_dt);
        battleMap.RenderMap(this); 
		
        base.SceneUpdate(_dt);
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

        battleMap.LoadMap("Map\\castle");

        mainPlayer = new tyoPlayer();
        mainPlayer.InitPlayerSprite("Player2");
        mainPlayer.currentRoleType = tyoPlayer.PlayerRoleType._main;
        mainPlayer.currentAction = tyoPlayer.PlayerAction._move_stop;
        mainPlayer.GetPlayerSprite().name = "Main Player";
        mainPlayer.GetPlayerSprite().AddSceneRender(this);

        battleMap.AddMapPlayer(mainPlayer);
        battleMap.SetPlayerRandomPos(mainPlayer.playerUIDInMap);

        GameObject _perfeb = tyoCore.resources.GetPerfebByName("ControlPad");

		if (_perfeb)
		{
			controlPadUI = new tyoSprite(_perfeb);

			if ( controlPadUI != null )
			{
				controlPadUI.name = "control pad";
				controlPadUI.AddSceneRender(this);
				controlPadUI.SetPosition(controlPadUI.GetTextureWidthInScreen() + 120, Screen.height - controlPadUI.GetTextureHeightInScreen() - 100, -100.0f);
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
