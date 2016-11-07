using UnityEngine;
using System.Collections;
using UnityEditor;

public class PropsWindow : EditorWindow
{
    public Object[] textures;
    public string searchString = "lalapdokapzkpzaokdpakdzpokzadpokzakd";
    public Vector2 scrollPos;
    string t = "This is a string inside a Scroll view!";

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Props")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PropsWindow window = (PropsWindow)EditorWindow.GetWindow(typeof(PropsWindow)); 
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.Label(t);
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
            t += " \nAnd this is more text!";
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Clear"))
            t = "";
        textures = Resources.LoadAll("Environment", typeof(Sprite));
        float x = 0;
        float y = 0;
        foreach (Sprite texture in textures)
        {
            //Graphics.DrawTexture(texture.rect, texture.texture);
            //EditorGUI.DrawPreviewTexture(new Rect(x, y, texture.textureRect.width, texture.textureRect.height), texture.texture);
            //x += texture.textureRect.width;
        }
    }
}