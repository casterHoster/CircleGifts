using UnityEngine;

namespace Gifts
{
    [CreateAssetMenu(fileName = "New gift filling", menuName = "Gift/Create gift filling", order = 51)]
    public class GiftCharacteristics : ScriptableObject
    {
        [SerializeField] private int _value;
        [SerializeField] private Sprite _sprite;

        public int Value => _value;
        public Sprite Sprite => _sprite;
    }
}
