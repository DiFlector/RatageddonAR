using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Enemy.Scripts
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("IsDead");
        private static readonly int Walk = Animator.StringToHash("IsMoving");
        private static readonly int Idle = Animator.StringToHash("IsIdle");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int Hit = Animator.StringToHash("Hit");
        
        public UnityEvent AttackEvent = new UnityEvent();
        public UnityEvent AttackEndEvent = new UnityEvent();

        private void Update()
        {
            AttackTiming();
        }

        public void GetParameterState(string parameterName)
        {
            print(_animator.GetBool(parameterName));
        }
    
        public void PlayAttack()
        {
            print("Play Attack Trigger");
            _animator.SetTrigger(Attack);
        }
        
        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
        }

        public void IsDead(bool condition)
        {
            _animator.SetBool(Death, condition);
        }
    
        public void IsMoving(bool condition)
        {
            _animator.SetBool(Walk, condition);
        }
    
        public void IsIdle(bool condition)
        {
            _animator.SetBool(Idle, condition);
        }

        private void AttackTiming()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Zombie Attack") && Mathf.Approximately(stateInfo.normalizedTime, 0.06f))
            {
                AttackEvent.Invoke();
            }
            if (stateInfo.IsName("Zombie Attack") && stateInfo.normalizedTime >= 0.99f)
            {
                AttackEndEvent.Invoke();
            }
        }

        public void SetAttackSpeed(float speed)
        {
            _animator.SetFloat(AttackSpeed, speed);
        }
    }
}