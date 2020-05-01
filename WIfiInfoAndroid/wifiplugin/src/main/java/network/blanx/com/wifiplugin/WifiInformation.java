package network.blanx.com.wifiplugin;

import android.content.Context;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;

import java.util.List;

public class WifiInformation {
    private static final WifiInformation ourInstance = new WifiInformation();
    private static final int LOCATION = 1;

    public static WifiInformation getInstance() {
        return ourInstance;
    }


    private WifiInformation() {
    }

    public String _getWifiInfo(Context context) {

            //
            WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);

            // Level of a Scan Result
            List<ScanResult> wifiList = wifiManager.getScanResults();
            for (ScanResult scanResult : wifiList) {
                int level = WifiManager.calculateSignalLevel(scanResult.level, 5);
                System.out.println("Level is " + level + " out of 5");
            }

            // Level of current connection
            int rssi = wifiManager.getConnectionInfo().getRssi();
            int level = WifiManager.calculateSignalLevel(rssi, 5);
            //WifiManager.getConn
            WifiInfo info = wifiManager.getConnectionInfo();
            String ssid = info.getSSID();
            String bssid = info.getBSSID();
            int describeContents = info.describeContents();
            int frequency = info.getFrequency();
            boolean hiddenSSID = info.getHiddenSSID();
            int ipAddress = info.getIpAddress();
            int linkSpeed = info.getLinkSpeed();
            String macAddress = info.getMacAddress();
            int networkId = info.getNetworkId();
            System.out.println("Level is " + level + " out of 5");
            return ssid + ";" + bssid + ";" + rssi + ";" + level + ";" + describeContents + ";" + frequency + ";" + hiddenSSID + ";" +
                    ipAddress + ";" + linkSpeed + ";" + macAddress + ";" + networkId;

    }
    //    public int GetWifiDBM() {
//        int dbm = 0;
//
//        WifiManager wifiManager = (WifiManager) getSystemService(getApplicationContext().WIFI_SERVICE);
//        if (wifiManager == null) {
//            return -1;
//        }
//
//        if (wifiManager.isWifiEnabled()) {
//            WifiInfo wifiInfo = wifiManager.getConnectionInfo();
//            if (wifiInfo != null) {
//                dbm = wifiInfo.getRssi();
//            }
//        }
//
//        return dbm;
//    }
}



