using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    //Variables de configuración del laberinto
    [Header("Variables de la configuración del laberinto")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1.0f; //Este es el tamaño de la celda, ojo! debe coincidir con la escala

    //Variables de los prefabs
    [Header("Variables de los prefabs")]
    public GameObject cellPrefab ; // el objeto de las cuatro paredes
    public GameObject keyPrefab;
    public GameObject exitPrefab;
    public GameObject playerPrefab;
        //TODO Meter Prefab de llaves y final
        
    private MazeCell[,] grid;//Matriz para los datos y las paredes de cada celda individual

    // Start is called before the first frame update
    void Start()
    {
        // Inicializaciamos
        InialGrid();
        // Ejecutamos el algoritmo
        GenerateMaze(0,0);
        // Colocariamos los distintos elementos (llaves, puertas y jugador)
        placeItems();
        // Ponemos la camara en su sitio para que se vea completa
        placeCamera();
        //
    }

    private void placeCamera()
    {
        Camera camera = Camera.main;
        //cALCULAMOS LA POSICION 

        float centroX = (width*cellSize) / 2f - (cellSize/2f);
        float centroZ = (height * cellSize) /2f - (cellSize / 2f);

        //Calculamos la altura 

        float altura = Mathf.Max(width, height) * cellSize * 1.1f;

        camera.transform.position = new Vector3(centroX,altura,centroZ);
        //Imporante, darle la vuelta a la camara

        camera.transform.rotation = Quaternion.Euler(90, 0, 0);

    }

    private void placeItems()
    {
        //Instancio la salida, arriba a la derecha
        Vector3 ExitPosition = new Vector3((width-1) * cellSize, 0.5f, (height - 1) * cellSize);   
        Instantiate(exitPrefab,ExitPosition,Quaternion.identity,transform);

        int posXKey, posYKey;

        do
        {
            posXKey = UnityEngine.Random.Range(0, width);
            posYKey = UnityEngine.Random.Range(0, height);
        } while (posYKey == 0 && posXKey == 0);

        Vector3 keyPosition = new Vector3(posXKey * cellSize,0 , posYKey * cellSize);
        Instantiate(keyPrefab, keyPosition, Quaternion.identity,transform);
        Vector3 PlayerPosition = new Vector3(0, 0.5f, 0);
        Instantiate(playerPrefab, PlayerPosition, Quaternion.identity,transform);

    }
    //Generacion aleatoria recursiva con backtracking
    private void GenerateMaze(int x, int y)
    {
        grid[x,y].IsVisitaded = true;
        //Creamos una lista en la que ponemos las paredes que debemos romper para su salida

        List<(int dx, int dy, string wallA, string wallB)> Direcciones = new List<(int dx, int dy, string wallA, string wallB)>
        {
             (0,1,"Wall_N","Wall_S"),
             (0,-1,"Wall_S","Wall_N"),
             (1,0,"Wall_W","Wall_E"),
             (-1,0,"Wall_E","Wall_W"),
        };

        for (int i = 0; i < Direcciones.Count; i++)
        {
            var temp = Direcciones[i];
            int ramdomTile= UnityEngine.Random.Range(i,Direcciones.Count);
            Direcciones[i] = Direcciones[ramdomTile];
            Direcciones[ramdomTile] = temp;
        }

        foreach(var direcion in Direcciones)
        {
            int nx = x + direcion.dx;
            int ny = y + direcion.dy;

            if (nx >= 0 && ny >= 0 && nx < width && ny < height && !grid[nx, ny].IsVisitaded)
            {
                grid[x, y].RemoveWall(direcion.wallA);
                grid[nx, ny].RemoveWall(direcion.wallB);
                GenerateMaze(nx,ny);
            }
        }
    }
    /// <summary>
    /// Inicia la rejilla entera
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InialGrid()
    {
        grid = new MazeCell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject newCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                grid[x, y] = new MazeCell(x, y, newCell);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
