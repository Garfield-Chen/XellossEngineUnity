using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoInput
{
    Vector2 lastTouchPos;
    Vector2 curTouchPos;
    Vector2 lastMousePos;
    Vector2 curMousePos;
    bool onceTouchFlag = false;
    bool onceMouseDownFlag = false;
    bool isTouch;
    bool isMouseDown;
    bool mouse_touch_same = false;
    float touchStartTime = 0.0f;
    float mouseDownStartTime = 0.0f;
    float lastTouchSPTime = 0.0f;
    float lastMouseDownSPTime = 0.0f;
    Vector2 mouseDownStartPoint;
    Vector2 touchStartPoint;

    float touchWaitDt = 0.0f;
    public tyoInput()
    {
        
    }
    public void Clear()
    {
        onceTouchFlag = false;
        onceMouseDownFlag = false;
        isTouch = false;
        isMouseDown = false;
        touchStartTime = 0.0f;
        mouseDownStartTime = 0.0f;
        lastTouchSPTime = 0.0f;
        lastMouseDownSPTime = 0.0f;
        touchWaitDt = 500.0f;
    }

    public void MouseTouchSameFlag(bool _isSame)
    {
        mouse_touch_same = _isSame;
    }

    public Vector2 GetLastTouchPos()
    {
        return lastTouchPos;
    }

    public Vector2 GetLastMousePos()
    {
        return lastMousePos;
    }

    public Vector2 GetCurTouchPos()
    {
        return curTouchPos;
    }

    public Vector2 GetCurMousePos()
    {
        return curMousePos;
    }

    public bool IsTouch()
    {
        return isTouch;
    }

    public bool IsMouseDown()
    {
        return isMouseDown;
    }

    public void InputUpdate(float _dt)
    {
        if ( touchWaitDt > 0.0f)
        {
            touchWaitDt -= _dt;
            return;
        }

        lastTouchPos = curTouchPos;
        lastMousePos = curMousePos;

        if ( Input.touchCount > 0 )
        {
            curTouchPos = Input.GetTouch(0).position;
            curTouchPos.y = Screen.height - curTouchPos.y;
            isTouch = true;
        
            if (!onceTouchFlag)
            {
                touchStartTime = Time.time;
                onceTouchFlag = true;
                touchStartPoint = curTouchPos;
            }
        }
        else
        {
            isTouch = false;

            if (onceTouchFlag)
            {
                lastTouchSPTime = Time.time - touchStartTime;
                onceTouchFlag = false; 
            }

            touchStartPoint = curTouchPos;
        }
    
        if ( Input.GetMouseButton(0) )
        {
            curMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y ); 
            curMousePos.y = Screen.height - curMousePos.y;
            isMouseDown = true;   

            if (!onceMouseDownFlag)
            {
                mouseDownStartTime = Time.time;
                onceMouseDownFlag = true;
                mouseDownStartPoint = curMousePos;
            }
        }
        else
        {
            isMouseDown = false;

            if (onceMouseDownFlag)
            {
                lastMouseDownSPTime = Time.time - mouseDownStartTime;
                onceMouseDownFlag = false; 
            }

            mouseDownStartPoint = curMousePos;
        }

        if ( mouse_touch_same )
        {
            curTouchPos = curMousePos;
            isTouch = isMouseDown;
            lastTouchSPTime = lastMouseDownSPTime;
            touchStartPoint = mouseDownStartPoint; 
        }

         
        /*tyoCore.Log(string.Format("Cur:{0} Last:{1} Speed:{2} Distance:{3} {4}",
        curTouchPos,
        lastTouchPos,
        lastTouchSPTime,
        GetTouchMoveDistanceX(),
        isMouseDown.ToString()));*/
        
    }

    public float GetLastTouchSPTime()
    {
        return lastTouchSPTime;
    }

    public float GetLastMouseDownSPTime()
    {
        return lastMouseDownSPTime;
    }

    public float GetMouseMoveDistanceX()
    {
        return curMousePos.x - mouseDownStartPoint.x;
    }

    public float GetMouseMoveDistanceY()
    {
        return curMousePos.y - mouseDownStartPoint.y;
    }

    public float GetTouchMoveDistanceX()
    {
        return curTouchPos.x - touchStartPoint.x;
    }

    public float GetTouchMoveDistanceY()
    {
        return curTouchPos.y - touchStartPoint.y;
    }

    public float GetMouseMoveDistance()
    {
        return Vector2.Distance(curMousePos,mouseDownStartPoint);
    }

    public float GetTouchMoveDistance()
    {
        return Vector2.Distance(curTouchPos,touchStartPoint);
    }
}