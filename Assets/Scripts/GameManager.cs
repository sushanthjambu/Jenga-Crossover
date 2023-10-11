using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public List<Transform> positions;
    public GameObject glassBlock;
    public GameObject woodenBlock;
    public GameObject stoneBlock;
    private List<Grades> gradesList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAPIData());
    }

    private IEnumerator GetAPIData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string wrappedJson = "{ \"data\" : " + www.downloadHandler.text + "}";
            JsonResponse res = JsonUtility.FromJson<JsonResponse>(wrappedJson);
            Debug.Log("Third Block : " + res.data[2].standarddescription);
            SortData(res.data);
        }
    }

    private void SortData(List<JengaBlocks> allBlocks)
    {
        Debug.Log("All Blocks : " + allBlocks.Count);
        gradesList = new();
        foreach(JengaBlocks block in allBlocks)
        {
            if(gradesList.Count == 0)
            {
                Grades grade = new();
                gradesList.Add(grade);
                grade.gradeName = block.grade;
                grade.gradeBlocks.Add(block);
                grade.objectInHierarchy = new GameObject(grade.gradeName);
                grade.gameManager = this;
            }
            else
            {
                bool isBlockAdded = false;
                foreach (Grades grade in gradesList)
                {
                    if((grade.gradeName == block.grade))
                    {
                        grade.gradeBlocks.Add(block);
                        isBlockAdded = true;
                        break;
                    }
                }
                if (!isBlockAdded)
                {
                    Grades grade = new();
                    gradesList.Add(grade);
                    grade.gradeName = block.grade;
                    grade.gradeBlocks.Add(block);
                    grade.objectInHierarchy = new GameObject(grade.gradeName);
                    grade.gameManager = this;
                }
            }            
        }
        if(gradesList.Count >= positions.Count)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                gradesList[i].gradeBlocks.OrderBy(gr => gr.domain);
                gradesList[i].gradeBlocks.OrderBy(gr => gr.cluster);
                gradesList[i].gradeBlocks.OrderBy(gr => gr.standardid);

                Debug.Log("Grade : " + gradesList[i].gradeName + " Blocks : " + gradesList[i].gradeBlocks.Count);
                gradesList[i].CreateJengaStructure(positions[i].position);
            }
        }
    }

    public GameObject CreateGlassBlock(Vector3 pos, Quaternion rot, GameObject parent)
    {
        return Instantiate(glassBlock, pos, rot, parent.transform);
    }

    public GameObject CreateWoodenBlock(Vector3 pos, Quaternion rot, GameObject parent)
    {
        return Instantiate(woodenBlock, pos, rot, parent.transform);
    }

    public GameObject CreateStoneBlock(Vector3 pos, Quaternion rot, GameObject parent)
    {
        return Instantiate(stoneBlock, pos, rot, parent.transform);
    }

    public void DestroyGradeBlocks(string gradeIdentifier)
    {
        foreach(Grades grade in gradesList)
        {
            if (grade.gradeName.Contains(gradeIdentifier))
            {
                foreach(GameObject blocks in grade.instantiatedBlocks)
                {
                    if(blocks.GetComponent<BlockGradeInfo>().mastery == 0)
                    {
                        Destroy(blocks);
                    }
                }
            }
        }
    }

    public void RegenerateAllGrades()
    {
        foreach(Grades grade in gradesList)
        {
            foreach(GameObject obj in grade.instantiatedBlocks)
            {
                Destroy(obj);
                grade.gradeBlocks.Clear();
            }
        }
        gradesList.Clear();

        StartCoroutine(GetAPIData());
    }
}

[Serializable]
public class JsonResponse
{
    public List<JengaBlocks> data;
}

[Serializable]
public class JengaBlocks
{
    public static float length = 1.4f;
    public static float breadth = 0.35f;
    public static float height = 0.2f;
    public static float gap = 0.175f; //(length - (no.of blocks in row)*(breadth))/(no.of blocks in a row - 1) => (1.4 - (3 * 0.35))/(3 - 1) 

    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}
