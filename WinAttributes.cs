using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.UIElements;
using UnityEngine; 
using UnityEngine.UIElements;

namespace WinFlowEditor
{

    [AttributeUsage(AttributeTargets.Method)]
    public class CustomTab : Attribute
    {
        private string v;

        public CustomTab(string v)
        {
            this.v = v;
        }
    }

}