using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class NDSaveLoad
{
    public static string fileName = "default.txt";
    private static string saveFolder = "saves";
    private static char saveToken = '='; //this divides keys from values in the file, not allowed to use
    private static string directoryPath = null;
    private static Dictionary<string, string> dataDict;

    static NDSaveLoad(){ //static constructor, called automatically :3
        dataDict = new Dictionary<string, string>();
        directoryPath = Directory.GetParent(Application.dataPath).FullName;
        directoryPath = Path.Combine(directoryPath, saveFolder);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        Debug.Log($"Save Load file set to {directoryPath}");
    }

    public static void SetFileName(string newFileName){
        fileName = newFileName;
    }



    public static void LoadFromFile(){
        string filePath = Path.Combine(directoryPath, fileName);
        dataDict.Clear();

        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(saveToken);
                    if (parts.Length == 2)
                    {
                        dataDict[parts[0].Trim()] = parts[1].Trim();
                    }
                }
                Debug.Log($"Loaded save file: {fileName}");
            }
        }
        catch
        {
            Debug.LogError("Could not load from file.");
        }
    }

    public static void SaveToFile(){
        string filePath = Path.Combine(directoryPath, fileName);

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var pair in dataDict)
                {
                    writer.WriteLine($"{pair.Key}{saveToken}{pair.Value}");
                }
            }
            Debug.Log($"Saved to file: {fileName}");
        }
        catch
        {
            Debug.LogError("Could not save to file.");
        }
    }

    public static void Flush(){
        SaveToFile();
    }

    public static void SaveDataDict(string key, string value){
        dataDict[key] = value;
    }

    public static string GetData(string key, string defaultValue = ""){
        return dataDict.TryGetValue(key, out string value) ? value : defaultValue;
    }

    public static void SaveInt(string key, int value){
        dataDict[key] = value.ToString();
    }

    public static int LoadInt(string key, int defaultValue = 0){
        if (dataDict.TryGetValue(key, out string value))
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
        }
        return defaultValue;
    }

    public static void SaveFloat(string key, float value){
        dataDict[key] = value.ToString("F6");
    }

    public static float LoadFloat(string key, float defaultValue = 0f){
        if (dataDict.TryGetValue(key, out string value))
        {
            if (float.TryParse(value, out float result))
            {
                return result;
            }
        }
        return defaultValue;
    }

    public static void SaveColor(string key, Color color){
        string colorString = $"{color.r:F6},{color.g:F6},{color.b:F6},{color.a:F6}";
        dataDict[key] = colorString;
    }

    public static Color LoadColor(string key)
    {
        if (dataDict.TryGetValue(key, out string value))
        {
            string[] components = value.Split(',');
            if (components.Length == 4 &&
                float.TryParse(components[0], out float r) &&
                float.TryParse(components[1], out float g) &&
                float.TryParse(components[2], out float b) &&
                float.TryParse(components[3], out float a))
            {
                return new Color(r, g, b, a);
            }
        }

        return Color.white;
    }

    public static void SaveVector3(string key, Vector3 vector){
        string vectorString = $"{vector.x:F6},{vector.y:F6},{vector.z:F6}";
        dataDict[key] = vectorString;
    }

    public static Vector3 LoadVector3(string key, Vector3 defaultValue = default)
    {
        if (dataDict.TryGetValue(key, out string value))
        {
            string[] components = value.Split(',');
            if (components.Length == 3 &&
                float.TryParse(components[0], out float x) &&
                float.TryParse(components[1], out float y) &&
                float.TryParse(components[2], out float z))
            {
                return new Vector3(x, y, z);
            }
        }
        return defaultValue;
    }

}