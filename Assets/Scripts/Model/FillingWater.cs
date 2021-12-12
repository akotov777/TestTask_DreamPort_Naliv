using UnityEngine;
using System;


public sealed class FillingWater : RatioProviderBehaviour, IExecutable
{
    #region Fields

    [SerializeField] private float _maxFillAmount;
    [SerializeField] private PipeWaterFlow[] _waterFlows;
    [SerializeField] private GameObject _objectToScale;
    [SerializeField] private Renderer _waterRenderer;
    public Action OnOverFlow = () => { };
    private float _currentFillAmount;
    private float[] _perFlowRatio;
    private float[] _perFlowAmount;
    private float[] _perTickAmount;

    #endregion


    #region UnityMethods

    private void Start()
    {
        ServiceLocatorMonoBehaviour.GetService<SimulationController>().AddExecutable(this);
        _perFlowAmount = new float[_waterFlows.Length];
        _perFlowRatio = new float[_waterFlows.Length];
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

    private void ChangeWaterColor()
    {
        Color newColor = new Color();
        float alpha = _waterRenderer.sharedMaterial.color.a;
        for (int i = 0; i < _waterFlows.Length; i++)
        {
            newColor += _waterFlows[i].FluidColor * _perFlowRatio[i];
        }

        newColor.a = alpha;
        _waterRenderer.material.color = newColor;
    }

    private void CalculatePerFlowPercentage()
    {
        for (int i = 0; i < _waterFlows.Length; i++)
        {
            _perFlowRatio[i] = _perFlowAmount[i] / _currentFillAmount;
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
        ChangeWaterColor();
    }

    #endregion


    #region IRatioProvider

    public override float GetRatio()
    {
        return _currentFillAmount / _maxFillAmount;
    }

    #endregion
}