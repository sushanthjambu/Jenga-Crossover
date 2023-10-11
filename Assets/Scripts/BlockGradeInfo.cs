using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGradeInfo : MonoBehaviour
{
    [HideInInspector] public string grade;
    [HideInInspector] public int mastery;
    [HideInInspector] public string domain;
    [HideInInspector] public string cluster;
    [HideInInspector] public string standardId;
    [HideInInspector] public string standardDescription;

    public void SetBlockInfo(string grade, int mastery, string domain, string cluster, string standardId, string standardDescription)
    {
        this.grade = grade;
        this.mastery = mastery;
        this.domain = domain;
        this.cluster = cluster;
        this.standardId = standardId;
        this.standardDescription = standardDescription;
    }

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
