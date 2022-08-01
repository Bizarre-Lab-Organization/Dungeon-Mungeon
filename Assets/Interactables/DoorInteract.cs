using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class DoorInteract : MonoBehaviour
    {
        private bool state = false; //false == closed; true == opened
        private void OnEnable()
        {
            InteractableOptions.Execute += DoorStuff;
        }
        private void OnDisable()
        {
            InteractableOptions.Execute -= DoorStuff;
        }
        private void DoorStuff(Transform target)
        {
            if (target.gameObject == gameObject)
            {
                if (state)
                {
                    Close();
                }
                else Open();
            }
        }
        private void Open()
        {
            Debug.Log("Opened " + gameObject);
            state = true;
        }
        private void Close()
        {
            Debug.Log("Closed " + gameObject);
            state = false;
        }
    }
}
