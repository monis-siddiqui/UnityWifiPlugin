using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WifiInfo : MonoBehaviour
{
    const string pluginName = "network.blanx.com.wifiplugin.WifiInformation";

    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluinInstance;

    public static AndroidJavaClass PluginClass {
        get {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance {
        get {
            if (_pluinInstance == null) {
                _pluinInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluinInstance;
        }
    }


    #region Declare external C interface

#if UNITY_IOS && !UNITY_EDITOR
        
    [DllImport("__Internal")]
    private static extern string _getWifiInfo();
    
#endif

    #endregion

    #region Wrapped methods and properties

    public static WifiInfoObject getWifiInfo()
    {
        WifiInfoObject wifiInfoObj = new WifiInfoObject();
        string[] token = null;
        if (Application.platform == RuntimePlatform.Android) {
            var plugin = new AndroidJavaClass(pluginName);
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            //string success = plugin.CallStatic<string>("WritePhoneContact", context);
            //object[] par = new object[2];
            //par[0] = context ;
            //par[1] = activity;
            string wifiInfoString = PluginInstance.Call<string>("_getWifiInfo", context);
            token = wifiInfoString.Split(';');
            //WifiInfoObject wifiInfoObj = new WifiInfoObject();
            wifiInfoObj.ssid = token[0];
            wifiInfoObj.bssid = token[1];
            wifiInfoObj.signalStrength = token[2];
            wifiInfoObj.isSecure = token[3];
            wifiInfoObj.didAutoJoin = token[4];
            return wifiInfoObj;
        }

#if UNITY_IOS && !UNITY_EDITOR
        string wifiInfo = _getWifiInfo();
        //WifiInfoObject wifiInfoObj = new WifiInfoObject();
        token = wifiInfo.Split(';');
        wifiInfoObj.ssid = token[0];
        wifiInfoObj.bssid = token[1];
        wifiInfoObj.signalStrength = token[2];
        wifiInfoObj.isSecure = token[3];
        wifiInfoObj.didAutoJoin = token[4];
        return wifiInfoObj;
#else
        WifiInfoObject wifiInfo = new WifiInfoObject();
        wifiInfo.ssid =  wifiInfo.bssid = wifiInfo.didAutoJoin = wifiInfo.isSecure = wifiInfo.signalStrength = "N/A";
		return wifiInfo;
#endif
    }


    #endregion

    #region Singleton implementation

    private static WifiInfo _instance;

    public static WifiInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("SwiftUnity");
                _instance = obj.AddComponent<WifiInfo>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion
}

public class WifiInfoObject
{
    public string ssid { get; set; }
    public string bssid { get; set; }
    public string signalStrength { get; set; }
    public string didAutoJoin { get; set; }
    public string isSecure { get; set; }
    public string describeContents;
    public string frequency;
    public string hiddenSSID;
    public string iPAddress;
    public string linSpeed;
    public string macAddress;
    public string networkID;
}