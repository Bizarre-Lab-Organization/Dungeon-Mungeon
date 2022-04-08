using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonMungeon
{
    public class TransparentGroups : MonoBehaviour
    {
        [SerializeField] private List<GameObject> transparentGroup = new List<GameObject>();

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
