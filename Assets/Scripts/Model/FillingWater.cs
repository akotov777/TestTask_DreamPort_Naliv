using UnityEngine;
using System;


public sealed class FillingWater : RatioProviderBehaviour, IExecutable
{
    #region Fields

    [SerializeField] private float _maxFillAmount;
    [SerializeField] private PipeWaterFlow[] _waterFlows;
    [SerializeField] private GameObject _objectToScale;
    public Action OnOverFlow = () => { };
    private float _currentFillAmount;
    private float[] _perFlowPercentage;
    private float[] _perFlowAmount;
    private float[] _perTickAmount;

    #endregion


    #region UnityMethods

    private void Start()
    {
        ServiceLocatorMonoBehaviour.GetService<SimulationController>().AddExecutable(this);
        _perFlowAmount = new float[_waterFlows.Length];
        _perFlowPercentage = new float[_waterFlows.Length];
        _perTickAmount = new float[_waterFlows.Length];
    }

    #endregion


    #region Methods

    private void ChangeWaterLevel()
    {
        var scale = _objectToScale.transform.localScale;
        scale.x = GetRatio();
    }

    #endregion


    #region IExecutable

    public void Execute()
    {
        float addAmount = 0;
        for (int i = 0; i < _waterFlows.Length; i++)
        {
            _perTickAmount[i] = _waterFlows[i].CurrentFlow * Time.deltaTime;
            _perFlowAmount[i] += _perTickAmount[i];
            addAmount += _perFlowAmount[i];
        }

        if (_currentFillAmount + addAmount > _maxFillAmount)
        {
            float beforeOverflow = _maxFillAmount - _currentFillAmount;
            float ratio = beforeOverflow / addAmount;
            for (int i = 0; i < _waterFlows.Length; i++)
            {
                _perFlowAmount[i] -= _perTickAmount[i] * ratio;
            }
            OnOverFlow.Invoke();
        }
        _currentFillAmount += addAmount;

        for (int i = 0; i < _waterFlows.Length; i++)
        {
            _perFlowPercentage[i] = _perFlowAmount[i] / _maxFillAmount;
        }

        ChangeWaterLevel();
    }

    #endregion


    #region IRatioProvider

    public override float GetRatio()
    {
        return _currentFillAmount / _maxFillAmount;
    }

    #endregion
}