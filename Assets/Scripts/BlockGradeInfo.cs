using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to every block to display infor upon right click
/// </summary>
public class BlockGradeInfo : MonoBehaviour
{
    [HideInInspector] public string grade;
    [HideInInspector] public int mastery;
    [HideInInspector] public string domain;
    [HideInInspector] public string cluster;
    [HideInInspector] public string standardId;
    [HideInInspector] public string standardDescription;

    /// <summary>
    /// Adding info about this block
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="mastery"></param>
    /// <param name="domain"></param>
    /// <param name="cluster"></param>
    /// <param name="standardId"></param>
    /// <param name="standardDescription"></param>
    public void SetBlockInfo(string grade, int mastery, string domain, string cluster, string standardId, string standardDescription)
    {
        this.grade = grade;
        this.mastery = mastery;
        this.domain = domain;
        this.cluster = cluster;
        this.standardId = standardId;
        this.standardDescription = standardDescription;
    }

    /// <summary>
    /// Detecting right click on this block
    /// </summary>
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var UIObject = GameObject.FindGameObjectWithTag("UIManager");
            if(UIObject != null)
            {
                var uiManager = UIObject.GetComponent<UIManager>();
                string blockInfoText = grade + " : " + domain + "\n\n" + cluster + "\n\n" + standardId + " : " + standardDescription;
                uiManager.ShowBlockInfo(blockInfoText);
            }
        }
    }
}
