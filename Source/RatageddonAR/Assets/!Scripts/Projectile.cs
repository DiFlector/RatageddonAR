using UnityEngine;

public class Projectile : MonoBehaviour, IIngredient
{
    public int Damage => _damage;
    [SerializeField] private int _damage;
    public int ShotStrength => _shotStrength;
    [SerializeField] private int _shotStrength;
    public float ExplosionRadius => _explosionRadius;
    [SerializeField] private float _explosionRadius;
}

public interface IIngredient
{

}