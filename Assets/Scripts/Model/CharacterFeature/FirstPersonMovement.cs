using UnityEngine;


public sealed class FirstPersonMovement : ExecutableCharacterFeature
{
    #region Fields

    private Transform _character;

    #endregion


    #region ClassLifeCycles

    public FirstPersonMovement(Transform charater)
    {
        _character = charater;
    }

    #endregion


    #region IExecutable

    public override void Execute()
    {

    }

    #endregion
}