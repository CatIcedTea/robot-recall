using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }
    public Vector2 size;
    public int startPos = 0;
    public Vector2 offset;
    List<Cell> board;
    public NavMeshSurface navMeshSurface;
    public GameObject[] roomPrefabs;


    void Start()
    {
        AdjustNavMeshBounds();
        MazeGenerator();
        BakeNavMesh();
    }


    void Update()
    {

    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    int random = Random.Range(0, roomPrefabs.Length);
                    var randomRoomPrefab = roomPrefabs[random];
                    var newRoom = Instantiate(randomRoomPrefab, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + " " + j;
                    if(random == 4){
                        roomPrefabs[4] = roomPrefabs[1];
                    }
                }
            }
        }
    }
    public void BakeNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
    void AdjustNavMeshBounds()
    {
        if (navMeshSurface != null)
        {
            // Calculate bounds based on dungeon size and offset
            Vector3 dungeonSize = new Vector3(size.x * offset.x, 10f, size.y * offset.y); // Ensure height is sufficient
            navMeshSurface.size = dungeonSize;
            navMeshSurface.center = new Vector3((size.x * offset.x) / 2, 0, -(size.y * offset.y) / 2);

            Debug.Log($"NavMesh bounds adjusted to size: {navMeshSurface.size}, center: {navMeshSurface.center}");

            // Rebake the NavMesh
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface not assigned!");
        }
    }
    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }
        int current = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 35)
        {
            k++;
            board[current].visited = true;

            if (current == board.Count - 1)
            {
                break;
            }

            List<int> neighbors = CheckNeighbors(current);
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    current = path.Pop();
                }
            }
            else
            {
                path.Push(current);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if (newCell > current)
                {
                    if (newCell - 1 == current)
                    {
                        board[current].status[2] = true;
                        current = newCell;
                        board[current].status[3] = true;
                    }
                    else
                    {
                        board[current].status[1] = true;
                        current = newCell;
                        board[current].status[0] = true;
                    }
                }
                else
                {
                    if (newCell + 1 == current)
                    {
                        board[current].status[3] = true;
                        current = newCell;
                        board[current].status[2] = true;
                    }
                    else
                    {
                        board[current].status[0] = true;
                        current = newCell;
                        board[current].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        //check up
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right
        if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbors;
    }
}
