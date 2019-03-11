using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoLogic : MonoBehaviour
{
	public tyoLogic()
	{

	}

	public virtual void Awake()
	{
		tyoCore.logic = this;
	}

	public virtual void Start()
	{

	}

	public virtual void Init()
	{

	}

	public virtual void Run()
	{

	}

	public virtual void LogicUpdate(float _dt)
	{

	}

	
}
