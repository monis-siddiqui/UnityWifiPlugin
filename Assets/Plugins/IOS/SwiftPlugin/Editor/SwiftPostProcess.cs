using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Diagnostics;

using System.IO;
using System.Linq;

public static class SwiftPostProcess
{

	[PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
	{
		if (buildTarget == BuildTarget.iOS)
		{
			var projPath = buildPath + "/Unity-Iphone.xcodeproj/project.pbxproj";
			var proj = new PBXProject();
			proj.ReadFromFile(projPath);
            string targetGuid = null ;
            string mainTargetGuid = null;
            //var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#if !UNITY_2019_3_OR_NEWER
            var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#else
          // .. targetGuid = proj.GetUnityFrameworkTargetGuid();
            mainTargetGuid = proj.GetUnityMainTargetGuid();
#endif

            // string targetGuid = proj.GetUnityMainTargetGuid();

            //         proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
            ////proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/Plugins/iOS/SwiftPlugin/Source/SwiftPlugin-Bridging-Header.h");
            //proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "SwiftPlugin-Swift.h");
            //proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks $(PROJECT_DIR)/lib/$(CONFIGURATION) $(inherited)");
            //proj.AddBuildProperty(targetGuid, "FRAMERWORK_SEARCH_PATHS",
            //	"$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
            //proj.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            //proj.AddBuildProperty(targetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
            //proj.AddBuildProperty(targetGuid, "LD_DYLIB_INSTALL_NAME",
            //	"@executable_path/../Frameworks/$(EXECUTABLE_PATH)");
            //proj.AddBuildProperty(targetGuid, "DEFINES_MODULE", "YES");
            //proj.AddBuildProperty(targetGuid, "SWIFT_VERSION", "4.0");
            //proj.AddBuildProperty(targetGuid, "COREML_CODEGEN_LANGUAGE", "Swift");

            proj.SetBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(mainTargetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/Plugins/iOS/SwiftPlugin/Source/SwiftPlugin-Bridging-Header.h");
            proj.SetBuildProperty(mainTargetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "SwiftPlugin-Swift.h");
            proj.AddBuildProperty(mainTargetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks $(PROJECT_DIR)/lib/$(CONFIGURATION) $(inherited)");
            proj.AddBuildProperty(mainTargetGuid, "FRAMERWORK_SEARCH_PATHS",
                "$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
            proj.AddBuildProperty(mainTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            proj.AddBuildProperty(mainTargetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
            proj.AddBuildProperty(mainTargetGuid, "LD_DYLIB_INSTALL_NAME",
                "@executable_path/../Frameworks/$(EXECUTABLE_PATH)");
            proj.AddBuildProperty(mainTargetGuid, "DEFINES_MODULE", "YES");
            proj.AddBuildProperty(mainTargetGuid, "SWIFT_VERSION", "4.0");
            proj.AddBuildProperty(mainTargetGuid, "COREML_CODEGEN_LANGUAGE", "Swift");
            proj.AddCapability(mainTargetGuid, PBXCapabilityType.AccessWiFiInformation);
			proj.WriteToFile(projPath);


            // Get plist
            string plistPath = buildPath + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            //Add File access Entitlements

            rootDict.SetString("NSLocationUsageDescription", "access wifi information");
            rootDict.SetString("NSLocationWhenInUseUsageDescription", "access wifi information");

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
	}

}