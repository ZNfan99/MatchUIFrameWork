using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
public enum ENGUILayer
{
    Main,
    Back,
}
/// <summary>
/// UI管理
/// </summary>
public class GUIManager
{
    #region Singleton
    private static GUIManager s_instance;
    public static GUIManager Instance
    {
        get
        {
            if (null == GUIManager.s_instance)
            {
                GUIManager.s_instance = new GUIManager();
            }
            return GUIManager.s_instance;
        }
    }
    #endregion
    GUIManager()
    {
        m_uiMap = new Dictionary<string, IUICtrlBase>();
    }
    //库
    private Dictionary<string, IUICtrlBase> m_uiMap;
    //获取UI
    public IUICtrlBase GetUI<T>() where T : IUICtrlBase
    {
        var typename = typeof(T).Name;
        IUICtrlBase tmpctrl;
        if (m_uiMap.TryGetValue(typename, out tmpctrl))
        {
            return tmpctrl;
        }
        return null;
    }
    //打开UI
    public IUICtrlBase OpenUI<T>(object args = null) where T : IUICtrlBase, new()
    {
        var typename = typeof(T).Name;
        IUICtrlBase tmpctrl;
        if (m_uiMap.TryGetValue(typename, out tmpctrl))
        {
            tmpctrl.Show();
            return tmpctrl;
        }
        GameObject obj = Object.Instantiate(ResourceManager.Instance.LoadUI(typename)) as GameObject;
        var ctrl = new T();
        ctrl.Init(obj);
        if (ctrl.View().uiLayer == ENGUILayer.Main)
        {
            obj.transform.SetParent(AppFacade.Instance.CanvasRoot);
        }
        else if (ctrl.View().uiLayer == ENGUILayer.Back)
        {
            obj.transform.SetParent(AppFacade.Instance.CanvasBack);
        }
        RectTransform rect = obj.transform as RectTransform;
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        ctrl.Open(args);
        m_uiMap.Add(typename, ctrl);
        return ctrl;
    }
    //打开孩子UI
    public T OpenChildUI<T>(Transform parent, object args = null) where T : IUICtrlBase, new()
    {
        var typename = typeof(T).Name;
        GameObject obj = Object.Instantiate(ResourceManager.Instance.LoadUI(typename)) as GameObject;
        var ctrl = new T();
        ctrl.Init(obj);
        obj.transform.SetParent(parent, false);
        RectTransform rect = obj.transform as RectTransform;
        //rect.offsetMin = Vector2.zero;
        //rect.offsetMax = Vector2.zero;
        ctrl.Open(args);
        return ctrl;
    }
    //关闭UI
    public void CloseUI<T>() where T : IUICtrlBase
    {
        var typename = typeof(T).Name;
        IUICtrlBase tmpctrl;
        if (m_uiMap.TryGetValue(typename, out tmpctrl))
        {
            m_uiMap.Remove(typename);
            tmpctrl.Destroy();
        }
    }
}
