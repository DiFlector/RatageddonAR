using UnityEngine;


namespace Enemy.Scripts
{
    public class EnemyStates : MonoBehaviour
    {
        private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAnimator _animator;
        

        public States CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState == value) return;
                _currentState = value;
                UpdateState();
            }
        }

        [SerializeField]private States _currentState;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _animator = GetComponent<EnemyAnimator>();
            _animator.AttackEndEvent.AddListener(() => CurrentState = States.Idle );
            CurrentState = States.Idle;
        }

        private void UpdateState()
        {
            //print("UpdateState called with state: " + CurrentState);
            switch (CurrentState)
            {
                case States.Moving:
                    SetMoving();
                    break;
                case States.Attack:
                    SetAttacking();
                    break;
                case States.Dead:
                    SetDead();
                    break;
                case States.Idle:
                    SetIdle();
                    break;
            }
        }

        private void SetIdle()
        {
            _animator.IsIdle(true);
            _animator.IsMoving(false);
        }

        private void SetAttacking()
        {
            _animator.IsMoving(false);
            _animator.IsIdle(false);
        }

        private void SetMoving()
        {
            _animator.IsMoving(true);
            _animator.IsIdle(false);
        }

        private void SetDead()
        {
            _animator.IsDead(true);
            _animator.IsMoving(false);
            _animator.IsIdle(false);
            _enemyMovement.Stop();
        }
    }
    public enum States
    {
        Idle,
        Moving,
        Attack,
        Dead
    }
}


