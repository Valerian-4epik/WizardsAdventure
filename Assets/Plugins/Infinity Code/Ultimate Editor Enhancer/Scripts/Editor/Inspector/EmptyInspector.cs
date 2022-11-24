/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace InfinityCode.UltimateEditorEnhancer.InspectorTools
{
    public class EmptyInspector: InspectorInjector
    {
        private const string ELEMENT_NAME = "EmptyInspector";

        private static EmptyInspector instance;
        private static VisualElement visualElement;

        public EmptyInspector()
        {
            EditorApplication.delayCall += InitInspector;
            WindowManager.OnMaximizedChanged += OnMaximizedChanged;
            Selection.selectionChanged += InitInspector;
        }

        private static void CreateButton(VisualElement parent, string submenu, string text)
        {
            ToolbarButton button = new ToolbarButton(() => EditorApplication.ExecuteMenuItem(submenu));
            button.text = text;
            button.style.unityTextAlign = TextAnchor.MiddleCenter;
            button.style.left = 0;
            button.style.borderLeftWidth = button.style.borderRightWidth = 0;
            parent.Add(button);
        }

        private VisualElement CreateContainer(VisualElement parent)
        {
            VisualElement el = new VisualElement();
            el.style.borderBottomWidth = el.style.borderTopWidth = el.style.borderLeftWidth = el.style.borderRightWidth = 1;
            el.style.borderBottomColor = el.style.borderTopColor = el.style.borderLeftColor = el.style.borderRightColor = Color.gray;
            el.style.marginLeft = 3;
            el.style.marginRight = 5;
            parent.Add(el);
            return el;
        }

        private static void CreateLabel(VisualElement parent, string text)
        {
            Label label = new Label(text);
            label.style.marginTop = 10;
            label.style.marginLeft = label.style.marginRight = 3;
            label.style.paddingLeft = 5;
            parent.Add(label);
        }

        [InitializeOnLoadMethod]
        private static void Init()
        {
            instance = new EmptyInspector();
        }

        private VisualElement InitItems()
        {
            VisualElement visualElement = new VisualElement();
            visualElement.name = ELEMENT_NAME;

            Label helpbox = new Label("Nothing selected");
            helpbox.style.backgroundColor = Color.gray;
            helpbox.style.height = 30;
            helpbox.style.unityTextAlign = TextAnchor.MiddleCenter;

            visualElement.Add(helpbox);

            CreateLabel(visualElement, "Settings");
            VisualElement container = CreateContainer(visualElement);
            CreateButton(container, "Edit/Project Settings...", "Project Settings");
            CreateButton(container, "Edit/Preferences...", "Preferences");
            CreateButton(container, "Edit/Shortcuts...", "Shortcuts");

            CreateLabel(visualElement, "Windows");
            container = CreateContainer(visualElement);

            bool skip = true;
            string groupName = "";

            foreach (string submenu in Unsupported.GetSubmenus("Window"))
            {
                string upper = Culture.textInfo.ToUpper(submenu);
                if (skip)
                {
                    if (upper == "WINDOW/PACKAGE MANAGER")
                    {

                    }
                    else if (upper.StartsWith("WINDOW/GENERAL"))
                    {
                        skip = false; 
                    }
                    else continue;
                }

                string[] parts = submenu.Split('/');
                string firstPart = parts[1];

                if (parts.Length == 2)
                {
                    CreateButton(container, submenu, firstPart);
                }
                else if (parts.Length == 3)
                {
                    if (groupName != firstPart)
                    {
                        CreateLabel(visualElement, firstPart);
                        container = CreateContainer(visualElement);
                        groupName = firstPart;
                    }

                    CreateButton(container, submenu, parts[2]);
                }
            }

            return visualElement;
        }

        protected override bool OnInject(EditorWindow wnd, VisualElement mainContainer, VisualElement editorsList)
        {
            if (editorsList.parent[0].name == ELEMENT_NAME) editorsList.parent.RemoveAt(0);
            if (!Prefs.emptyInspector) return false;
            if (editorsList.childCount != 0 || float.IsNaN(editorsList.layout.width)) return false;

            if (visualElement == null) visualElement = InitItems();
            editorsList.parent.Insert(0, visualElement);

            return true;
        }
    }
}