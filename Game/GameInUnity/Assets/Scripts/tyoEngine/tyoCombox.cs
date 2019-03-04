using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoCombox : tyoRenderNode 
{
	string text = "";
	RectTransform rectTransformObject = null;
	UnityEngine.UI.Dropdown uiDropdown = null; 

	public tyoCombox(GameObject _comboxPerfeb)
	{
		renderNodeObject = GameObject.Instantiate<GameObject>(_comboxPerfeb);
		uiDropdown = renderNodeObject.GetComponent<UnityEngine.UI.Dropdown>();
		rectTransformObject = renderNodeObject.GetComponent<RectTransform>();
	}

	public override void DelayInit(tyoScene.tyoSceneNode _scene)
	{
		base.renderNodeObject.transform.SetParent(_scene.TextCanvasObject.transform);
	}

	public override void Update(float _dt)
	{
		base.UpdateEffect(_dt);

		if ( base.colorFlag )
		{
			uiDropdown.captionText.color = base.GetColor();
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
		uiDropdown.captionText.alignment = _anchor;
	}

	public void Text(string _text)
	{
		uiDropdown.captionText.text = _text;
	}

	public string GetText()
	{
		return uiDropdown.captionText.text;
	}

	public List<UnityEngine.UI.Dropdown.OptionData> GetOptionDataList()
	{
		return uiDropdown.options;
	}
	
	public UnityEngine.UI.Dropdown GetDropdownObject()
	{
		return uiDropdown;
	}
}
