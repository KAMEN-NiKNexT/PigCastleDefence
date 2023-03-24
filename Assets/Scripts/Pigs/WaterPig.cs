using UnityEngine;

namespace PigCastleDefence
{
    public class WaterPig : Pig
    {
        [Header("Water Variables")]
        [SerializeField] private float _geyserDamage;
        [SerializeField] private float _geyserDuration;
        [SerializeField] private float _geyserForce;
        [SerializeField] private GameObject _geyserPrefab;

        protected override void Attack()
        {
            base.Attack();

            Vector3 geyserPosition = _target.position;
            geyserPosition.y = transform.position.y;
            GameObject geyser = Instantiate(_geyserPrefab, geyserPosition, Quaternion.identity);
        }
    }
}