using Godot;
using System;
public class SceneLoaderSingleton
{

    private static SceneLoaderSingleton _instance;
    public PackedScene NewHearElement;

    public SceneLoaderSingleton()
    {
        this.NewHearElement = ResourceLoader.Load<PackedScene>("res://World/HeartElement.tscn");
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
        HeartElement newHeart = this.NewHearElement.Instance<HeartElement>();
        return newHeart;
    }
}