using System;
using UnityEngine;
using UnityEngine.UI;

public class Cabinet : Point
{
    public Cabinet(Vector3 position, Action<Point> action) : base(position, action) 
    { 
        this.curProperties.PointType = 1;
    }

    public Cabinet(PointProperties pointProperties, Action<Point> action) : base(pointProperties, action) { }

    public override GameObject CreatePoint()
    {
        curButton = new GameObject("Cabinet Point"); // Создаем новый объект кнопки

        // Добавляем компоненты RectTransform и Button
        RectTransform rectTransform = curButton.AddComponent<RectTransform>();
        Button button = curButton.AddComponent<Button>();

        // Устанавливаем размер кнопки
        rectTransform.sizeDelta = new Vector2(20f, 20f);

        // Устанавливаем позицию кнопки
        rectTransform.anchoredPosition = position;

        // Устанавливаем цвет фона кнопки
        Image buttonImage = curButton.AddComponent<Image>();
        buttonImage.color = Color.black;

        AddEventListener(curButton);

        return curButton;
    }
}