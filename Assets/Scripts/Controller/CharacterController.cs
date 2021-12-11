using UnityEngine;


public sealed class CharacterController : IExecutable
{
    #region Fields

    private Camera _camera;
    private ExecutableCharacterFeature[] _executableFeatures;

    #endregion


    #region ClassLifeCycles

    public CharacterController()
    {
        _camera = Camera.main;

        _executableFeatures = new ExecutableCharacterFeature[1];
        var movement = new FirstPersonMovement(_camera.transform);

        _executableFeatures[0] = movement;
    }

    #endregion


    #region IExecutable

    public void Execute()
    {
        for (int i = 0; i < _executableFeatures.Length; i++)
        {
            _executableFeatures[i].Execute();
        }
    }

    #endregion
}