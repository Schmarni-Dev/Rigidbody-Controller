using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Movement Settings", menuName = "Schmarni"),Serializable]
public class movementSettings : ScriptableObject
{
    [Tooltip("The Max slope angle of the player")]
    public float maxSlopeAngle = 45f;
    [Tooltip("The Virtual max speed of the player")]
    public float MaxSpeed = 10f;
    [Tooltip("The move speed of the player")]
    public float moveSpeed = 10f;
    [Tooltip("The counter movement when the player goes to fast")]
    public float CounterMovement = 5f;
    [Tooltip("")]
    public float counterMovementThreshold = 0.01f;
    [Tooltip("Enable or Disable Airstrafing")]
    public bool Airstrafe = true;
    [Tooltip("Enable or Disable when grounded Airstrafing")]
    public bool AirstrafeOnGround = false;
    [Tooltip("Airstrafe Force")]
    public float AirstarfeForce = 5;
    [Tooltip("Max Airstrafe Speed")]
    public float MaxAirstarfeSpeed = 10;
}

