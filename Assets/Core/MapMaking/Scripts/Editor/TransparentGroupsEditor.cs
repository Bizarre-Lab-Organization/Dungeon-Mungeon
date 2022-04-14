using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace DungeonMungeon
{
    [CustomEditor(typeof(TransparentGroups))]
    public class TransparentGroupsEditor : Editor
    {
        private static TransparentGroups transparentGroup;
        
        private void OnEnable()
        {
            transparentGroup = (TransparentGroups)target;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var list = serializedObject.FindProperty("TransparentGroup");
            //transparentGroup.TransparentGroup = EditorGUILayout.PropertyField(list , new GUIContent("My List Test"), true);
        }
        
        public void OnDestroy()
        {
            transparentGroup = null;
        }
    }
}
