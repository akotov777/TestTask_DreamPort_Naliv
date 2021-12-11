using UnityEngine;


public sealed class Valve : MonoBehaviour, IExecutable
{
    #region Fields

    [SerializeField, Min(1)] private float maxAngle = 720;
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private bool isClosedOnStart;
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
            return _turnedAngle / maxAngle;
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
        if (isClosedOnStart)
        {
            objectToRotate.transform.Rotate(0, maxAngle, 0);
            _turnedAngle = maxAngle;
        }
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
            _previousPosition = Intersection;
            _hitFirstTime = false;
        }
        else if (_isClicked)
        {
            if (!TryGetIntersection(out _currentPosition))
                return;
            _currentPosition = transform.InverseTransformPoint(_currentPosition);

            _angle = GetAngleInDegrees;
            if (_turnedAngle + _angle > maxAngle)
            {
                _angle = maxAngle - _turnedAngle;
            }
            else if (_turnedAngle + _angle < 0)
            {
                _angle = -_turnedAngle;
            }

            objectToRotate.transform.Rotate(0, _angle, 0);
            _turnedAngle += _angle;
            _previousPosition = _currentPosition;
        }
        else if (!Controls.Interaction.DragButtonHeldDown())
        {
            _hitFirstTime = true;
        }
    }

    #endregion
}