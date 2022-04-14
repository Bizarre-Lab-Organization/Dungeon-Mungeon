using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class TransparentGroups : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<GameObject> transparentGroup;

        public List<GameObject> TransparentGroup
        {
            get { return transparentGroup; }
            //set { transparentGroup = value; }
            set { transparentGroup = new List<GameObject>(value); }
        }

        public void AddToGroup(GameObject gObject)
        {
            transparentGroup.Add(gObject);
        }

        public List<GameObject> GetGroup()
        {
            return transparentGroup;
        }

        public void SetList(List<GameObject> list)
        {
            transparentGroup = new List<GameObject>(list);
        }

        public void ClearList()
        {
            transparentGroup.Clear();
        }
    }
}
