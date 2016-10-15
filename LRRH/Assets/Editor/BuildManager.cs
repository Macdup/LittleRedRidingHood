using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
/*
The MIT License (MIT)

Copyright (c) 2014 Just a Pixel LTD - http://www.justapixel.co.uk

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
namespace JustAPixel.Pipeline.Builds{
	public class BuildManager : MonoBehaviour {	
		#region Options
		static string versionTextFileNameAndPath = "version.txt";
		/// <summary>
		/// The location of the final build.
		/// </summary>
		static string LocationName = "C:\\Users\\Marc-antoine\\Google Drive\\Little Red Riding Hood";
		/// <summary>
		/// The name of the application.
		/// </summary>
		static string ApplicationName = "LittleRedRidingHood";
		#endregion
		[MenuItem("Build/Build All")]
		static void BuildAll(){

            if (!File.Exists(LocationName))
                LocationName = "C:\\Users\\mika2\\Google Drive\\Little Red Riding Hood";

            //BuildMac32(false);
            //BuildLinux32(false);
            //BuildPC32(false);
            //Add whatver you want extra built into this (Like 64-bit Mac, etc)
            BuildAndroid(false);
			BuildPC32 (false);
			BuildPC64 (false);
		}

        #region Build Types
        static void Build(string sFolderName, bool bIncrementSubMinorVersion, BuildTarget tTarget, BuildOptions oOptions){
			
			string sDate = System.DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss");
			string pPathToBuilds = Path.Combine(LocationName, "Builds");
			string pPathToFolder = Path.Combine(pPathToBuilds
			                                    ,sFolderName);
			string pPathToFolderWithDate = Path.Combine(pPathToFolder, sDate);
			if(!Directory.Exists(pPathToFolder)){
				Directory.CreateDirectory(pPathToFolder);
			}
			if(!Directory.Exists(pPathToFolderWithDate)){
				Directory.CreateDirectory(pPathToFolderWithDate);
			}
			string sExtension = "";
			switch(tTarget){
			case BuildTarget.StandaloneLinuxUniversal:
			case BuildTarget.StandaloneLinux:
				sExtension = ".x86";
				break;
			case BuildTarget.StandaloneLinux64:
				sExtension = ".x86_64";
				break;
			case BuildTarget.StandaloneWindows64:
			case BuildTarget.StandaloneWindows:
				sExtension = ".exe";
				break;
			case BuildTarget.StandaloneOSXIntel:
			case BuildTarget.StandaloneOSXIntel64:
			case BuildTarget.StandaloneOSXUniversal:
				sExtension = ".app";
				break;
			case BuildTarget.Android:
				sExtension = ".apk";
				break;
			}
			string pFilePath = Path.Combine(pPathToFolderWithDate, ApplicationName + sExtension);
			List<string> scenes = new List<string>();
            /*foreach( EditorBuildSettingsScene scene in EditorBuildSettings.scenes){
				if(scene.enabled){
					scenes.Add(scene.path);
				}
			}*/
            
            scenes.Add(EditorSceneManager.GetActiveScene().path);

            BuildPipeline.BuildPlayer(scenes.ToArray(),pFilePath,tTarget,oOptions); 
		}
        /*[MenuItem("Build/Build Mac (32-Bit)")]
		static void BuildMac32_Internal(){
			BuildMac32(true);
		}
		static void BuildMac32(bool bIncrement){
			Build("Mac_32",bIncrement, BuildTarget.StandaloneOSXIntel, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Mac (64-Bit)")]
		static void BuildMac64_Internal(){
			BuildMac64(true);
		}
		static void BuildMac64(bool bIncrement){
			Build("Mac_64",bIncrement, BuildTarget.StandaloneOSXIntel64, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Mac (Universal)")]
		static void BuildMacUniversal_Internal(){
			BuildMacUniversal(true);
		}
		static void BuildMacUniversal(bool bIncrement){
			Build("Mac_32_64",bIncrement, BuildTarget.StandaloneOSXUniversal, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Linux (32-Bit)")]
		static void BuildLinux32_Internal(){
			BuildLinux32(true);
		}
		static void BuildLinux32(bool bIncrement){
			Build("Linux_32",bIncrement, BuildTarget.StandaloneLinux, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Linux (64-Bit)")]
		static void BuildLinux64_Internal(){
			BuildLinux64(true);
		}
		static void BuildLinux64(bool bIncrement){
			Build("Linux_64",bIncrement, BuildTarget.StandaloneLinux64, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Linux (Universal)")]
		static void BuildLinuxUniversal_Internal(){
			BuildLinuxUniversal(true);
		}
		static void BuildLinuxUniversal(bool bIncrement){
			Build("Linux_32_64",bIncrement, BuildTarget.StandaloneLinuxUniversal, BuildOptions.ShowBuiltPlayer);
		}*/
        [MenuItem("Build/Build Windows (32-Bit)")]
		static void BuildPC32_Internal(){
			BuildPC32(true);
		}
		static void BuildPC32(bool bIncrement){
			Build("Windows_32",bIncrement, BuildTarget.StandaloneWindows, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Windows (64-Bit)")]
		static void BuildPC64_Internal(bool bIncrement){
			BuildPC64(true);
		}
		static void BuildPC64(bool bIncrement){
			Build("Windows_64",bIncrement, BuildTarget.StandaloneWindows64, BuildOptions.ShowBuiltPlayer);
		}
		[MenuItem("Build/Build Android")]
		static void BuildAndroid_Internal(){
			BuildAndroid(true);
		}
		static void BuildAndroid(bool bIncrement){
			Build("Android",bIncrement, BuildTarget.Android, BuildOptions.ShowBuiltPlayer);
		}
        #endregion




        public static string ReadTextFile(string sFileName)
		{
			string sFileNameFound = "";
			if (File.Exists(sFileName))
			{
				sFileNameFound = sFileName; 
			}
			else if (File.Exists(sFileName + ".txt"))
			{
				sFileNameFound = sFileName + ".txt";
			}
			else
			{
				return null;
			}			
			StreamReader sr;
			try
			{
				sr = new StreamReader(sFileNameFound);
			}
			catch (System.Exception e)
			{
				return null;
			}			
			string fileContents = sr.ReadToEnd();
			sr.Close();			
			return fileContents;
		}
		public static void WriteTextFile(string sFilePathAndName, string sTextContents)
		{
			StreamWriter sw = new StreamWriter(sFilePathAndName);
			sw.WriteLine(sTextContents);
			sw.Flush();
			sw.Close();
		}
	}
}