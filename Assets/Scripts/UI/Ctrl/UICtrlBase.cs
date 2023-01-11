using UnityEngine;
using System;

/// <summary>
/// C层的基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class UICtrlBase<T> : IUICtrlBase where T : UIViewBase
{
    public T uiview;
    public UICtrlBase()
    {

    }
    //初始化
    public void Init(GameObject uiroot)
    {
        uiview = uiroot.GetComponent<T>();
        uiview.SetBackButtonPressedDelegate(AndroidBackPressed);
    }
    //打开
    public virtual void Open(object args) { }

    //销毁
    public virtual void Destroy()
    {
        UnityEngine.Object.DestroyImmediate(uiview.gameObject);
        uiview = null;
    }
    //显示
    public virtual void Show()
    {
        uiview.gameObject.SetActive(true);
    }
    //隐藏
    public virtual void Hide()
    {
        uiview.gameObject.SetActive(false);
    }
    //获取V层
    public UIViewBase View()
    {
        return uiview;
    }
    //添加UI事件，点击啊，拖拽啊
    public void AddUIEvent(GameObject obj, UIEventListener.VoidDelegate del, string uievent = "onClick")
    {
        switch (uievent)
        {
            case "onClick":
                UIEventListener.Get(obj).AddClick(del);
                break;
            case "onUp":
                UIEventListener.Get(obj).onUp += del;
                break;
            default:
                break;
        }

    }
    public void AddUIEvent(GameObject obj, UIEventListener.VoidDelgateWithEventdata del, string uievent)
    {
        switch (uievent)
        {
            case "onDraging":
                UIEventListener.Get(obj).onDraging += del;
                break;
            case "onBegin":
                UIEventListener.Get(obj).onBegin += del;
                break;
            case "onEnd":
                UIEventListener.Get(obj).onEnd += del;
                break;
            case "onDown":
                UIEventListener.Get(obj).onDown += del;
                break;
            case "onEnter":
                UIEventListener.Get(obj).onEnter += del;
                break;
            default:
                break;
        }

    }
    //删除UI事件
    public void RemoveUIEvent(GameObject obj, UIEventListener.VoidDelegate del, string uievent = "onClick")
    {
#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result
        switch (uievent)
        {
            case "onClick":

                UIEventListener.Get(obj).RemoveClick(del);
                break;
            case "onUp":
                UIEventListener.Get(obj).onUp -= del;
                break;


            default:
                break;

        }
#pragma warning restore RECS0020 // Delegate subtraction has unpredictable result
    }


    public void RemoveUIEvent(GameObject obj, UIEventListener.VoidDelgateWithEventdata del, string uievent = "onClick")
    {
#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result
        switch (uievent)
        {
            case "onBegin":
                UIEventListener.Get(obj).onBegin -= del;
                break;
            case "onDraging":
                UIEventListener.Get(obj).onDraging -= del;
                break;
            case "onEnd":
                UIEventListener.Get(obj).onEnd -= del;
                break;
            case "onDown":
                UIEventListener.Get(obj).onDown -= del;
                break;
            case "onEnter":
                UIEventListener.Get(obj).onEnter -= del;
                break;
            default:
                break;

        }
#pragma warning restore RECS0020 // Delegate subtraction has unpredictable result
    }
    //添加事件的监听
    public void AddEvent(string eventname, string cbname)
    {
        EventDispatcher.AddListener(eventname, this, cbname);
    }
    //删除事件的监听
    public void RemoveEvent(string eventname, string cbname)
    {
        EventDispatcher.RemoveListener(eventname, this, cbname);
    }

    protected virtual void AndroidBackPressed()
    {

    }
}