using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IOFileWork
{
    private string path = @"Assets\MapInfo";

    public IOFileWork(string fileName)
    {
        path += fileName;
    }

    public async void Write(List<PointProperties> points)
    {   
        MapProperties map = new MapProperties();
        map.Points = points;
        await File.WriteAllTextAsync(path, JsonUtility.ToJson(map));
    }

    public MapProperties Read()
    {   
        string file = File.ReadAllText(path);
        MapProperties map = JsonUtility.FromJson<MapProperties>(file);
        return map;
    }
}
