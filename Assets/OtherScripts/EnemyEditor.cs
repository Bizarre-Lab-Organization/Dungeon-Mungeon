using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Pathfinding;

namespace DungeonMungeon
{
    [InitializeOnLoad]
    [CustomEditor(typeof(EnemyManager))]

    public class EnemyEditor : Editor
    {
        private static EnemyManager enemyManager;
        private static EnemyWalking enemyWalking;
        private static EnemyRanged enemyRanged;
        private static Seeker seeker;
        private static EnemyTrigger enemyTrigger;
        private static EnemyPassive enemyPassive;

        private static bool foldRanged = true;
        private static bool foldHostile = true;
        // Start is called before the first frame update

        private void OnEnable() {
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

            if (enemyManager.GetComponent<EnemyWalking>() == null)
            {
                enemyWalking = enemyManager.gameObject.AddComponent<EnemyWalking>();
                enemyWalking.enabled = false;
                enemyWalking.hideFlags = HideFlags.HideInInspector;
            }
            else
            {
                enemyWalking = enemyManager.GetComponent<EnemyWalking>();
            }

            Repaint();
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            if (enemyManager.Passive)
            {
                if (enemyManager.GetComponent<EnemyPassive>() == null)
                {
                    enemyPassive = enemyManager.gameObject.AddComponent<EnemyPassive>();
                    enemyPassive.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    enemyPassive = enemyManager.GetComponent<EnemyPassive>();
                }

                enemyTrigger = enemyManager.GetComponent<EnemyTrigger>();
                DestroyImmediate(enemyTrigger);

                enemyTrigger = null;
            }
            else
            {
                enemyPassive = enemyManager.GetComponent<EnemyPassive>();
                DestroyImmediate(enemyPassive);

                enemyTrigger = null;

                if (enemyManager.GetComponent<EnemyTrigger>() == null)
                {
                    enemyTrigger = enemyManager.gameObject.AddComponent<EnemyTrigger>();
                    enemyTrigger.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    enemyTrigger = enemyManager.GetComponent<EnemyTrigger>();
                }
            }

            if (enemyManager.Ranged)
            {
                enemyWalking = enemyManager.GetComponent<EnemyWalking>();
                DestroyImmediate(enemyWalking);

                enemyWalking = null;

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
                DestroyImmediate(enemyWalking);
                DestroyImmediate(seeker);
                DestroyImmediate(enemyTrigger);
                DestroyImmediate(enemyPassive);
                DestroyImmediate(enemyRanged);
            }

            enemyManager = null;
            enemyWalking = null;
            seeker = null;
            enemyTrigger = null;
            enemyPassive = null;
            enemyRanged = null;

            Repaint();
        }

    }
}
