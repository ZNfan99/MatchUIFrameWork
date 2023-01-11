using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C层的接口
public interface IUICtrlBase
{
    void Init(GameObject uiroot);
    void Open(object args);
    void Destroy();
    void Show();
    void Hide();
    UIViewBase View();
}
