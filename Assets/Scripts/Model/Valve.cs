using UnityEngine;
using System;


public sealed class Valve : RatioProviderBehaviour, IExecutable
{
    #region Fields

    [SerializeField, Min(1)] private float _maxAngle = 720;
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private bool _isClosedOnStart;
    private Vector3 _previousPosition;
    private Vector3 _currentPosition;
    private bool _hitFirstTime = true;
    private bool _isClicked;
    private float _turnedAngle;
    private float _angle;

    #endregion


    #region Properties

    public float ClosingRatio
    {
        get
        {
            return _turnedAngle / _maxAngle;
        }
    }

    private Vector3 Intersection
    {
        get
        {
            return Math.FindRayPlaneIntersection(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    transform.position,
                    transform.up);
        }
    }

    private float GetAngleInDegrees
    {
        get
        {
            return Vector3.SignedAngle(_previousPosition, _currentPosition, transform.up);
        }
    }

    #endregion


    #region UnityMethods

    private void Start()
    {
        if (_isClosedOnStart)
        {
            _objectToRotate.transform.Rotate(0, _maxAngle, 0);
            _turnedAngle = _maxAngle;
        }

        ServiceLocatorMonoBehaviour.GetService<SimulationController>().AddExecutable(this);
    }

    private void OnMouseDown()
    {
        _isClicked = true;
    }

    private void OnMouseUp()
    {
        _isClicked = false;
    }

    #endregion


    #region Methods

    private bool TryGetIntersection(out Vector3 intersection)
    {
        return Math.TryFindRayPlaneIntersection(
             Camera.main.ScreenPointToRay(Input.mousePosition),
             transform.position,
             transform.up, out intersection);
    }

    #endregion


    #region IExecutable

    public void Execute()
    {
        if (_isClicked && _hitFirstTime)
        {
            _previousPosition = transform.InverseTransformPoint(Intersection);
            _hitFirstTime = false;
        }
        else if (_isClicked)
        {
            if (!TryGetIntersection(out _currentPosition))
                return;
            _currentPosition = transform.InverseTransformPoint(_currentPosition);

            _angle = GetAngleInDegrees;
            if (_turnedAngle + _angle > _maxAngle)
            {
                _angle = _maxAngle - _turnedAngle;
            }
            else if (_turnedAngle + _angle < 0)
            {
                _angle = -_turnedAngle;
            }

            _objectToRotate.transform.Rotate(0, _angle, 0);
            _turnedAngle += _angle;
            _previousPosition = _currentPosition;
            _onRatioChanged.Invoke();
        }
        else if (!Controls.Interaction.DragButtonHeldDown())
        {
            _hitFirstTime = true;
        }
    }

    #endregion


    #region IRatioProvider

    public override float GetRatio()
    {
        return ClosingRatio;
    }

    #endregion
}