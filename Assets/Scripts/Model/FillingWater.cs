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
        ChangeWaterLevel();
    }

    #endregion


    #region Methods

    private void ChangeWaterLevel()
    {
        float oldX = _objectToScale.transform.localScale.x;
        float oldZ = _objectToScale.transform.localScale.z;
        _objectToScale.transform.localScale = new Vector3(oldX, GetRatio(), oldZ);
    }

    private void CalculatePerFlowPercentage()
    {
        for (int i = 0; i < _waterFlows.Length; i++)
        {
            _perFlowPercentage[i] = _perFlowAmount[i] / _maxFillAmount;
        }
    }

    #endregion


    #region IExecutable

    public void Execute()
    {
        if (_currentFillAmount >= _maxFillAmount)
            return;

        float addAmount = 0;
        for (int i = 0; i < _waterFlows.Length; i++)
        {
            _perTickAmount[i] = _waterFlows[i].CurrentFlow * Time.deltaTime;
            _perFlowAmount[i] += _perTickAmount[i];
            addAmount += _perTickAmount[i];
        }

        if (_currentFillAmount + addAmount > _maxFillAmount)
        {
            float beforeOverflow = _maxFillAmount - _currentFillAmount;
            float ratio = beforeOverflow / addAmount;
            for (int i = 0; i < _waterFlows.Length; i++)
            {
                _perFlowAmount[i] -= _perTickAmount[i] * ratio;
            }
            _currentFillAmount += beforeOverflow;
            OnOverFlow.Invoke();
        }
        else
        {
            _currentFillAmount += addAmount;
        }

        CalculatePerFlowPercentage();

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