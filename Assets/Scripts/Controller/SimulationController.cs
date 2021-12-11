using UnityEngine;
using System.Collections.Generic;


public sealed class SimulationController : MonoBehaviour
{
    #region Fields

    private List<IExecutable> _executables = new List<IExecutable>();

    #endregion


    #region UnityMethods

    private void Update()
    {
        Execute();
    }

    #endregion


    #region Methods

    public void Execute()
    {
        for (int i = 0; i < _executables.Count; i++)
        {
            _executables[i].Execute();
        }
    }

    public void AddExecutable(IExecutable executable)
    {
        _executables.Add(executable);
    }

    public void RemoveExecutable(IExecutable executable)
    {
        if (_executables.Contains(executable))
        {
            _executables.Remove(executable);
        }
    }

    #endregion
}