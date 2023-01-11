using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 外观 ，提供了一个CanvasRoot目前
/// </summary>
public class AppFacade 
{
    #region Singleton
    private static AppFacade s_instance;
    public static AppFacade Instance
    {
        get
        {
            if (null == AppFacade.s_instance)
            {
                AppFacade.s_instance = new AppFacade();
            }
            return AppFacade.s_instance;
        }
    }

    #endregion
    public Transform CanvasRoot { get; set; }
    public Transform CanvasBack { get; set; }
}
