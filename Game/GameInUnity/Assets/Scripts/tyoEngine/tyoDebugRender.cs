﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoDebugRender : MonoBehaviour
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	Material lineMaterial;
	void CreateLineMaterial()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}

	void DrawRect( Rect rect, Color color)
	{
		Vector3[] line =new Vector3[5];
		line[0] = ttScr(new Vector3(rect.x,rect.y,-100));
		line[1] = ttScr(new Vector3(rect.x+rect.width, rect.y, -100));
		line[2] = ttScr(new Vector3(rect.x + rect.width, rect.y + rect.height, -100));
		line[3] = ttScr(new Vector3(rect.x, rect.y + rect.height, -100));
		line[4] = ttScr(new Vector3(rect.x, rect.y, -100));
		if (line != null && line.Length > 0)
		{
			DrawLineHelper(line, color);
		}
	}   

	Vector3 ttScr(Vector3 _or)
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(
			 _or.x, 
				Screen.height - _or.y, 
				_or.z));
	}
	

	void DrawLineHelper(Vector3[] line, Color color)
	{
		for (int i = 0; i < line.Length - 1; i++)
		{
			GL.Vertex3(line[i].x, line[i].y, line[i].z);
			GL.Vertex3(line[i+1].x, line[i+1].y, line[i+1].z);
		}
	}
	public void OnRenderObject()
    {
		CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
		GL.Color(Color.red);

		float _size = 170.0f;
		
        DrawRect(new Rect(50.0f /* + 100.0f * 0*/,Screen.height - _size * 2 - 50.0f, _size, _size),Color.red);
		DrawRect(new Rect(50.0f + _size * 2,Screen.height - _size * 2 - 50.0f, _size, _size),Color.red);
		DrawRect(new Rect(50.0f + _size * 1,Screen.height - _size * 3 - 50.0f, _size, _size),Color.red);
		DrawRect(new Rect(50.0f + _size * 1,Screen.height - _size * 1 - 50.0f, _size, _size),Color.red);
		//Vector3 _t = ttScr(new Vector3(50.0f + 170.0f + 170.0f /2 ,Screen.height - 170.0f * 2 - 50.0f + 170.0f / 2,100.0f));
		//GL.Vertex3(_t.x,_t.y,_t.z);
		//GL.Vertex3(_t.x + 1,_t.y,_t.z);
        GL.End();
        GL.PopMatrix();
	}

	void OnPostRender()
    {
        // Set your materials
        GL.PushMatrix();
        // yourMaterial.SetPass( );
        // Draw your stuff
        GL.PopMatrix();

    }


}
