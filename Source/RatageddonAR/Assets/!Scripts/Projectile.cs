using Enemy.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Projectile : PickableObject, IIngredient, IInteractable
{
    private Player _player;
    public int Damage => _damage;
    [SerializeField] private int _damage;
    public int ShotStrength => _shotStrength;
    [SerializeField] private int _shotStrength;
    public float ExplosionRadius => _explosionRadius;
    [SerializeField] private float _explosionRadius;
    
    private DamageType _damageType = DamageType.Burst;

    public bool IsCooked { get; private set; }

    public void Init(Player player)
    {
        GetComponentInChildren<Collider>().enabled = false;
        _player = player;
    }

    public override void Interact(Player player)
    {
        base.Interact(player);
    }

    public void Cook()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.gray;
        GetComponentInChildren<Collider>().enabled = false;
        IsCooked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && _player.ItemInHand != null)
        {
            if (_player.ItemInHand != this) return;
                Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out IDamageable dmgbl))
                    {
                        dmgbl.GetDamage(_damage, _damageType);
                        Destroy(gameObject);
                    }
                    
                }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(_player);
        if (col.gameObject.CompareTag("castle"))
        {
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.AddExplosionForce(5, transform.position, 0.5f);
                    Destroy(gameObject);
                }
            }
        }
        else if (col.gameObject.GetComponent<ARPlane>() != null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable dmgbl))
                {
                    dmgbl.GetDamage(_damage, _damageType);
                    Destroy(gameObject);
                }
            }
        }
    }
}

public interface IIngredient
{

}