using UnityEditor;
using UnityEngine;

namespace DungeonMungeon
{
    [CustomEditor(typeof(EnemyManager))]
    public class EnemyEditor : Editor
    {
        private EnemyManager em;

        private void OnEnable()
        {
            em = (EnemyManager)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EnemyManager enemyManager = (EnemyManager)target;

            if (enemyManager.ranged)
            {
                em.range = EditorGUILayout.IntField("Range", 5);
            }
        }
    }
}
