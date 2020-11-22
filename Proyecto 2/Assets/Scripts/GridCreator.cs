using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridCreator : MonoBehaviour {

	public bool displayGridGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public Transform reference;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	public LinkedList<Node> BlackList = new LinkedList<Node>();
	public LinkedList<Node> WalkableList = new LinkedList<Node>();
	public LinkedList<Node> IndestructibleList = new LinkedList<Node>();

	void Start() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		reference.position = reference.position + new Vector3(-gridSizeX/2 + nodeRadius,gridSizeY/2 - nodeRadius,0);
		CreateGrid();
	}

	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;
		
		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint, x,y);
				WalkableList.Add(grid[x,y]);
			}
		}
		Node nodo1 = new Node(true, new Vector3(5, -6, 0), 5, 3);
		BlackList.Add(nodo1);

		Node nodo2 = new Node(true, new Vector3(5, -3, 0), 5, 6);
		BlackList.Add(nodo2);

		Node nodo3 = new Node(true, new Vector3(6, -7, 0), 6, 2);
		BlackList.Add(nodo3);

		Node nodo4 = new Node(true, new Vector3(6, -6, 0), 6, 3);
		BlackList.Add(nodo4);

		Node nodo5 = new Node(true, new Vector3(6, -3, 0), 6, 6);
		BlackList.Add(nodo5);

		Node nodo6 = new Node(true, new Vector3(6, -2, 0), 6, 7);
		BlackList.Add(nodo6);

		Node nodo7 = new Node(true, new Vector3(13, -7, 0), 13, 2);
		BlackList.Add(nodo7);

		Node nodo8 = new Node(true, new Vector3(13, -6, 0), 13, 3);
		BlackList.Add(nodo8);

		Node nodo9 = new Node(true, new Vector3(13, -3, 0), 13, 6);
		BlackList.Add(nodo9);

		Node nodo10 = new Node(true, new Vector3(13, -2, 0), 13, 7);
		BlackList.Add(nodo10);

		Node nodo11 = new Node(true, new Vector3(14, -6, 0), 14, 3);
		BlackList.Add(nodo11);

		Node nodo12 = new Node(true, new Vector3(14, -3, 0), 14, 6);
		BlackList.Add(nodo12);

		Node temp1 = BlackList.head;
		Node temp2 = WalkableList.head;
		while(temp1 != null)
		{
			while(temp2 != null)
			{
				if(temp1.gridX == temp2.gridX && temp1.gridY == temp2.gridY)
				{
					Node temp4;
					temp4 = WalkableList.SearchNode(temp1.gridX, temp1.gridY);
					temp4.walkable = false;
					temp2 = temp2.next;
					Debug.Log("Entre if");
				}else {
					Debug.Log("Entre else");
					temp2 = temp2.next;
				}		
			}
			temp1 = temp1.next;
		}
		/*
		for(int x = 1; x < gridSizeX - 1; x++)
		{
			//var randY = Random.Range(1,9);
		}*/
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,gridWorldSize.y,1));

		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}