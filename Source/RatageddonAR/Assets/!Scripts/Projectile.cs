using UnityEngine;

public class Projectile : MonoBehaviour, IIngredient, IInteractable
{
    public int Damage => _damage;
    [SerializeField] private int _damage;
    public int ShotStrength => _shotStrength;
    [SerializeField] private int _shotStrength;
    public float ExplosionRadius => _explosionRadius;
    [SerializeField] private float _explosionRadius;

    public void Interact(Player player)
    {
        
    }
}

public interface IIngredient
{

}