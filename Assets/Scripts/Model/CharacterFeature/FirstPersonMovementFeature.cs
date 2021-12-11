using UnityEngine;


public sealed class FirstPersonMovementFeature : ExecutableCharacterFeature
{
    #region Fields

    private Transform _character;
    private CharacterMovementSettings _settings;

    #endregion


    #region ClassLifeCycles

    public FirstPersonMovementFeature(Transform charater)
    {
        _character = charater;
        _settings = Resources.Load<CharacterMovementSettings>(Constants.ResourcesPaths.Data.PlayerMovementSettings);
    }

    #endregion


    #region IExecutable

    public override void Execute()
    {
        if (!IsActive)
            return;

        Vector3 forward = Quaternion.AngleAxis(-_character.rotation.eulerAngles.x,
                                                _character.right) 
                          * _character.forward;

        Vector3 desiredMove = forward * Controls.Movement.GetVerticalAxis()
                              +_character.right * Controls.Movement.GetHorizontalAxis();

        var moveDirection = desiredMove * _settings.movementSpeed;

        _character.Translate(moveDirection, Space.World);
    }

    #endregion
}