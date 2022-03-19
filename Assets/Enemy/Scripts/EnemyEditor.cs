using UnityEditor;
using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

namespace DungeonMungeon
{
    [InitializeOnLoad]
    [CustomEditor(typeof(EnemyManager))]
    public class EnemyEditor : Editor // injects into manager
    {
        private static EnemyManager enemyManager;
        private static EnemyAI enemyAI;
        private static Seeker seeker;
        private static EnemyRange enemyRange;
        private static EnemyTrigger enemyTrigger;

        private static bool foldRanged = true;

        private void OnEnable()
        {
            if (enemyManager != null) return;

            enemyManager = (EnemyManager)target;

            if (enemyManager.GetComponent<Seeker>() == null)
            {
                seeker = enemyManager.gameObject.AddComponent<Seeker>();
                seeker.hideFlags = HideFlags.HideInInspector;
            }
            else
            {
                seeker = enemyManager.GetComponent<Seeker>();
            }

            if (enemyManager.GetComponent<EnemyAI>() == null)
            {
                enemyAI = enemyManager.gameObject.AddComponent<EnemyAI>();
                enemyAI.enabled = false;
                enemyAI.hideFlags = HideFlags.HideInInspector;
            }
            else
            {
                enemyAI = enemyManager.GetComponent<EnemyAI>();
            }

            if (enemyManager.GetComponent<EnemyRange>() == null)
            {
                enemyRange = enemyManager.gameObject.AddComponent<EnemyRange>();
                enemyRange.hideFlags = HideFlags.HideInInspector;
            }
            else
            {
                enemyRange = enemyManager.GetComponent<EnemyRange>();
            }

            Repaint();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (enemyManager.Passive != !enemyAI.enabled)
            {
                enemyAI.enabled = !enemyManager.Passive;

                if (enemyManager.Passive)
                {
                    if (enemyManager.GetComponent<EnemyTrigger>() == null)
                    {
                        enemyTrigger = enemyManager.gameObject.AddComponent<EnemyTrigger>();
                        enemyTrigger.hideFlags = HideFlags.HideInInspector;
                    }
                    else
                    {
                        enemyTrigger = enemyManager.GetComponent<EnemyTrigger>();
                    }

                    enemyRange = enemyManager.GetComponent<EnemyRange>();
                    DestroyImmediate(enemyRange);

                    enemyRange = null;
                } else
                {
                    enemyTrigger = enemyManager.GetComponent<EnemyTrigger>();
                    DestroyImmediate(enemyTrigger);

                    enemyTrigger = null;

                    if (enemyManager.GetComponent<EnemyRange>() == null)
                    {
                        enemyRange = enemyManager.gameObject.AddComponent<EnemyRange>();
                        enemyRange.hideFlags = HideFlags.HideInInspector;
                    }
                    else
                    {
                        enemyRange = enemyManager.GetComponent<EnemyRange>();
                    }
                } 
            }

            if (enemyManager.Ranged)
            {
                foldRanged = EditorGUILayout.BeginFoldoutHeaderGroup(foldRanged, "Ranged");
                
                if (foldRanged)
                {
                    enemyManager.Range = EditorGUILayout.IntField("Range", enemyManager.Range);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }
        
        private void OnDestroy()
        {
            if (enemyManager == null)
            {
                DestroyImmediate(enemyAI);
                DestroyImmediate(seeker);
                DestroyImmediate(enemyTrigger);
                DestroyImmediate(enemyRange);
            }

            enemyManager = null;
            enemyAI = null;
            seeker = null;
            enemyTrigger = null;
            enemyRange = null;

            Repaint();
        }
    }
}
