using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class SettingMenuManager : MonoBehaviour
{
    public TMP_Dropdown ResDropDown;
    public Toggle FullScreenToggle;

    Resolution[] AllResolutions;
    bool IsFullScreen;
    int SelectedResolution;
    List<Resolution> SelectedResoultionList = new List<Resolution>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsFullScreen = true;
        AllResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach(Resolution res in AllResolutions)
        {
            newRes = res.width.ToString() + "x" + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResoultionList.Add(res);
            }
            resolutionStringList.Add(res.ToString());
        }

        ResDropDown.AddOptions(resolutionStringList);
    }

    public void ChangeResoultion()
    {
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(SelectedResoultionList[SelectedResolution].width, SelectedResoultionList[SelectedResolution].height, IsFullScreen);
    }
    public void ChangeFullScreen()
    {
        IsFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(SelectedResoultionList[SelectedResolution].width, SelectedResoultionList[SelectedResolution].height, IsFullScreen);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
