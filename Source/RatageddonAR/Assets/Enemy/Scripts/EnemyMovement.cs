using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private EnemyStates _enemyStates;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _enemyStates = GetComponent<EnemyStates>();
            //_agent.destination = FindAnyObjectByType<Kitchen>().transform.position;
            _agent.destination = GameObject.Find("point").transform.position;
        }

        private void Update()
        {
            SetMovement();
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        private void SetMovement()
        {
            if(_enemyStates.CurrentState == States.Dead) return;
            _agent.isStopped = CheckDistance();
            if (!_agent.isStopped)
            {
                if(_enemyStates.CurrentState == States.Moving) return;
                _enemyStates.CurrentState = States.Moving;
            }
            else
            {
                if(_enemyStates.CurrentState == States.Idle) return;
                //_enemyStates.CurrentState = States.Idle;
            }
        }

        private bool CheckDistance()
        {
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }
    }
}
