/*

using System;
using System.Collections.Generic;
using UnityEngine;

enum Colour
{
	Red,
	Blue
}
public enum FacePosition
{
    Left = 1,
    Up = 2,
    Down = 3,
    Right = 4,
    Base = 5
}
public enum VisorPosition
{
    Left,
    Right,
    Mid
}
public enum VisorColour
{
    Red,
    Yellow,
    Blue,
    Green
}

public struct FaceData
{
    public FacePosition FacePosition { get; set; }
    public VisorPosition VisorPosition { get; set; }
    public VisorColour FaceVisorColour { get; set; }
}


public class Pyramid : MonoBehaviour 
{
    public Color Color { get; set; }
    public bool Shielded { get; set; }
   
    public List<FaceData> FaceData = new();

    void Awake()
    {
        FaceData.Add( new FaceData() { FacePosition = FacePosition.Left } );
        FaceData.Add( new FaceData() { FacePosition = FacePosition.Up } );
        FaceData.Add( new FaceData() { FacePosition = FacePosition.Down } );
        FaceData.Add( new FaceData() { FacePosition = FacePosition.Right } );
        FaceData.Add( new FaceData() { FacePosition = FacePosition.Base } );
    }
    
}

*/