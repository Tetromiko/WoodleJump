using UnityEditor.Animations;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
    public class ItemData : ScriptableObject
    {
        public Sprite sprite;
        public AnimatorController animatorController;
    }
}