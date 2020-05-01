using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WifiInfoHandler : MonoBehaviour
{
    public Text ssid;
    public Text signalStrength;

    string _ssid;
    string _bssid;
    string _signalStrength;
    string _isSecure;
    string _didAutoJoin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getWifiInfo() {
        WifiInfoObject wifiInfoObj = WifiInfo.getWifiInfo();
        //string[] token = wifiInfo.Split(';');
        
        _ssid = wifiInfoObj.ssid;
        _bssid = wifiInfoObj.bssid;
        _signalStrength = wifiInfoObj.signalStrength;
        _isSecure = wifiInfoObj.isSecure;
        _didAutoJoin = wifiInfoObj.didAutoJoin;
        ssid.text = _ssid;
        signalStrength.text = _signalStrength;
    }
}
