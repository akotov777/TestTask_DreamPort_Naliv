using UnityEngine;
using System;


public sealed class PipeWaterFlow : RatioProviderBehaviour
{
    #region Fields

    [SerializeField] private RatioProviderBehaviour _closingRatioProvider;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _minRateOverTime;
    [SerializeField] private float _maxRateOverTime;
    [SerializeField] private float _minFlowSpeed;
    [SerializeField] private float _maxFlowSpeed;
    [SerializeField] private float _maxFlowAmount;
    [SerializeField] private Color _fluidColor;

    #endregion


    #region Properties

    public float CurrentFlow
    {
        get { return GetRatio() * _maxFlowAmount; }
    }

    public Color FluidColor
    {
        get
        {
            return _fluidColor;
        }
    }

    #endregion


    #region UnityMethods

    private void Start()
    {
        _closingRatioProvider.OnRatioChanged = RatioChanged;
        var main = _particles.main;
        main.startColor = _fluidColor;
    }

    #endregion


    #region Methods

    private void RatioChanged()
    {
        _onRatioChanged.Invoke();
        ChangeParticleParameters();
    }

    private void ChangeParticleParameters()
    {
        float newRateOverTime = Mathf.Lerp(
                                           _maxRateOverTime,
                                           _minRateOverTime,
                                           _closingRatioProvider.GetRatio());

        float newParticleSpeed = Mathf.Lerp(_maxFlowSpeed,
                                            _minFlowSpeed,
                                            _closingRatioProvider.GetRatio());

        var emission = _particles.emission;
        emission.rateOverTime = newRateOverTime;

        var main = _particles.main;
        main.startSpeed = newParticleSpeed;
    }

    #endregion


    #region IRatioProvider

    public override float GetRatio()
    {
        return 1.0f - _closingRatioProvider.GetRatio();
    }

    #endregion
}
