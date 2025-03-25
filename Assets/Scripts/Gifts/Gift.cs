using UnityEngine;

namespace Gifts
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Gift : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public int Value { get; protected set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}