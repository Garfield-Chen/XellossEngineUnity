using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tyoHelper 
{
	public static bool GetRoundCollision(Vector2 _pos,Vector2 _roundCenterPos, float _r)
	{
		if( ((_roundCenterPos.x - _pos.x) * (_roundCenterPos.x - _pos.x) + 
			(_roundCenterPos.y - _pos.y) * (_roundCenterPos.y - _pos.y)) >
			_r * _r )
		{
			return false;
		}

		return true;
	}	

	public static bool GetRoundCollision(Vector3 _pos,Vector3 _roundCenterPos, float _r)
	{
		if( ((_roundCenterPos.x - _pos.x) * (_roundCenterPos.x - _pos.x) + 
			(_roundCenterPos.y - _pos.y) * (_roundCenterPos.y - _pos.y)) >
			_r * _r )
		{
			return false;
		}

		return true;
	}	

	public static bool GetRoundCollision(Vector3 _pos,Vector2 _roundCenterPos, float _r)
	{
		if( ((_roundCenterPos.x - _pos.x) * (_roundCenterPos.x - _pos.x) + 
			(_roundCenterPos.y - _pos.y) * (_roundCenterPos.y - _pos.y)) >
			_r * _r )
		{
			return false;
		}

		return true;
	}	

	public static bool GetRoundCollision(Vector2 _pos,Vector3 _roundCenterPos, float _r)
	{
		if( ((_roundCenterPos.x - _pos.x) * (_roundCenterPos.x - _pos.x) + 
			(_roundCenterPos.y - _pos.y) * (_roundCenterPos.y - _pos.y)) >
			_r * _r )
		{
			return false;
		}

		return true;
	}	

	public static bool GetRectCollision(Vector2 _pos,Vector2 _rect_pos,int _width,int _height)
	{
		_width = _width / 2;
		_height = _height / 2;
		
		if ( _pos.x >= _rect_pos.x - _width && _pos.x <= _rect_pos.x + _width && 
			 _pos.y >= _rect_pos.y - _height && _pos.y <= _rect_pos.y + _height )
		{
			return true;
		}
			
		return false;
	}

	public static bool GetRectCollision(Vector3 _pos,Vector2 _rect_pos,int _width,int _height)
	{
		_width = _width / 2;
		_height = _height / 2;

		if ( _pos.x >= _rect_pos.x - _width && _pos.x <= _rect_pos.x + _width && 
			 _pos.y >= _rect_pos.y - _height && _pos.y <= _rect_pos.y + _height )
		{
			return true;
		}
			
		return false;
	}

	public static bool GetRectCollision(Vector3 _pos,Vector3 _rect_pos,int _width,int _height)
	{
		_width = _width / 2;
		_height = _height / 2;
		
		if ( _pos.x >= _rect_pos.x - _width && _pos.x <= _rect_pos.x + _width && 
			 _pos.y >= _rect_pos.y - _height && _pos.y <= _rect_pos.y + _height )
		{
			return true;
		}
			
		return false;
	}

	public static bool GetRectCollision(Vector2 _pos,Vector3 _rect_pos,int _width,int _height)
	{
		_width = _width / 2;
		_height = _height / 2;
		
		if ( _pos.x >= _rect_pos.x - _width && _pos.x <= _rect_pos.x + _width && 
			 _pos.y >= _rect_pos.y - _height && _pos.y <= _rect_pos.y + _height )
		{
			return true;
		}
			
		return false;
	}

	public static List<T> GetRandomList<T>(List<T> inputList)
	{
		//Copy to a array
		T[] copyArray = new T[inputList.Count];
		inputList.CopyTo(copyArray);

		//Add range
		List<T> copyList = new List<T>();
		copyList.AddRange(copyArray);

		//Set outputList and random
		List<T> outputList = new List<T>();
		
		System.Random rd = new System.Random(DateTime.Now.Millisecond);

		while (copyList.Count > 0)
		{
			//Select an index and item
			int rdIndex = rd.Next(0, copyList.Count - 1);
			T remove = copyList[rdIndex];

			//remove it from copyList and add it to output
			copyList.Remove(remove);
			outputList.Add(remove);
		}
		return outputList;
	}

}
