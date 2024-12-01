/*using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyStatesEnum _currentState;
        [SerializeField] private Transform _moveTarget;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        //private Patrolling.Patrolling _patrolling;
        private EnemyBase _enemyBase;
        private NavMeshAgent _agent;
        private Transform _playerPosition;
        private EnemyController _enemyController;
        private Transform home;
        private bool _patrollingEnabled = false;
        public bool isInForest;

        private void Start()
        {
            //_patrolling = GetComponent<Patrolling.Patrolling>();
            _enemyBase = GetComponent<EnemyBase>();
            _agent = GetComponent<NavMeshAgent>();
            home = GameObject.Find("Home").transform;
            _currentState = EnemyStatesEnum.Idle;
            TryGetPlayerPosition();
        }

        private void Update()
        {
            if (_currentState == EnemyStatesEnum.Attacking)
            {
                _enemyBase.Attack();
            }
            StateSwitcher();
            Navigate();
            /*print("player distance"+ToPlayerDistance());
            print("navmesh distance" + _agent.remainingDistance);#1#
        }

        private bool TryGetPlayerPosition()
        {
            //if(!Player.Instance) return false;
            if (ToPlayerDistance() < _enemyBase._viewDistance)
            {
            //    _playerPosition = Player.Instance.transform;
                return true;
            }

            return false;
        }

        private void Navigate()
        {
            ChooseTarget();
            if (!_moveTarget) return;
            _agent.destination = _moveTarget.position;
            var look = new Vector3(_moveTarget.position.x, transform.position.y, _moveTarget.position.z);
            _agent.transform.LookAt( look );
        }

        private void Awake()
        {
            OnStateChange.AddListener(States);
        }
        
        public UnityEvent OnStateChange;


        private void ChooseTarget()
        {
            if (isInForest)
            {
                _moveTarget = _playerPosition;
                return;
            }
           // if(Player.Instance._isHomeZone)
            {
                _moveTarget = ToPlayerDistance() > Vector3.Distance(transform.position, home.position) + 2f ? home : _playerPosition;
            }
            //else
            {
                _moveTarget = home;
            }
        }

        

        private void States()
        {
            switch (_currentState)
            {
                case EnemyStatesEnum.Idle:
                    _enemyAnimator.IsIdle(true);
                    _enemyAnimator.IsMoving(false);
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    //if (_patrollingEnabled) _patrolling._isPatrolling = false;
                    break;
                case EnemyStatesEnum.Patrolling:
                    _agent.speed = 0f;
                    _agent.isStopped = false;
                   // if (_patrollingEnabled) _patrolling._isPatrolling = true;
                  //  _moveTarget = _patrolling.FindClosestWaypoint(_moveTarget);
                    break;
                case EnemyStatesEnum.Following:
                    _enemyAnimator.IsIdle(false);
                    _enemyAnimator.IsMoving(true);
                    _agent.speed = 4f;
                    _agent.isStopped = false;
                  //  if (_patrollingEnabled) _patrolling._isPatrolling = false;
                    //_moveTarget = _playerPosition;
                    break;
                case EnemyStatesEnum.Attacking:
                    _enemyAnimator.IsIdle(true);
                    _enemyAnimator.IsMoving(false);
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                   // if (_patrollingEnabled) _patrolling._isPatrolling = false;
                    //_moveTarget = _playerPosition;
                    break;
                case EnemyStatesEnum.Dead:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    _enemyAnimator.IsDead(true);
                    break;
            }
        }

        private void StateSwitcher()
        {
            if (!_moveTarget)
            {
                print("No target");
                return;
            };
            if (_agent.remainingDistance <= _enemyBase._maxAttackRange && TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Attacking)
            {
                _currentState = EnemyStatesEnum.Attacking;
                OnStateChange?.Invoke();
            }
            else if(_agent.remainingDistance > _enemyBase._maxAttackRange && TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Following)
            {
                _currentState = EnemyStatesEnum.Following;
                OnStateChange?.Invoke();
            }
            else if(!TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Patrolling && _patrollingEnabled)
            {
                _currentState = EnemyStatesEnum.Patrolling;
                OnStateChange?.Invoke();
            }
        }

        private float ToPlayerDistance()
        {
            return 1; // Vector3.Distance(transform.position, Player.Instance.transform.position);
        }
    }
}*/