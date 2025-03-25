using UnityEngine;

namespace BoardsRegulation
{
    [RequireComponent(typeof(RectTransform))]
    public class Background : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}
