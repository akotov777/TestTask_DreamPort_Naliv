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

    public static class Movement
    {
        public static float GetVerticalAxis()
        {
            return Input.GetAxis("Vertical");
        }

        public static float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}