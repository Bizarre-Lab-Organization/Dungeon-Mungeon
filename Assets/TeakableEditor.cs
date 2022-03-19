using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

/// <summary>
/// A simple class to inherit from when only minor tweaks to a component's inspector are required.
/// In such cases, a full custom inspector is normally overkill but, by inheriting from this class, custom tweaks become trivial.
/// 
/// To hide items from being drawn, simply override GetInvisibleInDefaultInspector, returning a string[] of fields to hide.
/// To draw/add extra GUI code/anything else you want before the default inspector is drawn, override OnBeforeDefaultInspector.
/// Similarly, override OnAfterDefaultInspector to draw GUI elements after the default inspector is drawn.
/// </summary>
public abstract class TweakableEditor : Editor
{
    private static readonly string[] _emptyStringArray = new string[1] { "m_Script" };
    private static Dictionary<int, dynamic> _order = new Dictionary<int, dynamic>();

    private static Dictionary<string, string> _type = new Dictionary<string, string>();
    private static Dictionary<string, string> _name = new Dictionary<string, string>();

    public bool passive;
    public bool ranged;

    public int range;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        OnBeforeDefaultInspector();
        DrawPropertiesExcluding(serializedObject, GetInvisibleInDefaultInspector());
        OnAfterDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    protected virtual void OnBeforeDefaultInspector()
    {
        _order.Add(1, _type);
        _order.Add(1, _name);



        _order.Add(2, "ranged");

        passive = EditorGUILayout.Toggle("Passive", passive);
        ranged = EditorGUILayout.Toggle("Ranged", ranged);

        for (int i = 1; i <= _order.Count; i++)
        {
            /*foreach (KeyValuePair<int, string> prop in _order)
            {
                if (prop.Key == i)
                {


                    break;
                }
            }*/

            continue;
        }
    }

    protected virtual void OnAfterDefaultInspector()
    {
        /*if (ranged)
        {
            range = EditorGUILayout.IntField("Range", range);
        }*/
    }

    protected virtual string[] GetInvisibleInDefaultInspector()
    {
        return _emptyStringArray;
    }
}