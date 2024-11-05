using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell{
        public bool visited = false;
        public bool[] status = new bool[4];
    }
    public Vector2 size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;
    List<Cell> board;
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        MazeGenerator();
    }

    
    void Update()
    {
        
    }

    void GenerateDungeon(){
        for(int i = 0; i < size.x; i++){
            for(int j = 0; j < size.y; j++){
                Cell currentCell = board[Mathf.FloorToInt(i+j*size.x)];
                if(currentCell.visited){
                    var newRoom = Instantiate(room, new Vector3(i*offset.x,0,-j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " "+i+" "+j;
                }
            }
        }
    }

    void MazeGenerator(){
        board = new List<Cell>();

        for(int i = 0; i < size.x; i++){
            for(int j = 0; j < size.y; j++){
                board.Add(new Cell());
            }
        }
        int current = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while(k < 35){
            k++;
            board[current].visited = true;

            if(current == board.Count - 1){
                break;
            }

            List<int> neighbors = CheckNeighbors(current);
            if(neighbors.Count == 0){
                if(path.Count == 0){
                    break;
                }
                else{
                    current = path.Pop();
                }
            }
            else{
                path.Push(current);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if(newCell > current){
                    if(newCell-1 == current){
                        board[current].status[2] = true;
                        current = newCell;
                        board[current].status[3] = true;
                    }
                    else{
                        board[current].status[1] = true;
                        current = newCell;
                        board[current].status[0] = true;
                    }
                }
                else{
                  if(newCell+1 == current){
                        board[current].status[3] = true;
                        current = newCell;
                        board[current].status[2] = true;
                    }
                    else{
                        board[current].status[0] = true;
                        current = newCell;
                        board[current].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }
    List<int> CheckNeighbors(int cell){
        List<int> neighbors = new List<int>();
        //check up
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited){
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down
        if(cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visited){
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right
        if((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell+1)].visited){
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left
        if(cell % size.x != 0 && !board[Mathf.FloorToInt(cell-1)].visited){
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbors;
    }
}
