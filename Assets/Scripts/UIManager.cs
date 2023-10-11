using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CameraMovement cameraController;
    [SerializeField] GameObject infoPanel;
    [SerializeField] TextMeshProUGUI blockInfo;

    private string selectedGrade = "6";
    // Start is called before the first frame update
    void Start()
    {
        //Default with 6th grade
        SixthGradeToggle(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SixthGradeToggle(bool isToggleOn)
    {
        if (isToggleOn)
        {
            selectedGrade = "6";
            cameraController.UpdateTarget(gameManager.positions[0]);
        }        
    }

    public void SeventhGradeToggle(bool isToggleOn)
    {
        if (isToggleOn)
        {
            selectedGrade = "7";
            cameraController.UpdateTarget(gameManager.positions[1]);
        }
    }

    public void EighthGradeToggle(bool isToggleOn)
    {
        if (isToggleOn)
        {
            selectedGrade = "8";
            cameraController.UpdateTarget(gameManager.positions[2]);
        }
    }

    /// <summary>
    /// Test My Stack button 
    /// </summary>
    public void DestroyGlassBlocks()
    {
        gameManager.DestroyGradeBlocks(selectedGrade);
    }

    /// <summary>
    /// Displays panel for block info
    /// </summary>
    /// <param name="text"></param>
    public void ShowBlockInfo(string text)
    {
        infoPanel.SetActive(true);
        blockInfo.text = text;
    }
}
