using System;
using UnityEngine;
using Zenject;

public class BattleView : View
{
    [Inject] private readonly Canon _canon;
    [SerializeField] private FixedJoystick _joystick;

    private void FixedUpdate()
    {
        _canon.ChangeTrajectory(new Vector2(_joystick.Horizontal, _joystick.Vertical));
    }
}
