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

        _executableFeatures = new ExecutableCharacterFeature[2];
        var movement = new FirstPersonMovementFeature(_camera.transform);
        var looking = new FirstPersonLookingFeature();

        _executableFeatures[0] = movement;
        _executableFeatures[1] = looking;
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