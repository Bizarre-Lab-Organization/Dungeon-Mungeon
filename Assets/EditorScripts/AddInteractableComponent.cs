using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DungeonMungeon 
{
    [InitializeOnLoad]
    [CustomEditor(typeof(InteractableOptions))]
    class InteracatbleOptionsEditor : Editor
    {
        static int lastLayer;
        static int currentLayer;
        static Transform go; 
        static InteracatbleOptionsEditor()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            
            try
            {
                lastLayer = currentLayer;
            go = Selection.activeTransform;
            currentLayer = go.gameObject.layer;
            }
            catch { };

            if(go == null)
            {
                return;
            }
            if (lastLayer != currentLayer)
            {
                if (go.gameObject.layer == 7 && go.GetComponent<InteractableOptions>() == null)
                {
                    go.gameObject.AddComponent<InteractableOptions>();
                }
                else if (go.gameObject.layer != 7 && go.GetComponent<InteractableOptions>() != null)
                {
                    DestroyImmediate(go.GetComponent<InteractableOptions>());
                }
            }
        }
    }
}