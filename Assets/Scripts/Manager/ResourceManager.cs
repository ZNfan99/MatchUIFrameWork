using UnityEngine;

/// <summary>
/// 资源加载管理
/// </summary>
public class ResourceManager
{
    #region Singleton
    private static ResourceManager s_instance;
    public static ResourceManager Instance
    {
        get
        {
            if (null == ResourceManager.s_instance)
            {
                ResourceManager.s_instance = new ResourceManager();
            }
            return ResourceManager.s_instance;
        }
    }
    #endregion
    ResourceManager()
    {

    }
    static string mBasePath { get { return ""; } }
    static System.Text.StringBuilder mStringBuilder = new System.Text.StringBuilder();

    //获取资源的地址
    static public string GetConfigPath(string filename)
    {
        mStringBuilder.Length = 0;
        mStringBuilder.Append(mBasePath);
        mStringBuilder.Append("Config/");
        mStringBuilder.Append(filename);
        mStringBuilder.Append(".bytes");
        return mStringBuilder.ToString();
    }
    //获取UI的地址
    static public string GetUIPath(string filename)
    {
        mStringBuilder.Length = 0;
        mStringBuilder.Append(mBasePath);
        mStringBuilder.Append("Prefabs/UI/");
        mStringBuilder.Append(filename);
        return mStringBuilder.ToString();
    }
    //加载UI
    public GameObject LoadUI(string name)
    {
        return Load<GameObject>(GetUIPath(name));
    }
    //加载资源
    public TextAsset LoadConfig(string filename)
    {
        mStringBuilder.Length = 0;
        mStringBuilder.Append(mBasePath);
        mStringBuilder.Append("Config/");
        mStringBuilder.Append(filename);
        return Load<TextAsset>(mStringBuilder.ToString());
    }
    //泛型加载
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load(path) as T;
    }
    //实例化
    public GameObject ClonePrefab(string path)
    {
        return Object.Instantiate(Load<GameObject>(path));
    }
    //设置位置和朝向的实例化
    public GameObject ClonePrefab(string path, Vector3 pos, Quaternion rotation)
    {
        return Object.Instantiate(Load<GameObject>(path), pos, rotation);
    }
}
