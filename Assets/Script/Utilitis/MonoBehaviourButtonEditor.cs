using UnityEditor;
using UnityEngine;

namespace Duriono
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class MonoBehaviourButtonEditor : BaseButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawButtons(target);
        }
    }
}
