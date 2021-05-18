using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    const string pluginName = "com.example.unity.MyPlugin";

    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Elapsed Time: " + getElapsedTime());
    }

    float elapsedTime = 0;
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 5)
        {
            elapsedTime -= 5;
            Debug.Log("Tick: " + getElapsedTime());
        }
    }

    public void ButtonClicked()
    {
        AndroidJavaClass cls_MainActivity = new AndroidJavaClass("com.example.unity.MyPlugin");

        cls_MainActivity.CallStatic("helloWorld");

        Debug.Log("clicked");

    }

    double getElapsedTime()
    {
        if (Application.platform == RuntimePlatform.Android)
            return PluginInstance.Call<double>("getElapsedTime");
        Debug.LogWarning("Wrong platform");
        return 0;
    }
}