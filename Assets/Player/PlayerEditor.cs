using UnityEditor;
using UnityEngine;

namespace DungeonMungeon
{
    [InitializeOnLoad]
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : Editor
    {
        /*private void OnEnable()
        {
            if (StaticPlayer.playerObject != null) return;

            var playerTarger = (Player)target;
            StaticPlayer.playerObject = playerTarger.gameObject;

            Repaint();
        }*/
    }
}
