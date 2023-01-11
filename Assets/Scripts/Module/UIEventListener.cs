using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// UI事件的监听
/// </summary>
public class UIEventListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject obj);
    public delegate void VoidDelgateWithEventdata(PointerEventData data);
    public VoidDelegate onClick;
    public VoidDelegate onUp;
    public VoidDelgateWithEventdata onEnter;
    public VoidDelgateWithEventdata onDown;
    public VoidDelgateWithEventdata onBegin;
    public VoidDelgateWithEventdata onDraging;
    public VoidDelgateWithEventdata onEnd;
    //获取身上的监听
    public static UIEventListener Get(GameObject obj)
    {
        UIEventListener listener = obj.GetComponent<UIEventListener>();
        if (null == listener)
        {
            listener = obj.AddComponent<UIEventListener>();
        }
        return listener;
    }
    //添加点击
    public void AddClick(VoidDelegate cb)
    {
        //GUIButton button = this.gameObject.GetComponent<GUIButton>();
        //if (null != button)
        //{
        //    button.Add(this, cb);
        //}
        //else
        //{
        //    onClick += cb;
        //}
    }
    //删除点击
    public void RemoveClick(VoidDelegate cb)
    {
//        GUIButton button = this.gameObject.GetComponent<GUIButton>();
//        if (null != button)
//        {
//            button.Remove(this);
//        }
//        else
//        {
//#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result
//            onClick -= cb;
//#pragma warning restore RECS0020 // Delegate subtraction has unpredictable result
//        }
    }
    //单击
    public override void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(gameObject);
    }
    //按下
    public override void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke(eventData);
    }
    //抬起
    public override void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke(gameObject);
    }
    //进入
    public override void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(eventData);
    }
    //开始拖拽
    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBegin?.Invoke(eventData);
    }
    //拖拽中
    public override void OnDrag(PointerEventData eventData)
    {
        onDraging?.Invoke(eventData);
    }
    //结束拖拽
    public override void OnEndDrag(PointerEventData eventData)
    {
        onEnd?.Invoke(eventData);
    }
}
