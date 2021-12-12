using UnityEngine;


[CreateAssetMenu(fileName = "CharacterMovementSettings", menuName = "ScriptableObjects/CharacterMovementSettings")]
class CharacterMovementSettings : ScriptableObject
{
    #region Fields

    public float movementSpeed = 5.0f;
    public float xSensetivity = 1.0f;
    public float ySensetivity = 1.0f;
    public float minLookingXAngel = -90.0f;
    public float maxLookingXAngel = 90.0f;

    #endregion
}

