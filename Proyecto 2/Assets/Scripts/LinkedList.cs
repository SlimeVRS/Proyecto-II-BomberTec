using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LinkedList
{
    private Node head;
    private Node last;

    public LinkedList()
    {
        
    }

    public void addNode(Node currentNode)
    {
        if (head == null)
        {
            SetHead(currentNode);
            SetLast(currentNode);
        }
        else
        {
            Node lastNode = GetLast();
            lastNode.SetNextNode(currentNode);
            SetLast(currentNode);
        }
    }
    

    private void SetHead(Node current_node)
    {
        this.head = current_node;
    }

    private void SetLast(Node current_node)
    {
        this.last = current_node;
    }

    private Node GetHead()
    {
        return head;
    }

    private Node GetLast()
    {
        return last;
    }
}
