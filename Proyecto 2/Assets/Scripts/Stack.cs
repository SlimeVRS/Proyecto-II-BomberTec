using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic Stack data structure, a standard, very basic implementation created to use on a backtracking algorithm
/// </summary>
public class Stack
{
    private BackTrackingNode head;

    /// <summary>
    /// A standard constructor for the class, sets the first element of the stack to null.
    /// </summary>
    public Stack()
    {
        head = null;
    }

    /// <summary>
    /// Pushes the given node to the Stack.
    /// </summary>
    /// <param name="newNode"> A BackTrackingNode object to be pushed onto the Stack</param>
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

    /// <summary>
    /// A function that returns the first element on the Stack.
    /// </summary>
    /// <returns> A BackTrackingNode object that sits on top of the Stack</returns>
    public BackTrackingNode peek()
    {
        return head;
    }

    /// <summary>
    /// Pops the first element of the Stack out of it and returns it.
    /// </summary>
    /// <returns> A BackTrackingNode object that used to sit on top of the Stack</returns>
    public BackTrackingNode pop()
    {
        if (head == null) 
            return null;
            
        BackTrackingNode temp = head;
        head = temp.NextNode;
        return temp;
    }
}
