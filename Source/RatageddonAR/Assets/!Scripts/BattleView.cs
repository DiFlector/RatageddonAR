using UnityEngine;
using Zenject;

public class BattleView : View
{
    [Inject] private readonly GameManager _gameManager;
    private Canon _canon;
    [SerializeField] private FixedJoystick _joystick;

    public override void Initialize()
    {
        _gameManager.GetTask<PreparationTask>().OnTaskCompleted += () => _viewManager.Show<BattleView>(true, false);
    }

    public void ConnectToCanon(Canon canon)
    {
        _joystick.OnRelease += canon.LaunchProjectile;
        _canon = canon;
    }

    private void FixedUpdate()
    {
        if (_canon == null) return;
        _canon.ChangeTrajectory(new Vector2(_joystick.Horizontal, _joystick.Vertical));
    }
}
