using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoRenderNode
{
	enum EffectType
	{	
		none,
		flash,
		autorot,
		movetox,
		movetoy,
		scaletoxy,
		alphatoa,
	}

	class EffectNode
	{
		public EffectType type = EffectType.none; 
		public float curDt = 0.0f;
		public Dictionary<string,float> valueMap = new Dictionary<string,float>();
		public bool curFlag = false;
		public bool disableFlag = false;
		public const float valueTRUE = 1.0f;
		public const float valueFLASE = 0.0f;
	}

	public GameObject renderNodeObject = null;
	public string name = "NewObject";
	List<EffectNode> curEffectList = new List<EffectNode>();
	Vector3 positionVec3 = new Vector3(0.0f, 0.0f, 1.0f);
	Vector3 scaleVec3 = new Vector3(0.0f, 0.0f, 1.0f);
	Vector3 rotationVec3 = new Vector3(0.0f, 0.0f, 0.0f);
	public bool scaleFlag = false;
	public bool positionFlag = true;
	public bool rotationFlag = false;
	public bool colorFlag = false;
	
	Color color = new Color( 1.0f, 1.0f, 1.0f, 1.0f);

	public tyoRenderNode()
	{
		
	}

	public virtual void DelayInit(tyoScene.tyoSceneNode _scene)
	{
		renderNodeObject.transform.SetParent(_scene.RenderNodeListObject.transform);
	}

	public void AddSceneRender(tyoScene.tyoSceneNode _scene)
	{
		_scene.AddRenderNode(this);
	}
	public bool RemoveSceneRender(tyoScene.tyoSceneNode _scene)
	{
		Destroy();
		return _scene.RemoveRenderNode(this);
	}

	void Destroy()
	{
		GameObject.Destroy(renderNodeObject);
		renderNodeObject = null;
	}

	public virtual void Update(float _dt) 
	{
		positionFlag = false;
		scaleFlag = false;
		rotationFlag = false;
		colorFlag = false;

		if ( renderNodeObject.name != name )
		{
			renderNodeObject.name = name;
		}
	}

	public virtual Vector3 GetRotation()
	{
		return rotationVec3;
	}

	public virtual void SetRotation(float _z,float _x = 0.0f,float _y = 0.0f)
	{
		if ( Mathf.Abs(_z) == 360.0f )
		{
			_z = 0.0f;
		}

		rotationVec3.x = _x;
		rotationVec3.y = _y;
		rotationVec3.z = _z;
		rotationFlag = true;
	}

	public virtual void SetPosition(float _x, float _y, float _z = 5.0f)
	{
		positionVec3.x = _x;
		positionVec3.y = _y;
		positionVec3.z = _z;
		positionFlag = true;
	}
	public virtual void SetZ(float _z)
	{
		positionVec3.z = _z;
		positionFlag = true;
	}
	public virtual void SetPosition(Vector3 _pos)
	{
		positionVec3.x = _pos.x;
		positionVec3.y = _pos.y;
		positionVec3.z = _pos.z;
		positionFlag = true;
	}

	public virtual Vector3 GetPosition()
	{
		return positionVec3;
	}

	public virtual float GetX()
	{
		return positionVec3.x;
	}

	public virtual float GetY()
	{
		return positionVec3.y;
	}

	public virtual float GetZ()
	{
		return positionVec3.z;
	}

	public virtual void SetX(float _x)
	{
		positionVec3.x = _x;
		positionFlag = true;
	}

	public virtual void SetY(float _y)
	{
		positionVec3.y = _y;
		positionFlag = true;
	}
	
	public virtual void SetScale(float _x, float _y)
	{
		scaleVec3.x = _x;
		scaleVec3.y = _y;
		scaleVec3.z = 1.0f;
		scaleFlag = true;
	}

	public virtual void SetScaleX(float _x)
	{
		scaleVec3.x = _x;
		scaleFlag = true;
	}

	public virtual void SetScaleY(float _y)
	{
		scaleVec3.y = _y;
		scaleFlag = true;
	}

	public virtual Vector3 GetScale()
	{
		return scaleVec3;
	}

	public virtual void SetColor( float _r, float _g, float _b, float _a)
	{
		color.r = _r;
		color.g = _g;
		color.b = _b;
		color.a = _a;
		colorFlag = true;
	}

	public void SetAlpha(float _a)
	{
		color.a = _a;
		colorFlag = true;
	}

	public virtual Color GetColor()
	{
		return color;
	}

	bool AddEffect(EffectNode _node)
	{
		EffectNode _find = curEffectList.Find( t => t.type == _node.type );

		if ( _find != null )
		{
			curEffectList.Remove(_find);
		}

		if ( _node.type == EffectType.alphatoa )
		{
			_find = curEffectList.Find( t => t.type == EffectType.flash );

			if ( _find != null )
			{
				_find.disableFlag = true;
			}	
		}
		else if ( _node.type == EffectType.flash )
		{
			_find = curEffectList.Find( t => t.type == EffectType.alphatoa );

			if ( _find != null )
			{
				_find.disableFlag = true;
			}
		}
		
		curEffectList.Add(_node);
		return true;
	}

	public bool AddEffect_Flash(float _flashDt,float _changeValue = 0.1f, float _high_alpha = 0.0f, float _low_alpha = 0.0f)
	{
		EffectNode _node = new EffectNode();

		_node.type = EffectType.flash;
		_node.curDt = 0.0f;
		_node.valueMap.Add("flashDt",_flashDt);
		_node.valueMap.Add("changeValue",_changeValue);
		_node.valueMap.Add("lowAlpha",_low_alpha);
		_node.valueMap.Add("highAlpha",_high_alpha);
		_node.valueMap.Add("changeflag",EffectNode.valueFLASE);

		return AddEffect(_node);
	}
	
	public bool AddEffect_AutoRotation(float _rotDt,float _rotValue = 1.0f)
	{
		EffectNode _node = new EffectNode();

		_node.type = EffectType.autorot;
		_node.curDt = 0.0f;
		_node.valueMap.Add("rotDt",_rotDt);
		_node.valueMap.Add("rotValue",_rotValue);

		return AddEffect(_node);
	}

	public bool AddEffect_MoveToX(float _finalX, float _time)
	{
		EffectNode _node = new EffectNode();

		float _distance = (_finalX - positionVec3.x);
		bool _right = false;

		if ( _distance > 0 )
		{
			_right = true;
		}
		else
		{
			_right = false;
		}

		float _frameDistance = Mathf.Abs(_distance / _time);

		_node.type = EffectType.movetox;
		_node.curDt = 0.0f;
		_node.valueMap.Add("finalX",_finalX);
		_node.valueMap.Add("frameDistance",_frameDistance);
		_node.valueMap.Add("time",_time);
		_node.valueMap.Add("isRight",_right ? EffectNode.valueTRUE : EffectNode.valueFLASE );

		return AddEffect(_node);
	}

	public bool AddEffect_MoveToY(float _finalY, float _time)
	{
		EffectNode _node = new EffectNode();

		float _distance = (_finalY - positionVec3.y);
		bool _down = false;

		if ( _distance > 0 )
		{
			_down = true;
		}
		else
		{
			_down = false;
		}

		float _frameDistance = Mathf.Abs(_distance / _time);

		_node.type = EffectType.movetoy;
		_node.curDt = 0.0f;
		_node.valueMap.Add("finalY",_finalY);
		_node.valueMap.Add("frameDistance",_frameDistance);
		_node.valueMap.Add("time",_time);
		_node.valueMap.Add("isDown",_down ? EffectNode.valueTRUE : EffectNode.valueFLASE );

		return AddEffect(_node);
	}

	public bool AddEffect_ScaleToXY(float _finalScaleXY, float _time)
	{
		EffectNode _node = new EffectNode();

		float _distance = (_finalScaleXY - scaleVec3.x);
		bool _scaleUP = false;

		if ( _distance > 0 )
		{
			_scaleUP = true;
		}
		else
		{
			_scaleUP = false;
		}

		float _frameScale = Mathf.Abs(_distance / _time);
		
		_node.type = EffectType.scaletoxy;
		_node.curDt = 0.0f;
		_node.valueMap.Add("finalScaleXY",_finalScaleXY);
		_node.valueMap.Add("frameScale",_frameScale);
		_node.valueMap.Add("time",_time);
		_node.valueMap.Add("isScaleUP",_scaleUP ? EffectNode.valueTRUE : EffectNode.valueFLASE );

		return AddEffect(_node);
	}

	public bool AddEffect_AlphaToA(float _finalAlpha, float _time)
	{
		EffectNode _node = new EffectNode();

		float _color_len = (_finalAlpha - color.a);
		bool _alphaUP = false;

		if ( _color_len > 0 )
		{
			_alphaUP = true;
		}
		else
		{
			_alphaUP = false;
		}

		float _frameAlpha = Mathf.Abs(_color_len / _time);

		

		_node.type = EffectType.alphatoa;
		_node.curDt = 0.0f;
		_node.valueMap.Add("finalAlpha",_finalAlpha);
		_node.valueMap.Add("frameAlpha",_frameAlpha);
		_node.valueMap.Add("time",_time);
		_node.valueMap.Add("isAlphaUP",_alphaUP ? EffectNode.valueTRUE : EffectNode.valueFLASE );

		return AddEffect(_node);
	}

	void EffectUpdate_AlphaToA(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.valueMap["isAlphaUP"] == EffectNode.valueTRUE )
		{
			color.a += (_effectNode.valueMap["frameAlpha"] * _dt) ;

			if ( color.a >= _effectNode.valueMap["finalAlpha"] )
			{
				color.a = _effectNode.valueMap["finalAlpha"];
			}

			if ( color.a >= 1.0f )
			{
				color.a = 1.0f;
			}
		}
		else
		{
			color.a -= (_effectNode.valueMap["frameAlpha"] * _dt) ;

			if ( color.a <= _effectNode.valueMap["finalAlpha"] )
			{
				color.a = _effectNode.valueMap["finalAlpha"];
			}

			if ( color.a < 0.0f )
			{
				color.a = 0.0f;
			}
		}
		
		//tyoCore.Log(color.a.ToString());

		colorFlag = true;

		if ( _effectNode.curDt > _effectNode.valueMap["time"] )
		{
			_effectNode.curDt = 0.0f;
			_effectNode.disableFlag = true;
		}
	}

	void EffectUpdate_MoveToX(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.valueMap["isRight"] == EffectNode.valueTRUE )
		{
			positionVec3.x += (_effectNode.valueMap["frameDistance"] * _dt) ;

			if ( positionVec3.x >= _effectNode.valueMap["finalX"] )
			{
				positionVec3.x = _effectNode.valueMap["finalX"];
			}
		}
		else
		{
			positionVec3.x -= (_effectNode.valueMap["frameDistance"] * _dt);

			if ( positionVec3.x <= _effectNode.valueMap["finalX"] )
			{
				positionVec3.x = _effectNode.valueMap["finalX"];
			}
		}
		
		positionFlag = true;

		if ( _effectNode.curDt > _effectNode.valueMap["time"] )
		{
			_effectNode.curDt = 0.0f;
			_effectNode.disableFlag = true;
		}
	}

	void EffectUpdate_MoveToY(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.valueMap["isDown"] == EffectNode.valueTRUE )
		{
			positionVec3.y += (_effectNode.valueMap["frameDistance"] * _dt);

			if ( positionVec3.y >= _effectNode.valueMap["finalY"] )
			{
				positionVec3.y = _effectNode.valueMap["finalY"];
			}
		}
		else
		{
			positionVec3.y -= (_effectNode.valueMap["frameDistance"] * _dt);

			if ( positionVec3.y <= _effectNode.valueMap["finalY"] )
			{
				positionVec3.y = _effectNode.valueMap["finalY"];
			}
		}
		
		positionFlag = true;

		if ( _effectNode.curDt > _effectNode.valueMap["time"] )
		{
			_effectNode.curDt = 0.0f;
			_effectNode.disableFlag = true;
		}
	}

	void EffectUpdate_ScaleToXY(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.valueMap["isScaleUP"] == EffectNode.valueTRUE )
		{
			scaleVec3.x += (_effectNode.valueMap["frameScale"] * _dt);
			scaleVec3.y += (_effectNode.valueMap["frameScale"] * _dt);

			if ( scaleVec3.x >= _effectNode.valueMap["finalScaleXY"] )
			{
				scaleVec3.x = _effectNode.valueMap["finalScaleXY"];
				scaleVec3.y = _effectNode.valueMap["finalScaleXY"];
			}
		}
		else
		{
			scaleVec3.x -= (_effectNode.valueMap["frameScale"] * _dt);
			scaleVec3.y -= (_effectNode.valueMap["frameScale"] * _dt);

			if ( scaleVec3.x <= _effectNode.valueMap["finalScaleXY"] )
			{
				scaleVec3.x = _effectNode.valueMap["finalScaleXY"];
				scaleVec3.y = _effectNode.valueMap["finalScaleXY"];
			}

			if ( scaleVec3.x < 0.0f )
			{
				scaleVec3.x = 0.0f;
				scaleVec3.y = 0.0f;
			}
		}
		
		scaleFlag = true;

		if ( _effectNode.curDt > _effectNode.valueMap["time"] )
		{
			_effectNode.curDt = 0.0f;
			_effectNode.disableFlag = true;
		}
	}

	void EffectUpdate_AutoRotation(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.curDt > _effectNode.valueMap["rotDt"] )
		{
			_effectNode.curDt = 0.0f;

			SetRotation(_effectNode.valueMap["rotValue"]);
		}
	}

	void EffectUpdate_Flash(EffectNode _effectNode, float _dt)
	{
		_effectNode.curDt += _dt;

		if ( _effectNode.curDt > _effectNode.valueMap["flashDt"] )
		{
			_effectNode.curDt = 0.0f;

			if ( _effectNode.valueMap["changeflag"] == EffectNode.valueTRUE )
			{
				color.a += _effectNode.valueMap["changeValue"];
			}
			else
			{
				color.a -= _effectNode.valueMap["changeValue"];
			}

			if(color.a > _effectNode.valueMap["highAlpha"])
			{
				_effectNode.valueMap["changeflag"] = EffectNode.valueFLASE;
				color.a = _effectNode.valueMap["highAlpha"];
			}
			else if(color.a < _effectNode.valueMap["lowAlpha"])
			{
				_effectNode.valueMap["changeflag"] = EffectNode.valueTRUE;
				color.a = _effectNode.valueMap["lowAlpha"];
			}

			if ( color.a > 1.0f)
			{
				color.a = 1.0f;
			}
			else if ( color.a < 0.0f)
			{
				color.a = 0.0f;
			}

			colorFlag = true;	
		}
	}

	public void UpdateEffect(float _dt)
	{
		foreach(EffectNode _effect in curEffectList)
		{
			if ( _effect.disableFlag )
			{
				continue;
			}

			if ( _effect.type == EffectType.flash )
			{
				EffectUpdate_Flash(_effect, _dt);
			} 
			else if ( _effect.type == EffectType.autorot )
			{
				EffectUpdate_AutoRotation(_effect,_dt);
			}
			else if ( _effect.type == EffectType.movetox )
			{
				EffectUpdate_MoveToX(_effect,_dt);
			}
			else if ( _effect.type == EffectType.movetoy )
			{
				EffectUpdate_MoveToY(_effect,_dt);
			}
			else if ( _effect.type == EffectType.scaletoxy )
			{
				EffectUpdate_ScaleToXY(_effect,_dt);
			}
			else if ( _effect.type == EffectType.alphatoa )
			{
				EffectUpdate_AlphaToA(_effect,_dt);
			}
		}	
	}
}
