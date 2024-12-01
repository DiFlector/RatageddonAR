/*
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public GameObject deathEffect;

        [SerializeField] private EnemyAnimator _enemyAnimator;
        private int _health = 4;
        //private int _armor;
        public int _damage = 1;
        public float _maxAttackRange = 3f;
        public float _viewDistance { get; private set; } = 10000;
        public float _attackTimeThreshold = 2f;
        [HideInInspector] public float Speed {get; private set;}
        private AudioClip _response1;
        [SerializeField] private float _attackTimer;
        private EnemySpawner _enemySpawner;

        private void Start()
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void Update()
        {
            _attackTimer += Time.deltaTime;
        }

        public void Attack()
        {
            
            if (_attackTimer < _attackTimeThreshold) return;
            //Player.Instance.ApplyDamage();
            if (Random.Range(0, 2) == 0)
            {
                _enemyAnimator.PlayAttack();
            }
            else
            {
                _enemyAnimator.PlayAttack2();
            }
            _attackTimer = 0;
            print("Attack");
        }
        
        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        private void Death()
        {
            _enemySpawner._enemiesCount--;
            StartCoroutine(DeathEffectActivate());
        }

        public void SetResponse()
        {
            //gameObject.GetComponent(AudioClip) = _response1;
        }

        public void PlayResponse()
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        public void ApplyDamage(int damage)
        {
            _health-= damage;
            if (_health > 0) return;
            _health = 0;
            Death();
        }

        private IEnumerator DeathEffectActivate()
        {
            deathEffect.GetComponent<ParticleSystem>().Play();
            gameObject.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        
    }
}
*/
