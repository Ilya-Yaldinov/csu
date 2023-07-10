using System;

[Serializable]
public class PointProperties
{
    public int Id;
    public int Floor;
    public int PointType;
    public float X;
    public float Y;

    public string TextFirst;
    public string TextSecond;
    public string TextThird;

    public PointProperties(float x, float y)
    { 
        this.Id = 0;
        this.X = x;
        this.Y = y;
    }

    public PointProperties() { }
}