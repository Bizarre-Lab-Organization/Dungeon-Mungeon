using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DungeonMungeon
{
    [CustomEditor(typeof(TransparentGroups))]
    public class TransparentGroupsEditor : Editor
    {
        //private static List<GameObject> tiles;
        private static TransparentGroups transparentGroups;

        private void OnEnable()
        {
            //tiles = new List<GameObject>(TransparentGroups.GetGroup());
            transparentGroups = (TransparentGroups)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //tiles = TransparentGroups.GetGroup();

            foreach (GameObject tile in transparentGroups.GetGroup())
            {
                /*TransparentGroups tileComp = tile.GetComponent<TransparentGroups>();

                transparentGroups.AddToGroup(transparentGroups.gameObject);

                //tileComp.ClearList();
                tileComp.SetList(transparentGroups.GetGroup());*/
            }
        }

        public void OnDestroy()
        {
            //tiles = null;
            transparentGroups = null;
        }
    }
}
