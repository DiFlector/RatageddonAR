using DG.Tweening;
using UnityEngine;
using Zenject;

public class Canon : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private int _resolution = 15;
    [SerializeField] private int _maxYAngleAbs = 30;
    [SerializeField] private float _maxLaunchForce = 8;
    [SerializeField] private Animation _anim;

    [SerializeField] private float _launchForce = 2f;
    [SerializeField] private Vector3 _localAngle = new (-45, 0, 0);

    private Player _player;
    private Projectile _projectile;

    private void Awake()
    {
        DrawTrajectory();
    }


    public void Interact(Player player)
    {
        if (player.ItemInHand is not Projectile) return;
        _projectile = player.ItemInHand as Projectile;
        if (!_projectile.IsCooked) return;
        _projectile.GetComponentInChildren<Collider>().enabled = true;
        player.ToggleJoystick(true);
        _trajectoryLine.enabled = true;
        _projectile.transform.parent = null;
        _projectile.transform.DOMove(_launchPoint.position, 1).SetEase(Ease.InOutSine).onComplete += () => _projectile.transform.parent = _launchPoint;
        _maxLaunchForce = _projectile.ShotStrength;
        _player = player;
        player.ClearItem();
    }

    public void ChangeTrajectory(Vector2 joystickInput)
    {
        if (_projectile == null) return;
        if (Mathf.Abs(_localAngle.y + joystickInput.x * 15 * Time.deltaTime) < _maxYAngleAbs)
            _localAngle += new Vector3(0, joystickInput.x * 15 * Time.deltaTime, 0);
        if (_launchForce + joystickInput.y * Time.deltaTime < _maxLaunchForce &&
            _launchForce + joystickInput.y * Time.deltaTime > 2f)
            _launchForce += joystickInput.y * Time.deltaTime;


        DrawTrajectory();
    }    

    private void DrawTrajectory()
    {
        if (!_trajectoryLine.enabled) return;
        Vector3[] trajectoryPoints = CalculateTrajectory();
        _trajectoryLine.positionCount = trajectoryPoints.Length;
        _trajectoryLine.SetPositions(trajectoryPoints);
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3 localDirection = Quaternion.Euler(_localAngle) * Vector3.forward;
        Vector3 globalDirection = transform.TransformDirection(localDirection);
        Vector3 velocity = globalDirection * _launchForce;
        Vector3 gravity = Physics.gravity;
        Vector3[] points = new Vector3[_resolution];
        float timeStep = 0.1f;

        for (int i = 0; i < _resolution; i++)
        {
            float t = i * timeStep;
            points[i] = _launchPoint.position + velocity * t + 0.5f * gravity * t * t;
        }

        return points;
    }

    public void LaunchProjectile()
    {
        if (_projectile == null) return;
        _anim.Play();
        Rigidbody rb = _projectile.GetComponent<Rigidbody>();
        Vector3 localDirection = Quaternion.Euler(_localAngle) * Vector3.forward;
        Vector3 globalDirection = transform.TransformDirection(localDirection);
        Vector3 velocity = globalDirection * _launchForce;
        rb.isKinematic = false;
        rb.linearVelocity = velocity;
        rb.useGravity = true;
        _projectile = null;
        _trajectoryLine.enabled = false;
        _player.ToggleJoystick(false);
    }

}
