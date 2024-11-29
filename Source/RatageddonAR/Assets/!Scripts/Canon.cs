using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private int _resolution = 30;

    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private Vector3 _initialAngle = new Vector3(45, 0, 0);

    private void FixedUpdate()
    {
        DrawTrajectory();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] trajectoryPoints = CalculateTrajectory();
        _trajectoryLine.positionCount = trajectoryPoints.Length;
        _trajectoryLine.SetPositions(trajectoryPoints);
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3 velocity = Quaternion.Euler(_initialAngle) * Vector3.forward * _launchForce;
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

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _launchPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 velocity = Quaternion.Euler(_initialAngle) * Vector3.forward * _launchForce;
        rb.linearVelocity = velocity;
        rb.useGravity = true;
    }
}
