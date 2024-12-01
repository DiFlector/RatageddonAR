using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 100f;
        [SerializeField] private ParticleSystem _burningEffect;
        private int _damage = 10;
        public float _attackRange = 2.5f;
        public float _attackRate = 1f;
        private EnemyStates _enemyStates;
        private NavMeshAgent _agent;
        private IDamageable _kitchen;
        private EnemyAnimator _animator;
        private float _timer;
        
        [Header("Damage over time")]
        [SerializeField] private float _damageTickRate = 1f;
        [SerializeField] private float _tickDamageDuration = 1f;
        private Coroutine damageCoroutine;

        private void Start()
        {
            _enemyStates = GetComponent<EnemyStates>();
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
            _animator.AttackEvent.AddListener(ApplyDamage);
            _kitchen = FindAnyObjectByType<Kitchen>() as IDamageable;
        }

        private void Update()
        {
            //_animator.SetAttackSpeed(1/_attackRate);
            _timer += Time.deltaTime;
            Attack();
        }

        private void Attack()
        {
            if (_agent.remainingDistance > _attackRange) return;
            if (_timer <= _attackRate) return;
            if (_enemyStates.CurrentState == States.Attack) return;
            _animator.PlayAttack();
            _enemyStates.CurrentState = States.Attack;
            _timer = 0;
        }

        public void GetDamage(int damage, DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Burst:
                    print("Burst");
                    if (_health - damage <= 0)
                    {
                        StartCoroutine(Die());
                    }
                    else
                    {
                        _animator.PlayHit();
                        _health -= damage;
                    }
                    break;
                case DamageType.Continuous:
                    GetDamageOverTime(damage);
                    break;
            }
            
        }


        private void GetDamageOverTime(int damage)
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageRoutine(damage));
            }
        }

        private IEnumerator DamageRoutine(int damage)
        {
            float elapsedTime = 0f;
            
            while (elapsedTime < _tickDamageDuration)
            {
                print("Continuous tick");
                if (_health - damage <= 0)
                {
                    StartCoroutine(Die());
                }
                else
                {
                    _animator.PlayHit();
                    _health -= damage;
                }
                yield return new WaitForSeconds(_damageTickRate); // Ждать интервал
                elapsedTime += _damageTickRate;
            }
            if (_burningEffect)
            {
                _burningEffect.Stop();
            }
            damageCoroutine = null;
        }

        private IEnumerator Die()
        {
            _enemyStates.CurrentState = States.Dead;
            yield return new WaitForSeconds(2);
            transform.DOMoveY(-0.5f, 5).onComplete += () => Destroy(gameObject);
        }
        public void ApplyDamage()
        {
            _kitchen.GetDamage(_damage, DamageType.Burst);
        }
    }
}

public enum DamageType
{
    Burst,
    Continuous
}
