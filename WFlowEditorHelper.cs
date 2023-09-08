using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace WinFlowEditor
{


    public interface ITemWinFlowSelector
    {
        public Sprite GetSpritePreview();
        public string GetNamePreview();
    }
     

    public static class WinFlowEditorVisualElementExtend
    {

        public static VisualElement MinWidth(this VisualElement element, int val)
        {
            element.style.minWidth= val;
            return element;
        }

        public static VisualElement Padding(this VisualElement element, int val)
        {
            element.style.paddingBottom = val;
            element.style.paddingLeft = val;
            element.style.paddingRight = val;
            element.style.paddingTop = val;
            return element;
        }
        public static VisualElement MarginBottomPlus(this VisualElement element, int val)
        {
            element.style.marginBottom = element.style.marginBottom.value.value + val;
            return element;
        }
    }

}