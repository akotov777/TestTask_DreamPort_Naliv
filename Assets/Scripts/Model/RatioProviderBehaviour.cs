using UnityEngine;
using System;


public abstract class RatioProviderBehaviour : MonoBehaviour, IRatioProvider
{
    #region Fields

    internal Action _onRatioChanged = () => { };

    #endregion


    #region IRatioProvider

    public Action OnRatioChanged
    {
        get { return _onRatioChanged; }
        set { _onRatioChanged = value; }
    }

    public abstract float GetRatio();

    #endregion
}