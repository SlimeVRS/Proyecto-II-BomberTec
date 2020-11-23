using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    public Node head;

    public void Add(Node node)    
    {       
        if (this.head == null) {    
            this.head = node;    
            return;
        }    
        Node lastNode = GetLastNode();    
        lastNode.next = node;
    }  

    public Node GetLastNode() 
    {  
        Node temp = this.head;
        while (temp.next != null) {  
            temp = temp.next;  
        }  
        return temp;  
    }  
    
    public void DeleteNode(int xpos, int ypos)  
    {  
        Node temp = this.head;  
        Node prev = null;  
        if (temp != null && temp.gridX == xpos && temp.gridY == ypos) {  
            this.head = temp.next;  
            return;  
        }  
        while (temp != null && temp.gridX != xpos && temp.gridY != ypos) {  
            prev = temp;  
            temp = temp.next;  
        }  
        if (temp == null) {  
            return;  
        }  
        prev.next = temp.next;  
    }

    public Node SearchNode(int xpos, int ypos)
    {  
        Node temp = this.head;  
        while (temp != null) {  
            if(temp.gridX == xpos && temp.gridY == ypos)
            {
                return temp;
            } else {
                temp = temp.next;
            }
        }
        Debug.Log("No se encontro");
        return temp = null;
    }
}
