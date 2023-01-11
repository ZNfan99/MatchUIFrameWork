using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// V层的基类，是直接挂载在场景对象预制体上的，实现自己的UI逻辑
/// </summary>
public class UIViewBase : MonoBehaviour
{
    public int zOrder = 0;
    public ENGUILayer uiLayer = ENGUILayer.Main;
    public UIViewBase()
    {

    }

    public delegate void AndroidBackButtonPressed();
    private AndroidBackButtonPressed _backButtonPressedDel;

    public void SetBackButtonPressedDelegate(AndroidBackButtonPressed del)
    {
        _backButtonPressedDel = del;
    }

    private void Awake()
    {
        transform.SetSiblingIndex(zOrder);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _backButtonPressedDel?.Invoke();
        }
    }
}
