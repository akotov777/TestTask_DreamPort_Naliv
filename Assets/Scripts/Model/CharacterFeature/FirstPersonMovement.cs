using UnityEngine;


public sealed class FirstPersonMovement : ExecutableCharacterFeature
{
    #region Fields

    private Transform _character;
    private CharacterMovementSettings _settings;

    #endregion


    #region ClassLifeCycles

    public FirstPersonMovement(Transform charater)
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


        Vector3 desiredMove = _character.forward * Controls.Movement.GetVerticalAxis()
                              +_character.right * Controls.Movement.GetHorizontalAxis();

        var moveDirection = desiredMove * _settings.movementSpeed;

        _character.Translate(desiredMove);
       // _characterController.Move(_moveDirection * Time.deltaTime);
    }

    #endregion
}