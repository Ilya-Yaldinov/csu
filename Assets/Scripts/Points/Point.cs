using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Point
{
    private protected Vector3 position;
    private protected Action<Point> buttonEvent;

    public GameObject curButton {get; private protected set;}
    public PointProperties curProperties {get; private protected set;}

    public Point(Vector3 position, Action<Point> action)
    {
        this.position = position;
        this.buttonEvent = action;
        this.curProperties = new PointProperties(this.position.x, this.position.y);
    }

    public Point(PointProperties pointProperties, Action<Point> action)
    {
        this.curProperties = pointProperties;
        this.position = new Vector3(curProperties.X, curProperties.Y);
        this.buttonEvent = action;
    }

    public abstract GameObject CreatePoint();

    private protected void AddEventListener(GameObject button)
    {
        button.AddComponent(typeof(EventTrigger));
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener( (eventData) => { 
            buttonEvent(this);
        });
        trigger.triggers.Add(entry);
    }
}

