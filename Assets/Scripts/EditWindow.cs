using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

public class EditWindow : MonoBehaviour
{
    [SerializeField] public GameObject PopupCabinet;
    [SerializeField] public GameObject PopupInterest;
    [SerializeField] public GameObject PopupLadder;
    [SerializeField] public GameObject PathFinder;


    
    private bool isCabinetButtonActive = false;
    private bool isInterestButtonActive = false;
    private bool isLadderButtonActive = false;
    private IOFileWork ioFile;
    private Point curPoint;
    private List<Point> points;
    
    void Start()
    {
        points = new List<Point>();
        ioFile = new IOFileWork( @"\file.json");
        StartWithPoints();
        AddPointForUser();
    }

    private void StartWithPoints()
    {
        var propertiesList = ioFile.Read();
        foreach (var properties in propertiesList.Points)
        {
            switch(properties.PointType)
            {
                case 1:
                    AddPointToCanvas(new Cabinet(properties, OpenPopupCabinet));
                    break;
                case 2:
                    AddPointToCanvas(new Interest(properties, OpenPopupCabinet));
                    break;
                case 3:
                    AddPointToCanvas(new Ladder(properties, OpenPopupCabinet));
                    break;
                default:
                    break;

            }
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var pos = getPosition();

            if(isCabinetButtonActive && !IsMouseOverUI())
            {
                AddPointToCanvas(new Cabinet(pos, OpenPopupCabinet));
            }
            else if(isInterestButtonActive && !IsMouseOverUI())
            {
                AddPointToCanvas(new Interest(pos, OpenPopupInterest));
            }
            else if(isLadderButtonActive && !IsMouseOverUI())
            {
                AddPointToCanvas(new Ladder(pos, OpenPopupLadder));
            }
        }
    }

    private void AddPointForUser()
    {
        var dropDowns = PathFinder.GetComponentsInChildren<TMP_Dropdown>();
        foreach (var dropdown in dropDowns)
        {
            foreach (var point in points)
            {
                if(!(point is Ladder))
                {
                    dropdown.options.Add(new TMP_Dropdown.OptionData(){text = point.curProperties.TextFirst});
                }
            }
        }
    }

    private void AddPointForDropDown(TMP_Dropdown dropdown)
    {
        
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private Vector3 getPosition()
    {
        Vector3 pos = Input.mousePosition;
        
        pos.x -= Screen.width / 2;
        pos.y -= Screen.height / 2;

        return pos;
    }

    private void AddPointToCanvas(Point point)
    {
        var button = point.CreatePoint();
        points.Add(point);
        button.transform.SetParent(transform, false);
    }

    public void CabinetPress()
    {
        isCabinetButtonActive = !isCabinetButtonActive;
    }

    public void InterestPress()
    {
        isInterestButtonActive = !isInterestButtonActive;
    }

    public void LadderPress()
    {
        isLadderButtonActive = !isLadderButtonActive;
    }

    private void OpenPopupCabinet(Point point)
    {
        if(PopupCabinet != null)
        {
            curPoint = point;
            
            var textFields = PopupCabinet.GetComponentsInChildren<TMP_InputField>();
            SetTextField(textFields);

            PopupCabinet.transform.SetAsLastSibling();
            PopupCabinet.SetActive(true);
        }
    }

    public void SavePopupCabinet()
    {
        var textFields = PopupCabinet.GetComponentsInChildren<TMP_InputField>();
        SaveTextField(textFields);
        CloseAllPopups();
    }

    private void OpenPopupInterest(Point point)
    {
        if(PopupInterest != null)
        {
            curPoint = point;

            var textFields = PopupInterest.GetComponentsInChildren<TMP_InputField>();
            SetTextField(textFields);

            PopupInterest.transform.SetAsLastSibling();
            PopupInterest.SetActive(true);
        }
    }

    public void SavePopupInterest()
    {
        var textFields = PopupInterest.GetComponentsInChildren<TMP_InputField>();
        SaveTextField(textFields);
        CloseAllPopups();
    }

    private void OpenPopupLadder(Point point)
    {
        if(PopupLadder != null)
        {
            curPoint = point;

            var textFields = PopupLadder.GetComponentsInChildren<TMP_InputField>();
            SetTextField(textFields);

            PopupLadder.transform.SetAsLastSibling();
            PopupLadder.SetActive(true);
        }
    }

    public void SavePopupLadder()
    {
        var textFields = PopupLadder.GetComponentsInChildren<TMP_InputField>();
        SaveTextField(textFields);

        int count1 = 0;
        int count2 = 0;
        foreach (var point in points)
        {
            if(point is Ladder)
            {
                if(point.curProperties.TextFirst == textFields[1].text)
                {
                    count1++;
                }
                if(point.curProperties.TextFirst == textFields[2].text)
                {
                    count2++;
                }
            }   
        }

        if(count1 == 0)
        {   
            curPoint.curProperties.TextSecond = "";
        }        
        if(count2 == 0)
        {
            curPoint.curProperties.TextThird = "";
        }
        CloseAllPopups();
    }

    public void CloseAllPopups()
    {
        PopupCabinet.SetActive(false);
        PopupInterest.SetActive(false);
        PopupLadder.SetActive(false);
    }

    public void DeletePoint()
    {
        DestroyImmediate(curPoint.curButton);
        DeletePointFromList();
        CloseAllPopups();
    }

    private void DeletePointFromList()
    {
        points.Remove(curPoint);
    }

    private void SaveTextField(TMP_InputField[] textFields)
    {
        curPoint.curProperties.TextFirst = textFields[0].text;
        curPoint.curProperties.TextSecond = textFields[1].text;
        curPoint.curProperties.TextThird = textFields[2].text;
    }

    private void SetTextField(TMP_InputField[] textFields)
    {
        textFields[0].text = curPoint.curProperties.TextFirst;
        textFields[1].text = curPoint.curProperties.TextSecond;
        textFields[2].text = curPoint.curProperties.TextThird;
    }


    public void SaveAll()
    {   
        List<PointProperties> pointsProperties = new List<PointProperties>();
        points.ForEach(x => pointsProperties.Add(x.curProperties));
        ioFile.Write(pointsProperties);
    }
}
