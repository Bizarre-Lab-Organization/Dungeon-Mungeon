using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class RotateAround : MonoBehaviour
    {
        public Transform target;
        public float fRadius = 3.0f;

        void Update()
        {
            Vector3 v3Pos = Camera.main.WorldToScreenPoint(target.position);
            v3Pos = Input.mousePosition - v3Pos;
            float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            v3Pos = Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3.right * fRadius);
            transform.position = target.position + v3Pos;
        }
       /* private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(target.position, fRadius);
        }*/
    }
}


