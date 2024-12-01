using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LuckyBox : PickableObject, IInteractable
{
    public event Action OnExplode;

    [SerializeField] private float _shakeThreshold = 2f;
    [SerializeField] private float _shakeCooldown = 1f;
    [SerializeField, Min(1)] private int _shakesForExplode = 3;
    [SerializeField] private ParticleSystem _explosion;

    private Vector3 _lastAcceleration;
    private float _lastShakeTime;

    private bool _isExploding = false;
    public override void Interact(Player player)
    {
        base.Interact(player);
        OnExplode += player.GetRandomIngredient;
        player.Babax.onClick.AddListener(() => StartCoroutine(Explode()));
        _lastAcceleration = Input.acceleration;
        _lastShakeTime = Time.time;
        Debug.Log("INTERACT");
    }

    private IEnumerator Translate(Player interactor)
    {
        transform.DOMove(interactor.ItemAnchor.position, 0.4f).SetEase(Ease.InOutSine);
        while (DOTween.IsTweening(transform))
            yield return null;
        transform.SetParent(interactor.ItemAnchor);
    }

    private void Update()
    {
        Vector3 currentAcceleration = Input.acceleration;
        float accelerationDelta = (currentAcceleration - _lastAcceleration).magnitude;

        if (accelerationDelta > _shakeThreshold && Time.time > _lastShakeTime + _shakeCooldown)
        {
            Handheld.Vibrate();
            _shakesForExplode--;
            _lastShakeTime = Time.time;
        }
        if (_shakesForExplode == 0)
        {
            StartCoroutine(Explode());
            _shakesForExplode = -1;
        }
        _lastAcceleration = currentAcceleration;
    }

    private IEnumerator Explode()
    {
        if (!_isExploding)
        {
            _explosion.gameObject.SetActive(true);
            _explosion.transform.parent = null;
            _explosion.Play();
            transform.DOScale(Vector3.zero, _explosion.main.duration * 0.7f);
            while (DOTween.IsTweening(transform))
                yield return null;

            Debug.Log("INVOKE");
            OnExplode?.Invoke();
            Invoke(nameof(DestroyObj), _explosion.main.duration * 0.3f);
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
        Destroy(_explosion.gameObject);
    }
}


public interface IInteractable
{
    public void Interact(Player player);
}
