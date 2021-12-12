using UnityEngine;
using System;


public sealed class PipeWaterFlow : MonoBehaviour, IRatioProvider
{
    #region Fields

    [SerializeField, SerializeReference] private IRatioProvider _closingRatioProvider;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _minRateOverTime;
    [SerializeField] private float _maxRateOverTime;
    [SerializeField] private float _minFlowSpeed;
    [SerializeField] private float _maxFlowSpeed;
    private Action _onRatioChanged;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _onRatioChanged = RatioChanged;
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

    public Action OnRatioChanged
    {
        get { return _onRatioChanged; }
    }

    public float GetRatio()
    {
        return _closingRatioProvider.GetRatio();
    }

    #endregion
}
