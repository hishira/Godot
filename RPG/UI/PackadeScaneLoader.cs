using Godot;
using System;
public class SceneLoaderSingleton
{

    private static SceneLoaderSingleton _instance;
    public PackedScene NewHearElement;

    public SceneLoaderSingleton()
    {
        NewHearElement = ResourceLoader.Load<PackedScene>("res://World/HeartElement.tscn");
    }

    public static SceneLoaderSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SceneLoaderSingleton();
        }
        return _instance;
    }

    public HeartElement GetHeartElement(){
        HeartElement newHeart = NewHearElement.Instance<HeartElement>();
        return newHeart;
    }
}