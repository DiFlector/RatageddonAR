using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Enemy.Scripts
{
    public class Test : MonoBehaviour
    {
        private EnemyStates _enemyStates;
        private EnemyAnimator _enemyAnimator;
        private Enemy _enemy;

        private void Start()
        {
            _enemyStates = FindAnyObjectByType<EnemyStates>();
            _enemyAnimator = FindAnyObjectByType<EnemyAnimator>();
            _enemy = FindAnyObjectByType<Enemy>();
        }

        private void Update()
        {
            Press();
        }

        private void Press()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _enemy.GetDamage(50f);
                //_enemyStates.CurrentState = States.Attack;
                //print(_enemyAnimator.GetAnimationClipDuration("Zombie Attack"));
                //_enemyStates.CurrentState = States.Moving;
            }
        }
    }
}
