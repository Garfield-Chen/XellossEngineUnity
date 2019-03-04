using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoLine : tyoRenderNode 
{
	LineRenderer lineRenderer = null;
	float lineSize = 1.0f;
	List<Vector3> pointList = new List<Vector3>(16);
	public tyoLine()
	{
		base.renderNodeObject = new GameObject();
		lineRenderer = base.renderNodeObject.AddComponent<LineRenderer>();
	}

	public void SetLineWidth(float _startWidth, float _endWidth)
	{
		lineRenderer.startWidth = _startWidth;
		lineRenderer.endWidth = _endWidth;
	}

	public int AddPoint( float _x, float _y, float _z = 5.0f )
	{
		int _idx = pointList.Count;

		pointList.Add(new Vector3(
				_x - (Screen.width / 2) + lineRenderer.startWidth, 
				(Screen.height / 2) - _y - lineRenderer.startWidth, 
				Camera.main.ScreenToWorldPoint(new Vector3(0.0f,0.0f,_z)).z));

		lineRenderer.SetPosition(_idx,pointList[_idx]);

		lineRenderer.material = tyoCore.resources.GetMaterialByName("Sprites");

		return _idx;
	}

	public override void Update(float _dt)
	{
		base.UpdateEffect(_dt);

		if ( base.colorFlag )
		{
			lineRenderer.startColor = base.GetColor();
			lineRenderer.endColor = base.GetColor();
		}

		base.Update(_dt);
	}
}
