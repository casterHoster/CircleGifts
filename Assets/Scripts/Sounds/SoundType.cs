using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "New sound type", menuName = "Sound Type/Create sound type", order = 52)]
    public class SoundType : ScriptableObject
    {
        [SerializeField] string _type;

        public string Type => _type;
    }
}
