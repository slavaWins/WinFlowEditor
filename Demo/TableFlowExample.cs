using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WinFlowEditor;


public class TableFlowExample : TableWinFlowEditorBase<WinFlowExampleData>
{



    [MenuItem("WinFlow/Table Base")]
    public static void Open()
    {

        TableFlowExample wnd = GetWindow<TableFlowExample>();
        wnd.titleContent = new GUIContent("Пример таблицы");
    }


    public override List<WinFlowExampleData> GetListItems()
    {
        return Resources.LoadAll<WinFlowExampleData>("").ToList<WinFlowExampleData>();
    }



}