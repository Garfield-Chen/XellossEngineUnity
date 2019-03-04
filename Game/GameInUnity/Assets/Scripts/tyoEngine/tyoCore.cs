using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tyoCore : MonoBehaviour
{
	public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
	{
		public delegate void VoidDelegate (GameObject go);
		public VoidDelegate onClick;
		public VoidDelegate onDown;
		public VoidDelegate onEnter;
		public VoidDelegate onExit;
		public VoidDelegate onUp;
		public VoidDelegate onSelect;
		public VoidDelegate onUpdateSelect;
	
		static public EventTriggerListener Get (GameObject go)
		{
			EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
			if (listener == null) listener = go.AddComponent<EventTriggerListener>();
			return listener;
		}
		public override void OnPointerClick(PointerEventData eventData)
		{
			if(onClick != null) onClick(gameObject);
		}
		public override void OnPointerDown (PointerEventData eventData){
			if(onDown != null) onDown(gameObject);
		}
		public override void OnPointerEnter (PointerEventData eventData){
			if(onEnter != null) onEnter(gameObject);
		}
		public override void OnPointerExit (PointerEventData eventData){
			if(onExit != null) onExit(gameObject);
		}
		public override void OnPointerUp (PointerEventData eventData){
			if(onUp != null) onUp(gameObject);
		}
		public override void OnSelect (BaseEventData eventData){
			if(onSelect != null) onSelect(gameObject);
		}
		public override void OnUpdateSelected (BaseEventData eventData){
			if(onUpdateSelect != null) onUpdateSelect(gameObject);
		}
	}
	public static tyoInput input = new tyoInput();
	public static tyoResources resources = new tyoResources();
	public static tyoScene scene = new tyoScene();
	public static tyoLogic logic = null;
	public static tyoSocketClient socket = new tyoSocketClient();
	public bool delayInitFlag = false;
	public bool TouchMouseIsSame = false;
	public bool fullScreen = true;
	public bool runLogicFlag = false;
	void Awake()
	{
		Camera.main.orthographicSize = Screen.height / 2;
		Camera.main.aspect =  2208.0f /  1242.0f;

		input.MouseTouchSameFlag(TouchMouseIsSame);

		Screen.fullScreen = fullScreen;
	}
	// Use this for initialization
	void Start ()
	{
		
	}

	public static void Log(string _log)
	{
		print(_log);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( !delayInitFlag )
		{
			delayInitFlag = true;
			resources.DelayInitResources();
		}

		if ( !resources.IsResourceInitDone() )
		{
			return;
		}

		if( !runLogicFlag )
		{
			if ( logic != null )
			{
				logic.Init();
				logic.Run();
				runLogicFlag = true;
			}

			return;
		}

		float _dt = 1000 * Time.deltaTime;
		//socket.Update();
		input.InputUpdate(_dt);
		scene.SceneUpdate(_dt);
		logic.LogicUpdate(_dt);
	}
}
