using UnityEngine;


public sealed class CharacterController : IExecutable
{
    #region Fields

    private Camera _camera;

    #endregion


    #region ClassLifeCycles

    public CharacterController()
    {
        _camera = Camera.main;
    }

    #endregion


    #region IExecutable

    public void Execute()
    {

    }

    #endregion
}