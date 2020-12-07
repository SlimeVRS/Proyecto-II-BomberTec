using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    private BackTrackingNode head;

    public Stack()
    {
        head = null;
    }

    public void push(BackTrackingNode newNode)
    {
        if (head == null)
        {
            head = newNode;
            return;
        }

        BackTrackingNode tempNext = head;
        head = newNode;
        head.NextNode = tempNext;
    }

    public BackTrackingNode peek()
    {
        return head;
    }

    public BackTrackingNode pop()
    {
        if (head == null) 
            return null;
            
        BackTrackingNode temp = head;
        head = temp.NextNode;
        return temp;
    }
}
