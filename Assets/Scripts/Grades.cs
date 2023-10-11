using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is created for each grade.
/// </summary>
public class Grades
{
    public string gradeName;
    public GameObject objectInHierarchy;
    public List<JengaBlocks> gradeBlocks = new();
    public GameManager gameManager;
    public List<GameObject> instantiatedBlocks = new();

    /// <summary>
    /// Start creating the Jenga structure
    /// </summary>
    /// <param name="startPos"></param>
    public void CreateJengaStructure(Vector3 startPos)
    {
        if(gradeBlocks.Count > 1)
        {
            for(int i = 0; i < gradeBlocks.Count; i++)
            {
                Vector3 blockPos = GetSpawnPosition(startPos, i);
                Quaternion blockRotation = GetSpawnRotation(i);
                if(gradeBlocks[i].mastery == 0)
                {
                    var insBlock = gameManager.CreateGlassBlock(blockPos, blockRotation, objectInHierarchy);
                    insBlock.GetComponent<BlockGradeInfo>().SetBlockInfo(gradeBlocks[i].grade, gradeBlocks[i].mastery, 
                        gradeBlocks[i].domain, gradeBlocks[i].cluster, gradeBlocks[i].standardid, gradeBlocks[i].standarddescription);
                    instantiatedBlocks.Add(insBlock);
                }
                else if(gradeBlocks[i].mastery == 1)
                {
                    var insBlock = gameManager.CreateWoodenBlock(blockPos, blockRotation, objectInHierarchy);
                    insBlock.GetComponent<BlockGradeInfo>().SetBlockInfo(gradeBlocks[i].grade, gradeBlocks[i].mastery,
                        gradeBlocks[i].domain, gradeBlocks[i].cluster, gradeBlocks[i].standardid, gradeBlocks[i].standarddescription);
                    instantiatedBlocks.Add(insBlock);
                }
                else if(gradeBlocks[i].mastery == 2)
                {
                    var insBlock = gameManager.CreateStoneBlock(blockPos, blockRotation, objectInHierarchy);
                    insBlock.GetComponent<BlockGradeInfo>().SetBlockInfo(gradeBlocks[i].grade, gradeBlocks[i].mastery,
                        gradeBlocks[i].domain, gradeBlocks[i].cluster, gradeBlocks[i].standardid, gradeBlocks[i].standarddescription);
                    instantiatedBlocks.Add(insBlock);
                }
            }
        }
    }

    /// <summary>
    /// Calculates spawn position of each block based in its index.
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3 GetSpawnPosition(Vector3 startPos, int index)
    {
        Vector3 spawnPosition = Vector3.zero;
        int rowNo = index / 3; //height of structure
        int posNo = index % 3; //position of block in that particular row
        if (rowNo % 2 == 0)
        {
            spawnPosition.x = startPos.x + (posNo - 1) * (JengaBlocks.breadth + JengaBlocks.gap);
            spawnPosition.y = startPos.y + rowNo * JengaBlocks.height;
            spawnPosition.z = startPos.z;
            return spawnPosition;
        }
        else
        {
            spawnPosition.x = startPos.x; //Adding length because in this row length of block is along positive x axis. Subtracting breadth because the position is at center of block.
            spawnPosition.y = startPos.y + rowNo * JengaBlocks.height;
            spawnPosition.z = startPos.z + (posNo - 1) * (JengaBlocks.breadth + JengaBlocks.gap);
            return spawnPosition;
        }
    }

    /// <summary>
    /// Calculates spawn rotation of each block based on its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private Quaternion GetSpawnRotation(int index)
    {
        int rowNo = index / 3;
        if(rowNo % 2 == 0)
        {
            return Quaternion.identity;
        }
        else
        {
            return Quaternion.AngleAxis(90, Vector3.up);
        }
    }
}
