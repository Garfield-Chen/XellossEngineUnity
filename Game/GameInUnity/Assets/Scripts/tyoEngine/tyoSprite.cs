using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoSprite : tyoRenderNode 
{
	public class tyoSpriteAnimationNode
	{
		public tyoSpriteAnimationNode()
		{
			
		}

		public List<Sprite> spriteList = new List<Sprite>();
		public int curIndex = 0;
		
		public Sprite GetCurSprite()
		{
			return spriteList[curIndex];
		} 

		public void NextFrame()
		{
			curIndex++;

			if ( curIndex >= spriteList.Count )
			{
				curIndex = 0;
			}
		}
		public bool SetFrame(int _frame)
		{
			if ( _frame >= 0 && _frame < spriteList.Count )
			{
				curIndex = _frame;
				return true;
			}

			return false;
		}
	}

	SpriteRenderer spriteRenderer = null;
	Dictionary<string,tyoSpriteAnimationNode> animationStateList = new Dictionary<string,tyoSpriteAnimationNode>();
	bool isAnimationSprite = false;
	bool singleAnimation = false;
	float frameDt = 100.0f;
	float curDt = 0;
	string curState = "";
	bool singleAnimationLoop = false;
	int singleAnimationIdx = 0;
	int singleAnimationStartIdx = 0;
	string singleAnimationSpriteName = "";
	bool singleAnimationStop = false;
	bool singleAnimationEndFrame = false;

	public tyoSprite(Sprite _spr)
	{
		base.renderNodeObject = new GameObject();
		spriteRenderer = base.renderNodeObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = _spr;

	}

	public tyoSprite(GameObject _perfeb)
	{
		base.renderNodeObject = GameObject.Instantiate(_perfeb);
		spriteRenderer = base.renderNodeObject.GetComponent<SpriteRenderer>();
	}

	public tyoSprite(string _animationFile)
	{
		base.renderNodeObject = new GameObject();
		isAnimationSprite = true;

		InitAnimation( _animationFile );

		base.renderNodeObject.name = name;
		
		spriteRenderer = base.renderNodeObject.AddComponent<SpriteRenderer>();

	}

	public tyoSprite(string _animationSpriteName,float _frameDt,bool _loop = true)
	{
		base.renderNodeObject = new GameObject();
		singleAnimation = true;
		singleAnimationLoop = _loop;

		curDt = 0;
		frameDt = _frameDt;
		singleAnimationIdx = 0;
		singleAnimationSpriteName = _animationSpriteName;
		singleAnimationEndFrame = false;

		spriteRenderer = base.renderNodeObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = tyoCore.resources.GetSpriteByName(string.Format("{0}{1}",singleAnimationSpriteName,singleAnimationIdx));
		
	}
	
	public void PlaySingleAnimation(int _frameIdx = -1,bool _loop = true)
	{
		singleAnimationStop = false;
		singleAnimationLoop = _loop;
		singleAnimationIdx = _frameIdx;
		singleAnimationStartIdx = _frameIdx;
		singleAnimationEndFrame = false;
		curDt = 0;
	}

	public void StopSingleAnimation()
	{
		spriteRenderer.sprite = null;
		singleAnimationStop = true;
	}

	public void SetSingleAnimationFrame(int _idx)
	{
		singleAnimationStartIdx = _idx;
		singleAnimationIdx = _idx;
		spriteRenderer.sprite = tyoCore.resources.GetSpriteByName(string.Format("{0}{1}",singleAnimationSpriteName,_idx));
	}

	public bool IsSingleAniatmionPlay()
	{
		return !singleAnimationStop;
	}

	public bool IsSingleAniatmionEndFrame()
	{
		//tyoCore.Log(singleAnimationEndFrame.ToString() + "," + singleAnimationStop.ToString());
		return singleAnimationEndFrame;
	}

	public void ChangeSingleAniamtionSprite(string _animationSpriteName)
	{
		singleAnimationSpriteName = _animationSpriteName;
	}

	bool InitAnimation(string _file)
	{
		tyoFile _tyoFile = new tyoFile(true);
		if ( _tyoFile.LoadFile(_file) )
		{
			string _txt = _tyoFile.GetText();

            int _idx = _txt.IndexOf("\r\n");

            if ( _idx >= 0 )
            {
                _txt = _txt.Replace("\r\n", "");
            }
            else
            {
                _idx = _txt.IndexOf("\n");

                if ( _idx >=0 )
                {
                    _txt = _txt.Replace("\n", "");
                }
            }
            

			string[] _lines =_txt.Split(';');

			foreach ( string _line in _lines)
			{
				string[] _setting = _line.Split(':');

                if ( _setting.Length == 2 )
				{
                    if ( _setting[0] == "name" )
					{
						string[] _infos = _setting[1].Split(',');
						name = _infos[0]; //name
						frameDt = float.Parse(_infos[1]); // frame delay time
                    }
					else if ( _setting[0] == "texture")
					{
                        int _texcount = tyoCore.resources.LoadSprites(_setting[1]);

                        tyoCore.Log("Texload Count:" + _texcount + _setting[1]);
					}
					else
					{
						if ( !animationStateList.ContainsKey(_setting[0]) )
						{
							string[] _animationNames = _setting[1].Split(',');

							if ( _animationNames.Length > 0 )
							{
								tyoSpriteAnimationNode _node = new tyoSpriteAnimationNode();

								foreach ( string _animationName in _animationNames)
								{
									if ( _animationName.Length > 0 )
									{
										Sprite _spr = tyoCore.resources.GetSpriteByName(_animationName);

										if ( _spr != null )
										{
											_node.spriteList.Add(_spr);
										}
										else
										{
											tyoCore.Log("Don't load " + _animationName + " [tyoSprite]");
										}
									}
								}

								if ( _node.spriteList.Count > 0 )
								{
									animationStateList.Add(_setting[0],_node);
									tyoCore.Log("Add animation type : " + _setting[0] + " [tyoSprite]");
								}
							}
						}
						else
						{
							tyoCore.Log("Have same name by animation type : " + _setting[0] + " [tyoSprite]");
						}
					}
				}
			}

			return true;
		}

		return false;
	}

	public SpriteRenderer GetSpriteRenderer()
	{
		return spriteRenderer;
	}

	public bool AnimationChangeState(string _state)
	{
		if ( animationStateList.ContainsKey(_state) )
		{
			curState = _state;
			animationStateList[_state].curIndex = 0;
			spriteRenderer.sprite = animationStateList[_state].GetCurSprite();
			return true;
		}

		return false;
	}

	void NextFrame()
	{
		animationStateList[curState].NextFrame();
		spriteRenderer.sprite = animationStateList[curState].GetCurSprite();
	}

	void SingleAnimationNextFrame()
	{
		if ( singleAnimationStop || singleAnimationEndFrame)
		{
			return;
		}

		singleAnimationIdx++;

		Sprite _spr = tyoCore.resources.GetSpriteByName(string.Format("{0}{1}",singleAnimationSpriteName,singleAnimationIdx));
				
		if ( _spr != null )
		{
			spriteRenderer.sprite = _spr;
		}
		else
		{
			if ( singleAnimationLoop )
			{
				singleAnimationIdx = singleAnimationStartIdx;
			}
			else
			{
				singleAnimationEndFrame = true;
			}
			
		}
	}

	public override void Update(float _dt) 
	{
		if ( isAnimationSprite || singleAnimation )
		{
			curDt += _dt;

			if ( curDt > frameDt )
			{
				if ( singleAnimation )
				{
					SingleAnimationNextFrame();
				}
				else
				{
					NextFrame();
				}
				
				curDt = 0;
			}
		}

		base.UpdateEffect(_dt);

		if ( base.colorFlag )
		{
			spriteRenderer.color = base.GetColor();
		}

		// 3d pos to screen 2d pos , and 0,0 it's screen left top.

		Vector3 _scale = base.GetScale();
		Vector3 _pos = base.GetPosition();
		Vector3 _rot = base.GetRotation();

		if ( base.scaleFlag )
		{
			base.renderNodeObject.transform.localScale = _scale;
		}
		
		//tyoCore.Log(_scale.x.ToString() + " , " + _scale.y.ToString());

		if ( base.positionFlag )
		{
			base.renderNodeObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
			 _pos.x, 
				Screen.height - _pos.y, 
				_pos.z));
		}

		if ( base.rotationFlag )
		{
			base.renderNodeObject.transform.Rotate(_rot);
		}

		base.Update(_dt);
		
	}

	public void ScaleToFullScreen()
	{
		float _sws = Screen.width / spriteRenderer.sprite.textureRect.width;
		float _shs = Screen.height / spriteRenderer.sprite.textureRect.height;

		base.SetScale( _sws, _shs);
	}

	public void ScaleToFullWidth()
	{
		float _sws = Screen.width / spriteRenderer.sprite.textureRect.width;
		base.SetScaleX( _sws);
	}

	public void ScaleToFullHeight()
	{
		float _shs = Screen.height / spriteRenderer.sprite.textureRect.height;
		base.SetScaleY(_shs);
	}

	public float GetTextureWidthInScreen()
	{
		return spriteRenderer.sprite.textureRect.width;
	}

	public float GetTextureHeightInScreen()
	{
		return spriteRenderer.sprite.textureRect.height;
	}

}
