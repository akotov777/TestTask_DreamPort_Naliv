using System;


public interface IRatioProvider
{
    Action OnRatioChanged { get; }
    float GetRatio();
}