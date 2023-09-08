
![img_2.png](info/img_2.png)

WinFlowEditor - это плагин для Unity, который позволяет создавать табличные базы данных для поиска и редактирования предметов в игре. Плагин предоставляет классы, на основе которых можно создать удобный редактор игрового контента.


 
## Возможности

WinFlowEditor предоставляет два варианта редактора:

1. **Табличный редактор**: отображает данные только для одного класса в виде таблицы. Этот режим позволяет легко и удобно просматривать и редактировать данные предметов.

![Табличный редактор](info/img_1.png)


2. **Редактор с вкладками**: позволяет настраивать вкладки и выводить связанные компоненты класса. В этом режиме можно отобразить связи между разными компонентами предмета для более гибкого взаимодействия с данными.
 
![Редактор с вкладками](info/img.png)

## Установка

1. Скачайте последнюю версию WinFlowEditor из репозитория.
2. Вставьте в папку проекта, в папку Plugin. Если нет просто создайте её.
3. Готово! Теперь вы можете использовать WinFlowEditor в своем проекте.

## Использование

Посмотрите на демо:
````

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

````

## Contributing

Все вкладыши могут использоваться для работы с таблицей. Работает автоматически
