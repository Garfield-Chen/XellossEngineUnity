using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoScene 
{
	public class tyoSceneData
	{
		public string name = "";
	}
	public class tyoSceneNode 
	{
		public GameObject RenderNodeListObject = new GameObject(); 
		public GameObject TextCanvasObject = new GameObject();
		List<tyoRenderNode> renderNodeList = new List<tyoRenderNode>();
		public bool isDelayInitFlag = false;
		
		public tyoSceneNode(string _name)
		{
			sceneName = _name;

			RenderNodeListObject.name = string.Format("{0}[RenderNodeList]",sceneName);
			TextCanvasObject.name = string.Format("{0}[TextCanvas]",sceneName);

			RectTransform _rectTransform = TextCanvasObject.AddComponent<RectTransform>();
			TextCanvasObject.AddComponent<Canvas>();
			_rectTransform.position = new Vector3( 0.0f, 0.0f, 0.0f );
			_rectTransform.sizeDelta = new Vector2((float)Screen.width,(float)Screen.height);

			UnityEngine.UI.CanvasScaler _canvasScaler = TextCanvasObject.AddComponent<UnityEngine.UI.CanvasScaler>();
			_canvasScaler.referencePixelsPerUnit = 1.0f;

			TextCanvasObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();

			RenderNodeListObject.SetActive(false);
			TextCanvasObject.SetActive(false);

		}
		public void AddRenderNode(tyoRenderNode _node)
		{
			renderNodeList.Add(_node);

			if (isDelayInitFlag)
			{
				_node.DelayInit(this);
			}
		}

		public bool RemoveRenderNode(tyoRenderNode _node)
		{
			return renderNodeList.Remove(_node);
		}

		public void DestroyAllRenderNode()
		{
			foreach ( tyoRenderNode _node in renderNodeList )
			{
				if ( _node.renderNodeObject != null )
				{
					GameObject.Destroy(_node.renderNodeObject);
					_node.renderNodeObject = null;
				}
			}

			renderNodeList.Clear();
		}
		
		public virtual void SceneDelayInit()
		{
			if ( isDelayInitFlag )
			{
				return;
			}

			isDelayInitFlag = true;
			
			foreach (tyoRenderNode _node in renderNodeList)
			{
				_node.DelayInit(this);
			}
		}

		public virtual void InitScene()
		{

		}

		public virtual void FreeScene()
		{
			Object.Destroy(RenderNodeListObject);
			Object.Destroy(TextCanvasObject);
		}

		public virtual void InvokeSceneData(tyoSceneData _data)
		{

		}

		public virtual void EnterScene()
		{
			RenderNodeListObject.SetActive(true);
			TextCanvasObject.SetActive(true);
		}

		public virtual void LeaveScene()
		{
			RenderNodeListObject.SetActive(false);
			TextCanvasObject.SetActive(false);
		}

		public virtual void SceneUpdate(float _dt)
		{
			foreach (tyoRenderNode _node in renderNodeList)
			{
				_node.Update(_dt);
			}
		}

		public string GetSceneName()
		{
			return sceneName;
		}

		string sceneName = "new scene";
	}
	public tyoScene()
	{

	}

	Dictionary<string,tyoSceneNode> sceneList = new Dictionary<string,tyoSceneNode>();
	tyoSceneNode curScene = null;
	tyoSceneNode nextScene = null;
	bool changeSceneFlag = false;
	public bool AddScene(tyoSceneNode _scene)
	{
		if ( !sceneList.ContainsKey(_scene.GetSceneName()) )
		{
			sceneList.Add(_scene.GetSceneName(),_scene);
			_scene.InitScene();
			return true;
		}

		return false;
	}

	public bool DeleteScene(string _name)
	{
		if ( sceneList.ContainsKey(_name) )
		{
			sceneList[_name].FreeScene();
			sceneList[_name] = null;
			sceneList.Remove(_name);

			return true;
		}

		return false;
	}

	public tyoSceneNode GetScene(string _name)
	{
		if ( sceneList.ContainsKey(_name) )
		{
			return sceneList[_name];
		}

		return null;
	}

	public tyoSceneNode GetCurScene()
	{
		return curScene;
	}

	public bool ChangeScene(string _name,tyoSceneData _data = null)
	{
		if ( curScene != null )
		{
			if ( curScene.GetSceneName() == _name )
			{
				tyoCore.Log("Change the same scene.");			
				return false;
			}
		}

		nextScene = GetScene(_name);
		if ( nextScene != null )
		{
			nextScene.SceneDelayInit();

			if ( _data != null )
			{
				nextScene.InvokeSceneData(_data);
			}

			changeSceneFlag = true;

			return true;
		}

		return false;
	}

	public void SceneUpdate(float _dt)
	{	
		if ( changeSceneFlag && nextScene.isDelayInitFlag )
		{
			changeSceneFlag = false;

			if ( curScene != null )
			{
				curScene.LeaveScene();
				tyoCore.input.Clear();
				nextScene.EnterScene();
			}
			else
			{
				nextScene.EnterScene();
			}

			curScene = nextScene;
		}

		if ( curScene != null )
		{
			curScene.SceneUpdate( _dt );
		}
	}
}
