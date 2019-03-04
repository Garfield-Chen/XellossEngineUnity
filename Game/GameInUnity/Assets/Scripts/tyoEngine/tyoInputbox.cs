using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoInputbox : tyoRenderNode 
{
	string text = "";
	RectTransform rectTransformObject = null;
	UnityEngine.UI.InputField uiInputField = null; 

	public tyoInputbox(GameObject _inputboxPerfeb)
	{
		base.renderNodeObject = GameObject.Instantiate<GameObject>(_inputboxPerfeb);
		uiInputField = renderNodeObject.GetComponent<UnityEngine.UI.InputField>();
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
			uiInputField.textComponent.color = base.GetColor();
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
		uiInputField.textComponent.alignment = _anchor;
	}

	public void Text(string _text)
	{
		uiInputField.text = _text;
	}

	public string GetText()
	{
		return uiInputField.textComponent.text;
	}

	public UnityEngine.UI.Text GetTextObject()
	{
		return uiInputField.textComponent;
	}

	public UnityEngine.UI.Text GetTextPlaceholderObject()
	{
		return (UnityEngine.UI.Text)uiInputField.placeholder;
	}
}
