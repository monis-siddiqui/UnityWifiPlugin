//
//  SwiftForUnityBridge.mm
//  SwiftPlugin
//
//  Created by Monis on 17/10/2019.
//  Copyright © 2019 Monis. All rights reserved.
//

#include "SwiftPlugin-Swift.h"
#pragma mark - C interface
extern "C" {
     char* _getWifiInfo() {
          NSString *returnString = [[SwiftForUnity shared]       getWifiInfo];
          char* cStringCopy(const char* string);
          return cStringCopy([returnString UTF8String]);
     }
}
char* cStringCopy(const char* string){
     if (string == NULL){
          return NULL;
     }
     char* res = (char*)malloc(strlen(string)+1);
     strcpy(res, string);
     return res;
}
