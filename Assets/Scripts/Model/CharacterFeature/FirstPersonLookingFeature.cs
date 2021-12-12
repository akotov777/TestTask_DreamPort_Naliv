using UnityEngine;


public class FirstPersonLookingFeature : ExecutableCharacterFeature
{
    #region Fields

    private Transform _camera;
    private CharacterMovementSettings _settings;
    private float rotationY;

    #endregion


    #region ClassLifeCycles

    public FirstPersonLookingFeature()
    {
        _camera = Camera.main.transform;
        _settings = Resources.Load<CharacterMovementSettings>(Constants.ResourcesPaths.Data.PlayerMovementSettings);
    }

    #endregion


    #region Methods

    private void LookRotation()
    {
        float rotationX = _camera.localEulerAngles.y
                          + Controls.Looking.LookingXAxis()
                          * _settings.xSensetivity;

        rotationY += +Controls.Looking.LookingYAxis()
                     * _settings.ySensetivity;

        rotationY = Mathf.Clamp(rotationY, _settings.minLookingXAngel, _settings.maxLookingXAngel);

        _camera.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    public override void Execute()
    {
        if (!IsActive)
            return;
        if (!Controls.Looking.LookingIsActive())
            return;

        LookRotation();
    }

    #endregion
}
