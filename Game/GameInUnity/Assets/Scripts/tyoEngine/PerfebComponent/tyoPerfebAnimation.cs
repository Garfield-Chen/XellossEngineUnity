using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoPerfebAnimation : MonoBehaviour 
{
	[Serializable]
	public class tyoPerfebAnimationNode
	{
		public string animationName;
		public List<Sprite> animationSpriteList = null;
		public tyoPerfebAnimationNode()
		{

		}
	}
	
	Dictionary<string,List<Sprite>> animationSpriteList = new Dictionary<string,List<Sprite>>();
	public tyoPerfebAnimationNode[] animationSpriteNodeList;
	public string currentAnimationName = "";
	public int frameDeltaTime = 100;
	SpriteRenderer spriteRenderObject = null;
	int curAniDtTotal = 0;
	int curAniFrameIdx = 0;
	// Use this for initialization
	void Start () 
	{
		foreach(tyoPerfebAnimationNode _animationNode in animationSpriteNodeList)
		{
			animationSpriteList.Add(_animationNode.animationName,_animationNode.animationSpriteList);
		}

		animationSpriteNodeList = null;

	 	spriteRenderObject = gameObject.GetComponent<SpriteRenderer>();
		
		if ( spriteRenderObject == null )
		{
			tyoCore.Log("spriteRenderObject = null , tyoPerfebAnimation");
		}
		else
		{
			if ( currentAnimationName.Length > 0 )
			{
				if ( animationSpriteList.ContainsKey(currentAnimationName) )
				{
					if ( animationSpriteList[currentAnimationName].Count > 0)
					{
						spriteRenderObject.sprite = animationSpriteList[currentAnimationName][0];
						curAniFrameIdx = 0;
					}
				}
			}
			
		}
	}
	// Update is called once per frame
	void Update () 
	{
		float _dt = 1000 * Time.deltaTime;

		if ( curAniDtTotal < frameDeltaTime )
		{
			curAniDtTotal += (int)_dt;
		}
		else
		{
			curAniDtTotal = 0;
			ChangeFrame();
		}
	}

	void ChangeFrame()
	{
		curAniFrameIdx++;

		if ( currentAnimationName.Length > 0 )
		{
			if ( animationSpriteList.ContainsKey(currentAnimationName) )
			{
				if ( curAniFrameIdx >= animationSpriteList[currentAnimationName].Count )
				{
					curAniFrameIdx = 0;
				}

				spriteRenderObject.sprite = animationSpriteList[currentAnimationName][curAniFrameIdx];
			}
		}
	}

	public bool ChangeAnimation(string _name)
	{
		if ( animationSpriteList.ContainsKey(_name) )
		{
			if ( animationSpriteList[_name].Count > 0)
			{
				spriteRenderObject.sprite = animationSpriteList[_name][0];
				curAniFrameIdx = 0;

				currentAnimationName = _name;
				return true;
			}
		}

		return false;
	}
}
