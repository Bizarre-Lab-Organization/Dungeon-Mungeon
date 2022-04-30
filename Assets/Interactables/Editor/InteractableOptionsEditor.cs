using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DungeonMungeon
{
    [InitializeOnLoad]
    [CustomEditor(typeof(InteractableOptions))]
    class InteractableOptionsEditor : Editor
    {
        static InteractableOptions interactableOptions;
        static InteractableOptions.Type previousType;
        static GameObject interactableoptionsGO;
        static ButtonInteract buttonInteract;
        static ItemInteract itemInteract;
        static DoorInteract doorInteract;

        private void OnEnable()
        {
            interactableOptions = (InteractableOptions)target;
            interactableoptionsGO = interactableOptions.gameObject;
            
        }
        dynamic CheckType(InteractableOptions.Type type)
        {
            if (type == InteractableOptions.Type.Button)
            {
                return interactableoptionsGO.GetComponent<ButtonInteract>();
            }
            if (type == InteractableOptions.Type.Door)
            {
                return interactableoptionsGO.GetComponent<DoorInteract>();
            }
            if (type == InteractableOptions.Type.Item)
            {
                return interactableoptionsGO.GetComponent<ItemInteract>();
            }
            return null;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(interactableOptions.Typer == InteractableOptions.Type.Button)
            {
                if(interactableOptions.GetComponent<ButtonInteract>() == null)
                {
                    buttonInteract = interactableOptions.gameObject.AddComponent<ButtonInteract>();
                }
                else
                {
                    buttonInteract = interactableOptions.GetComponent<ButtonInteract>();
                } 
            }
            else if (interactableOptions.Typer == InteractableOptions.Type.Door)
            {
                if (interactableOptions.GetComponent<DoorInteract>() == null)
                {
                    doorInteract = interactableOptions.gameObject.AddComponent<DoorInteract>();
                }
                else
                {
                    doorInteract = interactableOptions.GetComponent<DoorInteract>();
                }
            }
            else if (interactableOptions.Typer == InteractableOptions.Type.Item)
            {
                if (interactableOptions.GetComponent<ItemInteract>() == null)
                {
                    itemInteract = interactableOptions.gameObject.AddComponent<ItemInteract>();
                }
                else
                {
                    itemInteract = interactableOptions.GetComponent<ItemInteract>();
                }
            }
            if (previousType != interactableOptions.Typer)
            {
                Debug.Log("ches");
                DestroyImmediate(CheckType(previousType));
            }
            previousType = interactableOptions.Typer;
            Repaint();
        }
        private void OnDestroy()
        {
            if(interactableOptions == null)
            {
                Debug.Log("kgoa se vika");
                DestroyImmediate(CheckType(previousType));
            }
        }
    }
}
