using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 100f;
        private float _damage = 10f;
        public float _attackRange = 2.5f;
        public float _attackRate = 1f;
        private EnemyStates _enemyStates;
        private NavMeshAgent _agent;
        private IDamageable _kitchen;
        private EnemyAnimator _animator;
        private float _timer;

        private void Start()
        {
            _enemyStates = GetComponent<EnemyStates>();
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
            _animator.AttackEvent.AddListener(ApplyDamage);
            //_kitchen = FindAnyObjectByType<Kitchen>();
        }

        private void Update()
        {
            _animator.SetAttackSpeed(1/_attackRate);
            _timer += Time.deltaTime;
            Attack();
        }

        private void CheckAttackRange()
        {
            if (_agent.remainingDistance > _attackRange || _enemyStates.CurrentState == States.Attack) return;
        }

        private void Attack()
        {
            CheckAttackRange();
            if (_timer <= _attackRate && _enemyStates.CurrentState != States.Idle)
            {
                //_enemyStates.CurrentState = States.Idle;
                return;
            }
            if (_enemyStates.CurrentState == States.Attack) return;
            _animator.PlayAttack();
            _enemyStates.CurrentState = States.Attack;
            _timer = 0;
        }

        public void GetDamage(float damage)
        {
            if (_health - damage <= 0)
            {
                print("Dead");
                _enemyStates.CurrentState = States.Dead;
            }
            else
            {
                _animator.PlayHit();
                _health -= damage;
            }
        }
        public void ApplyDamage()
        {
            print("ApplyDamage");
            _kitchen.GetDamage(_damage);
        }
    }
}
