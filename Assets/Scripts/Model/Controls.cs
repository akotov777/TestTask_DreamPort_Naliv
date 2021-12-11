using UnityEngine;


public static class Controls
{
    public static class Interaction
    {
        public static bool InteractionButtonPressed()
        {
            return Input.GetMouseButtonDown(0);
        }

        public static bool DragButtonHeldDown()
        {
            return Input.GetMouseButton(0);
        }
    }
}