using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WinFlowEditor;
using static UnityEditor.Progress;

public class WinFlowExampleData : MonoBehaviour, ITemWinFlowSelector
{

    [SerializeField]
    public Sprite otherSpriteLink;

     
    public string itemName;
    public string otherName;
    public string ind;
    public string miniText;
    public string infoShow;
    public int gameLevel;
    public int hpMax;
    public int deathCount;

    [Header("Adding header text")]
    public string descr;
    public int maxStack;
    public bool isCheckBox = false;

    public ExampleEnumDrop dropDownExample; 


    public Sprite GetSpritePreview()
    {
        return   GetComponent<SpriteRenderer>().sprite; ;
    }

    public string GetNamePreview()
    {
        return itemName;
    }


    [Serializable]
    public enum ExampleEnumDrop
    {
        one,
        two,
        other,
        
    }

}



public class ExampleWinFE : WinFlowEditorBase<WinFlowExampleData>
{



    [MenuItem("WinFlow/Example Base")]
    public static void Open()
    {

        ExampleWinFE wnd = GetWindow<ExampleWinFE>();
        wnd.titleContent = new GUIContent("Пример редактора");
    }

    //
    public override void Init()
    {
        TabAdd(TabTwo);
    }

    public override List<WinFlowExampleData> GetListItems()
    {
        return Resources.LoadAll<WinFlowExampleData>("").ToList<WinFlowExampleData>();
    }


    public void TabTwo(WinFlowExampleData seleted, VisualElement panel)
    {
        panel.Add(new Label("Влкадка кастом 1"));
        //  GenerateBoxProperty(seleted.GetComponent<ItemFlow>(), panel); 
    }



    public override void RenderOpenWindow(WinFlowExampleData seleted, VisualElement panel)
    {
        base.RenderOpenWindow(seleted, panel); 

        BuilderWFE.GenerateBoxProperty(seleted, panel);
    }




}