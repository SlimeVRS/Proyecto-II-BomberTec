using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    public Node head;

    public void Add(Node node)    
    {    
        Node new_node = new Node(node);    
        if (this.head == null) {    
            this.head = new_node;    
            return;
        }    
        Node lastNode = GetLastNode(list);    
        lastNode.next = new_node;    
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
        if (temp != null && temp.x == xpos && temp.y == ypos) {  
            this.head = temp.next;  
            return;  
        }  
        while (temp != null temp.x != xpos && temp.y != yposy) {  
            prev = temp;  
            temp = temp.next;  
        }  
        if (temp == null) {  
            return;  
        }  
        prev.next = temp.next;  
    } 
}
