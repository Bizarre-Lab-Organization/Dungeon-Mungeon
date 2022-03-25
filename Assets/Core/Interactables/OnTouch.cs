using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DungeonMungeon
{
    public class OnTouch : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/testing/GameObject.prefab", typeof(GameObject));
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
