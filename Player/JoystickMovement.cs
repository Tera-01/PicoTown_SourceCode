using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    public static JoystickMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoystickMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("JoyStickMovement");
                    instance = instanceContainer.AddComponent<JoystickMovement>();
                }
            }
            return instance;
        }
    }

    private static JoystickMovement instance;

    public GameObject smallStick;
    public GameObject bigStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickRadius;
    
    private bool _onDrag;

    void Start()
    {
        stickRadius = bigStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        _onDrag = false;
        OnMoveStart += AudioManager.instance.SetWalkSound;
        OnIdle += AudioManager.instance.EndWalkSound;
    }

    public void PointDown()
    {
        bigStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }

        if (_onDrag == false)
        {
            _onDrag = true;
            OnMoveStart?.Invoke((int)Anim.WALK);
        }
        
        OnMove?.Invoke((int)Anim.WALK);
    }

    public event Action<int> OnMoveStart;
    public event Action<int> OnMove;
    public event Action OnIdle;

    public void PointUp()
    {
        _onDrag = false;
        OnIdle?.Invoke();
        joyVec = Vector3.zero;
    }

}   
