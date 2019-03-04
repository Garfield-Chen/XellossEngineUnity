using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoStructure 
{
	public tyoStructure()
	{

	}

	public class tyoPointInt
	{
		int x = 0;
		int y = 0;

		public tyoPointInt(int _x = 0,int _y = 0)
		{
			x = _x;
			y = _y;
		}

		public int X
		{
			get
			{
				return x;
			}
			
			set
			{
				x = value;
			}
		}  

		public int Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
			}
		}
	}

	public class tyoPointFloat
	{
		float x = 0.0f;
		float y = 0.0f;

		public tyoPointFloat(float _x = 0.0f, float _y = 0.0f)
		{
			x = _x;
			y = _y;
		}

		public float X
		{
			get
			{
				return x;
			}
			
			set
			{
				x = value;
			}
		}  

		public float Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
			}
		}
	}

}
