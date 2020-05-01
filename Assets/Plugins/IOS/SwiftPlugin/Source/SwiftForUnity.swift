//
//  SwiftForUnity.swift
//  SwiftPlugin
//
//  Created by Monis on 17/10/2019.
//  Copyright © 2019 Monis. All rights reserved.
//

import Foundation
import UIKit
import SystemConfiguration.CaptiveNetwork
import CoreLocation
//import NetworkExtension

@objc public class SwiftForUnity: UIViewController {
    
    let locationManager:CLLocationManager = CLLocationManager()
    
      @objc static let shared = SwiftForUnity()
      @objc func getWifiInfo() -> String{
        locationManager.requestWhenInUseAuthorization()
        locationManager.startUpdatingLocation()
            //return getSSID()
       // btnGetLocation()
        //requestAlwaysAuthorization();
        let ssid = getWiFiSsid();
        locationManager.stopUpdatingLocation()
        let randomDouble = Double.random(in: 0...1)
            return "\(ssid);HomeNetwork;\(randomDouble);WPA-2;YES"
      }
    
    func getWiFiSsid() -> String? {
        var ssid: String?
        if let interfaces = CNCopySupportedInterfaces() as NSArray? {
            for interface in interfaces {
                if let interfaceInfo = CNCopyCurrentNetworkInfo(interface as! CFString) as NSDictionary? {
                    ssid = interfaceInfo[kCNNetworkInfoKeySSID as String] as? String
                    break
                }
            }
        }
        return ssid
    }
    
    /*
    private func getSSID() -> String {
        let networkInterfaces = NEHotspotHelper.supportedNetworkInterfaces()
        let wifi = NEHotspotNetwork()
        
        print(wifi)
        print(networkInterfaces)
        
     /*   let st = "SSID：\(wifi.SSID)， BSSID：\(wifi.BSSID)， SignalStrength：\(wifi.signalStrength)， IsSecure：\(wifi.secure)， IsAutoJoined：\(wifi.autoJoined)\n" */
        let st = "\(wifi.SSID);\(wifi.BSSID);\(wifi.signalStrength);\(wifi.secure);\(wifi.autoJoined)"
        return st
    }*/
}
