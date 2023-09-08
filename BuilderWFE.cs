using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

public class BuilderWFE 
{



    public static VisualElement GenerateBoxProperty(UnityEngine.Object selectedGameObject, VisualElement box, bool isTableMode= false)
    {


        SerializedObject serializedCard = new SerializedObject(selectedGameObject);


        int i = 0;
        SerializedProperty cardProperty = serializedCard.GetIterator();
        cardProperty.Next(true);
        while (cardProperty.NextVisible(false))
        {

            if (cardProperty.name == "m_Script") continue;

            PropertyField prop = new PropertyField(cardProperty);


            IStyle styleInput = null;

            if (cardProperty.type == "string"   )
            {

                var t = new TextField(cardProperty.name);

                 

                styleInput = t.style;
                t.BindProperty(cardProperty);
                box.Add(t);
                prop.Bind(serializedCard);

                if (isTableMode) t.label = null;
            }

            else if (  cardProperty.type == "int"  )
            { 
                var t = new IntegerField(cardProperty.name);  
                styleInput = t.style;
                t.BindProperty(cardProperty);
                box.Add(t);
                prop.Bind(serializedCard);

                if (isTableMode) t.label = null;
            }

           else if (  cardProperty.type == "float")
            {
                var t = new FloatField(cardProperty.name);  
                styleInput = t.style;
                t.BindProperty(cardProperty);
                box.Add(t);
                prop.Bind(serializedCard);

                if (isTableMode) t.label = null;
            }


            else if (cardProperty.type == "Enum")
            {
                var t2 = new UnityEngine.UIElements.DropdownField(cardProperty.name);
                t2.choices.Clear();
                t2.choices = cardProperty.enumDisplayNames.ToList();

                //t2.label = cardProperty.name;
                t2.BindProperty(cardProperty);
                box.Add(t2);
                prop.Bind(serializedCard);
                styleInput = t2.style;

                if (isTableMode) t2.label = null;
            }
            else if (cardProperty.type == "bool")
            {
                var t = new Toggle(cardProperty.name);
                t.BindProperty(cardProperty);
                box.Add(t);
                prop.Bind(serializedCard);
                styleInput = t.style;
                if (isTableMode) t.label = null;
            }
            else if (cardProperty.type.IndexOf("PPtr<$") > -1)
            {

                var t = new ObjectField(cardProperty.name);
                t.objectType = GetTypeByProperty(cardProperty);
                t.BindProperty(cardProperty);
                box.Add(t);
                prop.Bind(serializedCard);
                styleInput = t.style;
                if (isTableMode) t.label = null;
            }
            else
            {

                Debug.Log(cardProperty.name + " : " + cardProperty.type);
            }



            if (styleInput != null)
            {
                if (!isTableMode)
                {
                    styleInput.marginBottom = 10;
                    styleInput.unityFontStyleAndWeight = FontStyle.Normal;
                    styleInput.fontSize = 13;

                    
                    styleInput.unityTextAlign = TextAnchor.MiddleCenter;
                    styleInput.unityBackgroundImageTintColor = new Color(0.8f, 0.8f, 0.8f);
                    styleInput.unityBackgroundScaleMode = ScaleMode.StretchToFill;
                }
            }


        }


        return box;
    }



    public static System.Type GetTypeByProperty(SerializedProperty property)
    {
        System.Type parentType = property.serializedObject.targetObject.GetType();
        System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
        return fi.FieldType;
    }

}
