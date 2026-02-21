<img width="800" alt="Renga Inspector plugin" src="https://github.com/user-attachments/assets/3a700fdb-d7d7-4650-91b3-a0fd49d65015" />

# О плагине

Плагин Inspector для BIM системы Renga разработан для более быстрой и легкой разработки программ и приложений, используя открытый API. Фактически данный плагин является аналогом всем известных плагинов для AutoCAD [ARXDBG и MGDDBG](https://adn-cis.org/forum/index.php?topic=7274.0), или [RevitLookup](https://github.com/lookup-foundation/RevitLookup) для Revit, или [Tekla Lookup](http://github.com/karpovichpv/lookup) для TeklaStructures. 

Код построен на рефлексии. Хоть API модель Renda и довольно скудна на свойства, методы, часть свойств, методов обрабатывается в коде отдельно. К ним относятся:

* IBeamParams
* IColumnParams
* IEntityCollection
* ILayerCollection
* IMaterial через Id
* IParameterContainer
* IParameterContainer
* IPlacement3DCollection
* IPolyCurve2D
* IPropertyContainer
* IQuantityContainer
* и т.д.

С выходом новых версий данный список будет только расширяться.

# Установка

Содержимое архива распаковать в папку `C:\Program Files\Renga Standard\Plugins\Inspector`

# Работа с плагином

1. Левой кнопкой мыши нажимаем на кнопку плагина на верхней панели
2. Далее должна отобразиться форма плагина
    - если ничего не было выбрано - отобразяться свойства объекта IProject
    - если были выбраны объекты - в левой панели будут отображены имена объектов, в правой свойства первого объекта
3. На строки в правой панели, которые имеют жирный шрифт, можно нажимать 2 щелчком левой кнопки - откроется новый экземпляр плагина с загруженными свойствами дочернего объекта

# Возможные проблемы

1. Если плагин не грузиться, следует проверить AecApp.log

# Примеры использования

![3](https://github.com/user-attachments/assets/b0258126-4288-499b-b662-b99fa8ad2f3f)
![2](https://github.com/user-attachments/assets/eb58a722-5216-4bc8-b022-c72685c8c234)
![1](https://github.com/user-attachments/assets/f055c61d-ff2b-41e2-9378-7ff5ef7e1097)

3. Если плагин вдруг вылетел - в папке `C:\Program Files\Renga Standard\Plugins\Inspector` должен быть crash_log с текстом исключения
4. Объект IModel пока грузиться долго. Чем больше модель тем больше объектов надо получить. Чуть позже сделаю этот процесс более плавным и предсказуемым
