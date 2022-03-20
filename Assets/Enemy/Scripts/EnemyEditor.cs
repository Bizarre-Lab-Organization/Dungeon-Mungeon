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
        private static EnemyMelee enemyMelee;
        private static EnemyRanged enemyRanged;
        private static Seeker seeker;
        private static EnemyRange enemyRange;
        private static EnemyTrigger enemyTrigger;

        private static bool foldRanged = true;
        private static bool foldHostile = true;

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

            if (enemyManager.GetComponent<EnemyMelee>() == null)
            {
                enemyMelee = enemyManager.gameObject.AddComponent<EnemyMelee>();
                enemyMelee.enabled = false;
                enemyMelee.hideFlags = HideFlags.HideInInspector;
            }
            else
            {
                enemyMelee = enemyManager.GetComponent<EnemyMelee>();
            }

            Repaint();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Components

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
            }
            else
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

            if (enemyManager.Ranged)
            {
                enemyMelee = enemyManager.GetComponent<EnemyMelee>();
                DestroyImmediate(enemyMelee);

                enemyMelee = null;

                if (enemyManager.GetComponent<EnemyRanged>() == null)
                {
                    enemyRanged = enemyManager.gameObject.AddComponent<EnemyRanged>();
                    enemyRanged.enabled = false;
                    enemyRanged.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    enemyRanged = enemyManager.GetComponent<EnemyRanged>();
                }
            }

            // Properties

            if (!enemyManager.Passive)
            {
                foldHostile = EditorGUILayout.BeginFoldoutHeaderGroup(foldHostile, "Hostile");
                if (foldHostile)
                {
                    enemyManager.RangeStart = EditorGUILayout.FloatField("Range Start", enemyManager.RangeStart);
                    enemyManager.RangeEnd = EditorGUILayout.FloatField("Range End", enemyManager.RangeEnd);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            if (enemyManager.Ranged)
            {
                foldRanged = EditorGUILayout.BeginFoldoutHeaderGroup(foldRanged, "Ranged");
                if (foldRanged)
                {
                    enemyManager.RangeToStayAt = EditorGUILayout.FloatField("Range", enemyManager.RangeToStayAt);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }
        
        private void OnDestroy()
        {
            if (enemyManager == null)
            {
                DestroyImmediate(enemyMelee);
                DestroyImmediate(seeker);
                DestroyImmediate(enemyTrigger);
                DestroyImmediate(enemyRange);
                DestroyImmediate(enemyRanged);
            }

            enemyManager = null;
            enemyMelee = null;
            seeker = null;
            enemyTrigger = null;
            enemyRange = null;
            enemyRanged = null;

            Repaint();
        }
    }
}
