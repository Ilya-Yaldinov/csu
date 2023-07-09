using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : Point
{
    public Ladder(Vector3 position, Action<Point> action) : base(position, action) 
    { 
        this.curProperties.PointType = 3;
    }

    public Ladder(PointProperties pointProperties, Action<Point> action) : base(pointProperties, action) { }

    public override GameObject CreatePoint()
    {
        curButton = new GameObject("Ladder Point"); // Создаем новый объект кнопки

        // Добавляем компоненты RectTransform и Button  
        RectTransform rectTransform = curButton.AddComponent<RectTransform>();
        Button button = curButton.AddComponent<Button>();

        // Устанавливаем размер кнопки
        rectTransform.sizeDelta = new Vector2(20f, 20f);

        // Устанавливаем позицию кнопки
        rectTransform.anchoredPosition = position;

        // Устанавливаем цвет фона кнопки
        Image buttonImage = curButton.AddComponent<Image>();
        buttonImage.color = Color.green;

        AddEventListener(curButton);

        return curButton;
    }
}
