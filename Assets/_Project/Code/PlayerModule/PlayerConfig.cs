using MAEngine.Extention;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SO/Config/Player/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private SerializableDictionary<ProductID, int> _storableProducts;
        
        public float Speed => _speed;
        public SerializableDictionary<ProductID, int> StorableProducts => _storableProducts;
    }
}