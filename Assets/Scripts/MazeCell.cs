using UnityEngine;

public class MazeCell
{
    //Variables
    public bool IsVisitaded = false;
    public GameObject cellObject;
    public MazeCell(int x, int y, GameObject obj)
    {
        cellObject = obj;
    }

    public void RemoveWall(string wallName)
    {
        Transform wall = cellObject.transform.Find(wallName);
        if (wall != null)
        {
            wall.gameObject.SetActive(false); // SE DESACTIVA
        }
    }
}