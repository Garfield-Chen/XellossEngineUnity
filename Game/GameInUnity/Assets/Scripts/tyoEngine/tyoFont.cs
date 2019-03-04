using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoFont : tyoRenderNode 
{
	string text = "";
	RectTransform rectTransformObject = null;
	UnityEngine.UI.Text uiText = null; 
	public tyoFont(Font _font, int _size = 12, FontStyle _style = FontStyle.Normal)
	{
		base.renderNodeObject = new GameObject();
		
		rectTransformObject = base.renderNodeObject.AddComponent<RectTransform>();
		uiText = base.renderNodeObject.AddComponent<UnityEngine.UI.Text>();

		uiText.font = _font;
		uiText.fontSize = _size;
		uiText.fontStyle = _style;
		uiText.material = _font.material;
	}

	public override void DelayInit(tyoScene.tyoSceneNode _scene)
	{
		//rectTransformObject.transform.parent = _scene.TextCanvasObject.transform;
		base.renderNodeObject.transform.SetParent(_scene.TextCanvasObject.transform);
		//base.renderNodeObject.transform.parent = _scene.TextCanvasObject.transform;
	}

	public override void Update(float _dt)
	{
		base.UpdateEffect(_dt);

		if ( base.colorFlag )
		{
			uiText.color = base.GetColor();
		}

		Vector3 _scale = base.GetScale();
		Vector3 _pos = base.GetPosition();

		if ( base.scaleFlag )
		{
			rectTransformObject.localScale = _scale;
		}
		
		if ( base.positionFlag )
		{
			rectTransformObject.position = Camera.main.ScreenToWorldPoint(new Vector3(
				(rectTransformObject.sizeDelta.x * _scale.x) / 2 + _pos.x, 
				Screen.height - (rectTransformObject.sizeDelta.y * _scale.y) / 2 - _pos.y, 
				_pos.z));
		}

		base.Update(_dt);
	}

	public void SetRectSize(float _w, float _h)
	{
		rectTransformObject.sizeDelta = new Vector2(_w,_h);
	}

	public void SetTextAnchor(TextAnchor _anchor)
	{
		uiText.alignment = _anchor;
	}

	public void Text(string _text)
	{
		uiText.text = _text;
	}

	public string GetText()
	{
		return uiText.text;
	}
}
