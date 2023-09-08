using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor; 
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//Version 0.2
namespace WinFlowEditor
{
    public class WinFlowEditorBase<T> : EditorWindow
        where T : MonoBehaviour, ITemWinFlowSelector, new()
    {


        UnityAction<T, VisualElement> tabs;
        VisualElement rightSplit;
        VisualElement root;
        TwoPaneSplitView splitView;
        private VisualElement inspectorPanelRight;
        int currentPickerWindow;


        public virtual void Init()
        {

        }

        public List<Action<T, VisualElement>> tabsList = new List<Action<T, VisualElement>>();


        public void TabAdd(Action<T, VisualElement> tabTwo)
        {
            if (tabsList.Contains(tabTwo)) return;
            tabsList.Add(tabTwo);
        }


        public virtual List<T> GetListItems()
        {
            return new List<T>();
        }

        ListView listViewItems;

        public void RenderLeftCol()
        {
            leftSplit = new VisualElement();
            splitView.Add(leftSplit);


            listViewItems = new ListView();

            listViewItems.StretchToParentSize();
            leftSplit.Add(listViewItems);
            listViewItems.style.paddingTop = 15;

            List<T> allObjectGuids = GetListItems();
            var m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/UnknownIcon.png", typeof(Sprite));

            listViewItems.makeItem = () => new VisualElement();
            listViewItems.bindItem = (element, index) =>
            {

                element.style.flexDirection = FlexDirection.Row;

                VisualElement colImg = new VisualElement();
                VisualElement colText = new VisualElement();


                colText.style.paddingLeft = 5;
                element.Add(colImg);

                element.Add(colText);

                T item = allObjectGuids[index];

                var spriteImage = new Image();
                spriteImage.scaleMode = ScaleMode.ScaleToFit;
                spriteImage.sprite = item.GetSpritePreview();
                spriteImage.style.height = 52;
                spriteImage.style.width = 52;


                colImg.Add(spriteImage);
                colText.Add(new Label(item.GetNamePreview()));
                colText.Add(new Label(item.name));

            };
            listViewItems.itemsSource = allObjectGuids;


            listViewItems.onSelectionChange += OnSelectionFromListChange;


        }


        public string currentSelectedTab;


        ListView listTabView;
        public void RenderTabs()
        {


            tabPanel = new VisualElement();
            tabPanel.style.backgroundColor = new Color(0.61f, 0.61f, 0.61f, 0.1f);
            tabPanel.style.width = Length.Percent(20);



            listTabView = new ListView();
            listTabView.StretchToParentSize();
            tabPanel.Add(listTabView);


            List<string> allObjectGuids = new List<string>() { typeof(T).Name };


            foreach (Action<T, VisualElement> item in tabsList)
            {
                allObjectGuids.Add(item.Method.Name);
            }


            listTabView.makeItem = () => new VisualElement();
            listTabView.bindItem = (element, index) =>
            {

                element.style.flexDirection = FlexDirection.Row;

                VisualElement colText = new VisualElement();
                element.Add(colText);


                string item = allObjectGuids[index];

                colText.Add(new Label(item));
            };
            listTabView.itemsSource = allObjectGuids;

            listTabView.onSelectionChange += TabSelect;
        }


        public void TabSelect(IEnumerable<object> selectedTabs)
        {
            string ind = selectedTabs.First() as string;

            currentSelectedTab = ind;
            inspectorPanelRight.Clear();


            if (currentSelectedItem == null)
            {
                return;
            }

            if (ind == typeof(T).Name)
            {
                RenderOpenWindow(currentSelectedItem, inspectorPanelRight);
                return;
            }


            foreach (Action<T, VisualElement> item in tabsList)
            {
                if (item.Method.Name != ind) continue;

                item(currentSelectedItem, inspectorPanelRight);

                return;
            }


            Debug.Log("Not isset Tab: " + ind);
        }

        VisualElement tabPanel;
        public void RenderRightCol()
        {


            rightSplit = new VisualElement();
            rightSplit.style.flexDirection = FlexDirection.Row;

            splitView.Add(rightSplit);

            RenderTabs();
            rightSplit.Add(tabPanel);


            inspectorPanelRight = new VisualElement();
            inspectorPanelRight.style.paddingLeft = 10;
            inspectorPanelRight.style.width = Length.Percent(80);
            rightSplit.Add(inspectorPanelRight);

            inspectorPanelRight.style.paddingTop = 10;
            listTabView.style.paddingTop = 10;
            listTabView.style.paddingLeft = 10;
            listViewItems.style.paddingTop = 0;

        }

        VisualElement leftSplit;
        public void CreateGUI()
        {

            Init();

            root = rootVisualElement;
            root.Padding(0);


            splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);


            rootVisualElement.Add(splitView);



            RenderLeftCol();


            RenderRightCol();

            if (currentSelectedItem != null)
            {
                int _sel = listViewItems.itemsSource.IndexOf(currentSelectedItem);
                if (_sel != -1)
                {
                    listViewItems.selectedIndex = (_sel);
                    OnSelectionFromListChange(new List<object>() { currentSelectedItem });
                }


                if (currentSelectedTab != null)
                {

                    TabSelect(new List<string>() { currentSelectedTab });
                }

            }

        }



        public T currentSelectedItem;
        public virtual void RenderOpenWindow(T seleted, VisualElement panel)
        {

            panel.Add(new Label("Редактирование объекта"));

        }


        void OnSelectionFromListChange(IEnumerable<object> selectedItems)
        {
            inspectorPanelRight.Clear();


            currentSelectedItem = selectedItems.First() as T;

            if (currentSelectedItem == null)
                return;

            inspectorPanelRight.style.minHeight = 300;
            inspectorPanelRight.style.height = 300;
            inspectorPanelRight.style.borderBottomWidth = 2;
            inspectorPanelRight.style.borderBottomWidth = 2;


            RenderOpenWindow(currentSelectedItem, inspectorPanelRight);


        }
    }
}