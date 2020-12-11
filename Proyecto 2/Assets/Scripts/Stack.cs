using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    private BackTrackingNode head;


    /// <sumary>
    /// Constructor of the Stack
    /// </sumary>
    public Stack()
    {
        head = null;
    }

    /// <sumary>
    /// Adds a new node at the head of the stack
    /// </sumary>
    /// <param name="newNode">New node to be added</param>
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

    /// <sumary>
    /// Returns the first node on the stack
    /// </sumary>
    public BackTrackingNode peek()
    {
        return head;
    }

    /// <sumary>
    /// Returns and removes the first node on the stack
    /// </sumary>
    public BackTrackingNode pop()
    {
        if (head == null) 
            return null;
            
        BackTrackingNode temp = head;
        head = temp.NextNode;
        return temp;
    }
}
