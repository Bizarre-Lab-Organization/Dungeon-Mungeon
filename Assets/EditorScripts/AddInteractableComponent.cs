using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DungeonMungeon 
{
    [InitializeOnLoad]
    class AddInteractableComponent : Editor
    {
        static int lastLayer;
        static int currentLayer;
        static Transform go; 
        static Transform prevgo;
        static AddInteractableComponent()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            
            prevgo = go;
            try
            {
                lastLayer = currentLayer;
            }
            catch { };
            go = Selection.activeTransform;
            currentLayer = go.gameObject.layer;

            if(go == null)
            {
                return;
            }
            if (lastLayer != currentLayer)
            {
                if (go.gameObject.layer == 7 && go.GetComponent<interactableOptions>() == null)
                {
                    go.gameObject.AddComponent<interactableOptions>();
                }
                else if (go.gameObject.layer != 7 && go.GetComponent<interactableOptions>() != null)
                {
                    DestroyImmediate(go.GetComponent<interactableOptions>());
                }
            }

/*            prevgo = go;
            lastLayer = go.gameObject.layer;
            go = Selection.activeGameObject.transform;
            Debug.Log(go.gameObject.layer + " " + prevgo.gameObject.layer);
            if (!prevgo.Equals(go))
            {
                lastLayer = 0;
            }

            if(lastLayer != go.gameObject.layer)
            {
                if (go.gameObject.layer == 7 && go.GetComponent<interactableOptions>() == null)
                {
                    go.gameObject.AddComponent<interactableOptions>();
                }
                else if (go.gameObject.layer != 7 && go.GetComponent<interactableOptions>() != null)
                {
                    DestroyImmediate(go.GetComponent<interactableOptions>());
                }
            }*/
        }
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DungeonMungeon
{
    [CustomEditor()]
    public class AddInteractableComponent : Editor
    {
        // Start is called before the first frame update
        void OnEnable()
        {

            foreach(GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                Debug.Log(go);
                if(go.layer == LayerMask.NameToLayer("Interactables"))
                {
                    go.AddComponent<interactableOptions>();
                    
                }
            }
        }

     *//*   List<GameObject> GetAllObjectsInScene()
        {
            List<GameObject> objectsInScene = new List<GameObject>();
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                    continue;
                if (!EditorUtility.IsPersistent(go.transform.root.gameObject))
                    continue;
                objectsInScene.Add(go);
            }
            return objectsInScene;
        }*//*
        
    }
}
*/