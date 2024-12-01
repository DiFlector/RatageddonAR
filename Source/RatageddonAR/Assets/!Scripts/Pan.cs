using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Pan : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _itemAnchor;
    private Quaternion initialRotation;
    [SerializeField] private float tiltSensitivity = 10f;
    [SerializeField] private float _rotationLimit = 10f;
    [SerializeField] private float _threshold;
    private Vector3 _lastGyroRotation;
    private Vector3 _lastMousePosition;
    private float _progress = 0f;
    private Projectile _item;
    private Player _player;


    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    public void Interact(Player player)
    {
        if (player.ItemInHand == null) return;
        if (player.ItemInHand is Projectile)
        {
            _item = player.ItemInHand as Projectile;
            _item.GetComponentInChildren<Collider>().enabled = true;
            if (_item.IsCooked) return;
        }
        _player = player;
        player.ItemInHand.transform.DOMove(_itemAnchor.position, 0.5f).SetEase(Ease.InOutSine).onComplete += () =>
        {
            
            player.ItemInHand.transform.parent = _itemAnchor;
            player.ItemInHand.GetComponent<Rigidbody>().isKinematic = false;
            player.ItemInHand.GetComponent<Rigidbody>().useGravity = true;
        };
        StartCoroutine(Cooking());
    }
    private IEnumerator Cooking()
    {
        _progress = 0;
        while (true)
        {
#if UNITY_EDITOR
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 deltaMouse = currentMousePosition - _lastMousePosition;

            float tiltX = deltaMouse.y * tiltSensitivity;
            float tiltY = -deltaMouse.x * tiltSensitivity;

            float mouseDistance = Vector3.Distance(currentMousePosition, _lastMousePosition);

            if (mouseDistance > _threshold)
            {
                _progress += Time.deltaTime;
                _progress = Mathf.Clamp(_progress, 0f, 3);
            }


            transform.DORotateQuaternion(initialRotation * Quaternion.Euler(
                Mathf.Clamp(tiltX, -_rotationLimit, _rotationLimit),
                Mathf.Clamp(tiltY, -_rotationLimit, _rotationLimit),
                0
            ), 0.1f);

            _lastMousePosition = currentMousePosition;

            Debug.Log($"Progress (Editor): {_progress}/{3}");
#else

            Vector3 currentGyroRotation = Input.gyro.rotationRateUnbiased;

            Vector3 deltaRotation = currentGyroRotation - _lastGyroRotation;
            float deltaProgress = Mathf.Abs(deltaRotation.x) + Mathf.Abs(deltaRotation.y) + Mathf.Abs(deltaRotation.z);

            if (Mathf.Abs(currentGyroRotation.x) > _threshold ||
                    Mathf.Abs(currentGyroRotation.y) > _threshold ||
                    Mathf.Abs(currentGyroRotation.z) > _threshold)
            {
                // Добавляем время к прогрессу
                _progress += Time.deltaTime;
                _progress = Mathf.Clamp(_progress, 0f, 100);
            }

            _lastGyroRotation = currentGyroRotation;

            float tiltX = Mathf.Clamp(currentGyroRotation.x * tiltSensitivity, -_rotationLimit, _rotationLimit);
            float tiltY = Mathf.Clamp(currentGyroRotation.y * tiltSensitivity, -_rotationLimit, _rotationLimit);

            transform.rotation = initialRotation * Quaternion.Euler(tiltX, tiltY, 0);

            Debug.Log($"Progress (Gyro): {_progress}/{100}");
            

#endif
            if (_progress == 3)
            {
                _item.Cook();
                _item.GetComponent<Rigidbody>().useGravity = false;
                _item.GetComponent<Rigidbody>().isKinematic = true;
                _player.TakeItem(_item);
                yield break;
            }
            yield return null;
        }
    }
}
