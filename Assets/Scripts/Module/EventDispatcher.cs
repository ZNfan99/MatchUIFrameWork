using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// 事件系统
/// </summary>
public class EventDispatcher
{
    //结构体，作为集合的值
    public struct Pair
    {
        public object obj;
        public string funcname;
        public System.Reflection.MethodInfo callback;
    }
    //库
    static Dictionary<string, List<Pair>> s_events = new Dictionary<string, List<Pair>>();
    //添加侦听
    public static void AddListener(string eventname, Action handler)
    {
        AddListener(eventname, handler.Target, handler.Method.Name);
    }
    //泛型的添加侦听的方法（1个参数的回调）
    public static void AddListener<T1>(string eventname, Action<T1> handler)
    {
        AddListener(eventname, handler.Target, handler.Method.Name);
    }
    //泛型的添加侦听的方法（2个参数的回调）
    public static void AddListener<T1, T2>(string eventname, Action<T1, T2> handler)
    {
        AddListener(eventname, handler.Target, handler.Method.Name);
    }
    //泛型的添加侦听的方法（3个参数的回调）
    public static void AddListener<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
    {
        AddListener(eventname, handler.Target, handler.Method.Name);
    }
    //泛型的添加侦听的方法（4个参数的回调）
    public static void AddListener<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
    {
        AddListener(eventname, handler.Target, handler.Method.Name);
    }
    
    public static void AddListener(string eventname, object obj, string cbname)
    {
        Pair pair = new Pair();
        pair.obj = obj;
        pair.funcname = cbname;
        pair.callback = obj.GetType().GetMethod(cbname);
        List<Pair> lst;
        if (!s_events.TryGetValue(eventname, out lst))
        {
            lst = new List<Pair>();
            s_events.Add(eventname, lst);
        }
        else
        {
            if (lst.Any(i => i.obj == obj && i.funcname == cbname))
            {
                UnityEngine.Debug.LogError("duplicate eventname:" + eventname + " funcname:" + cbname);
                return;
            }
        }
        lst.Add(pair);
    }
    //删除侦听
    public static void RemoveListener(string eventname, Action handler)
    {
        RemoveListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener<T1>(string eventname, Action<T1> handler)
    {
        RemoveListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener<T1, T2>(string eventname, Action<T1, T2> handler)
    {
        RemoveListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
    {
        RemoveListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
    {
        RemoveListener(eventname, handler.Target, handler.Method.Name);
    }
    public static void RemoveListener(string eventname, object obj, string cbname)
    {
        List<Pair> list;
        if (!s_events.TryGetValue(eventname, out list))
        {
            return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].obj == obj && list[i].funcname == cbname)
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
    //通知
    public static void Notify(string eventname, params object[] args)
    {
        List<Pair> lst;
        if (!s_events.TryGetValue(eventname, out lst))
        {
            return;
        }
        for (int i = 0; i < lst.Count; i++)
        {
            Pair pair = lst[i];
            try
            {
                pair.callback.Invoke(pair.obj, args);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Event::notify: event=" + pair.callback.DeclaringType.FullName + "::" + pair.funcname + "\n" + e.ToString());
            }
        }
    }
}
