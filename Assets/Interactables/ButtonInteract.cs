using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class ButtonInteract : MonoBehaviour
    {
        [SerializeField] private GameObject ButtonTarget;
        private void OnEnable()
        {
            InteractableOptions.Execute += Push;
        }
        private void OnDisable()
        {
            InteractableOptions.Execute -= Push;
        }
        private void Push(Transform target)
        {
            if(target.gameObject == gameObject)
            {
                Debug.Log("Pushed " + gameObject);
            } 
        }
    }
}
