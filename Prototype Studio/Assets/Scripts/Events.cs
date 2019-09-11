using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to define events for the eventmanager
public class Events : MonoBehaviour
{
    public class PlayerChange : GameEvent
    {
        public ShapeMovement.PlayerShape Shape { get; }
        public ShapeMovement.PlayerColor Color { get; }

        public PlayerChange(ShapeMovement.PlayerShape shape, ShapeMovement.PlayerColor color)
        {
            Color = color;
            Shape = shape;
        }
    }
}
