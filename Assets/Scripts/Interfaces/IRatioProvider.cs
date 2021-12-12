using System;


public interface IRatioProvider
{
    Action OnRatioChanged { get; set; }
    float GetRatio();
}