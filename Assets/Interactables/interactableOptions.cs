using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMungeon
{
    public class InteractableOptions : MonoBehaviour
    {
        public delegate void ExecuteInteractable(Transform target);
        public static event ExecuteInteractable Execute;
        public enum Type { Button, Door, Item };
        [SerializeField] private Type _type;

        private void OnEnable()
        {
            PlayerInteract.OnInteract += ExecuteFunctionality;
        }
        private void OnDisable()
        {
            PlayerInteract.OnInteract -= ExecuteFunctionality;
        }
        public Type Typer
        {
            get { return _type; }
            set { _type = value; }
        }
        void ExecuteFunctionality(Transform target)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (target.gameObject == gameObject)
                {
                    if(Execute != null)
                    Execute(target);
                }
            }
        }

    }
}
