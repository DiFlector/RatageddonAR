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
    [SerializeField] private DamageType _damageType;
    [SerializeField] private ParticleSystem _explosion;

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
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out IDamageable dmgbl))
                    {
                        dmgbl.GetDamage(_damage, _damageType);
                        Explode();
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        int damage = 0;
        if (col.gameObject.CompareTag("castle"))
        {
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rb))
                {
                    if (!rb.useGravity) damage++;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.AddExplosionForce(5, transform.position, 0.5f);
                    Explode();
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
                    Explode();
                }
            }
        }
        _player.PlaceObjects.Castle.ApplyDamage(damage);
    }

    private void Explode()
    {
        _explosion.gameObject.SetActive(true);
        _explosion.transform.rotation = Quaternion.identity;
        _explosion.transform.parent = null;
        _explosion.Play();
        Destroy(gameObject);
    }
}

public interface IIngredient
{

}