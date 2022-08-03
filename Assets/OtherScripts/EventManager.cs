using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class EventManager : MonoBehaviour
    {
        public delegate void InteractableEvent(Transform target);
        public delegate void SetSpeed(float speed);
        public delegate void DamageTransfer(int damage);    }
}
