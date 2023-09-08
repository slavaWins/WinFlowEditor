using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace WinFlowEditor
{
    public class TableWinFlowEditorBase<T> : EditorWindow
        where T : MonoBehaviour, ITemWinFlowSelector, new()
    {


        VisualElement root;

        
        int currentPickerWindow;


        public virtual void Init()
        {

        } 

        public virtual List<T> GetListItems()
        {
            return new List<T>();
        } 


        public string currentSelectedTab;


        public void RenderTopBar()
        {
            VisualElement element = new VisualElement();
            element.style.flexDirection = FlexDirection.Row;
            element.style.paddingLeft = 8;
            element.style.paddingTop = 7;
            element.style.height= 65;

            element.style.paddingBottom= 7;
            element.style.backgroundColor= new Color(1,1,1,0.1f);

            var t = new TextField();
            t.style.fontSize = 15;
            t.style.minWidth= 120;
            //t.style.minHeight = 35; 

            element.Add(new Label("Найти").Padding(3));
            element.Add(t);


            root.Add(element);
        }


        public void RenderHeaderRow()
        {

            List<T> allObjectGuids = GetListItems();
            var m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/UnknownIcon.png", typeof(Sprite));

            T item = allObjectGuids.FirstOrDefault();

            VisualElement element = new VisualElement();
            element.style.flexDirection = FlexDirection.Row;
            element.style.backgroundColor = new Color(1, 1, 1, 0.08f);


            VisualElement colImg = new VisualElement().MinWidth(34);
            colImg.name = "ignore";


            VisualElement colText = new VisualElement();


            colText.style.paddingLeft = 5;
            element.Add(colImg);
            element.Add(colText.MinWidth(113));


            var spriteImage = new Image();
            spriteImage.scaleMode = ScaleMode.ScaleToFit;
            spriteImage.sprite = m_DefaultItemIcon;
            spriteImage.style.height = 32;
            spriteImage.style.width = 32;


            colImg.Add(spriteImage);
            colText.Add(new Label("Название"));


            //colText.AddManipulator(new Clickable(evt => Selection.activeObject = item)); 

            sccrol.Add(element);
             



            SerializedObject serializedCard = new SerializedObject(item);
            SerializedProperty cardProperty = serializedCard.GetIterator();
            cardProperty.Next(true);
            while (cardProperty.NextVisible(false))
            {

                if (cardProperty.name == "m_Script") continue;

                var myl = new Label(cardProperty.name);
                
                if (cardProperty.type == "bool") myl.name = typeof(Toggle).Name;
                if (cardProperty.type.IndexOf("PPtr<$") > -1) myl.name = typeof(ObjectField).Name;
                myl.tooltip = cardProperty.name;
                
                myl.style.marginRight= 6;
                element.Add(myl); 

                
            }


            foreach (VisualElement child in element.Children())
            {
                if (child.name == "ignore") continue;
                child.style.width = 120;
                if (child.name == typeof(Toggle).Name) child.style.width = 30;
                if (child.name == typeof(ObjectField).Name) child.style.width = 160;


            }
        }

        public void RenderRows()
        {

            List<T> allObjectGuids = GetListItems();
            var m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/UnknownIcon.png", typeof(Sprite));

            foreach (var item in allObjectGuids)
            {
                VisualElement element = new VisualElement();
                element.style.height = 30;
                element.style.flexDirection = FlexDirection.Row;
                

                VisualElement colImg = new VisualElement();
                colImg.name = "ignore"; 


                VisualElement colText = new VisualElement(); 


                colText.style.paddingLeft = 5;
                element.Add(colImg);
                element.Add(colText.MinWidth(110));


                var spriteImage = new Image();
                spriteImage.scaleMode = ScaleMode.ScaleToFit;
                spriteImage.sprite = item.GetSpritePreview();
                spriteImage.style.height = 32;
                spriteImage.style.width = 32;


                colImg.style.minWidth = 32;


                colImg.Add(spriteImage);
                colText.Add(new Label(item.GetNamePreview()));
                colText.Add(new Label(item.name));


                colText.AddManipulator(new Clickable(evt => Selection.activeObject = item));



                sccrol.Add(element);

                BuilderWFE.GenerateBoxProperty(item, element, true);

                foreach (VisualElement child in element.Children())
                {
                    if (child.name == "ignore") continue;
                    child.style.width = 120;
                    if (child.GetType() == typeof(Toggle)) child.style.width = 30;
                    if (child.GetType() == typeof(ObjectField)) child.style.width = 160; 
                }
            }
        }
        ScrollView sccrol;

        public void CreateGUI()
        { 

            Init();

            root = rootVisualElement; 

            //root.Padding(0);

            RenderTopBar(); 




            sccrol = new ScrollView();
          //  sccrol.style.flexDirection = FlexDirection.Row;
           // sccrol.style.flexWrap = Wrap.Wrap;
            rootVisualElement.Add(sccrol);


            RenderHeaderRow();

            RenderRows();
            RenderRows();
            RenderRows();
            RenderRows(); 
            RenderRows();
            RenderRows();
            RenderRows();  
        }



        public T currentSelectedItem; 
         
    }
}