using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LinkedList
{
    private Node head;
    private Node last;
    public int length = 0;

    public LinkedList()
    {
        
    }
    

    public void AddNode(Node currentNode)
    {
        if (head == null)
        {
            SetHead(currentNode);
            SetLast(currentNode);
            length++;

        }
        else
        {
            Node lastNode = GetLast();
            lastNode.SetNextNode(currentNode);
            SetLast(currentNode);
            length++;
        }
    }

    public void RemoveNode(Node deletion)
    {
        if (this.head == deletion)
        {
            this.head = deletion.GetNextNode();
        }
        
        else if (this.last == deletion)
        {
            this.last = deletion.GetPreviousNode();
        }
        else
        {
            Node previous = deletion.GetPreviousNode();
            Node next = deletion.GetNextNode();
            previous.SetNextNode(next);
        }
    }

    public bool Contains(Node node)
    {
        
        for (Node currentNode = this.GetHead();
            currentNode != null;
            currentNode = currentNode.GetNextNode())
        {
            if (currentNode == node)
            {
                return true;
            }
        }

        return false;
    }
    

    private void SetHead(Node currentNode)
    {
        this.head = currentNode;
    }

    private void SetLast(Node currentNode)
    {
        this.last = currentNode;
    }

    public Node GetHead()
    {
        return head;
    }

    private Node GetLast()
    {
        return last;
    }

    public LinkedList Reverse()
    {
        LinkedList reversedList = new LinkedList();
        reversedList.SetHead(this.last);
        for (Node currentNode = this.last.GetPreviousNode();
            currentNode!= null;
            currentNode = currentNode.GetPreviousNode())
        {
            reversedList.AddNode(currentNode);
        }

        return reversedList;
    }
}
