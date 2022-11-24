/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.Attributes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    [InitializeOnLoad]
    public static class RuntimeSaveButton
    {
        private static Dictionary<int, string> savedComponents = new Dictionary<int, string>();

        static RuntimeSaveButton()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        [ComponentHeaderButton]
        public static bool Draw(Rect rectangle, Object[] targets)
        {
            Object target = targets[0];
            if (!Validate(target)) return false;

            if (GUI.Button(rectangle, EditorIconContents.saveActive, Styles.iconButton))
            {
                string backup = GUIUtility.systemCopyBuffer;
                Component c = target as Component;
                ComponentUtility.CopyComponent(c);
                int id = target.GetInstanceID();
                savedComponents[id] = GUIUtility.systemCopyBuffer;
                GUIUtility.systemCopyBuffer = backup;

                Debug.Log($"{c.gameObject.name}/{ObjectNames.NicifyVariableName(target.GetType().Name)} component state saved.");
            }

            return true;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode) savedComponents.Clear();
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                Undo.SetCurrentGroupName("Set saved state");
                int group = Undo.GetCurrentGroup();

                string backup = GUIUtility.systemCopyBuffer;

                foreach (KeyValuePair<int, string> pair in savedComponents)
                {
                    Object obj = EditorUtility.InstanceIDToObject(pair.Key);
                    if (obj != null)
                    {
                        Undo.RecordObject(obj, "Set saved state");
                        GUIUtility.systemCopyBuffer = pair.Value;
                        ComponentUtility.PasteComponentValues(obj as Component);
                        EditorUtility.SetDirty(obj);
                    }
                }

                Undo.CollapseUndoOperations(group);
                GUIUtility.systemCopyBuffer = backup;
            }
        }

        private static bool Validate(Object target)
        {
            if (!Prefs.componentExtraHeaderButtons || !Prefs.saveComponentRuntime) return false;
            if (!EditorApplication.isPlaying) return false;
            if (!(target is Component)) return false;
            return true;
        }
    }
}
