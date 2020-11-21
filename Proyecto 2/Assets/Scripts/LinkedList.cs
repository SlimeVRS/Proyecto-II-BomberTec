using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    internal Node<T> head;

    internal void Add(SingleLinkedList list, int new_data)    
    {    
        Node<T> new_node = new Node<T>(new_data);    
        if (list.head == null) {    
            list.head = new_node;    
            return;    
        }    
        Node<T> lastNode = GetLastNode(list);    
        lastNode.next = new_node;    
    }  

    internal Node<T> GetLastNode(SingleLinkedList list) 
    {  
        Node<T> temp = list.head;  
        while (temp.next != null) {  
            temp = temp.next;  
        }  
        return temp;  
    }  

    internal void DeleteNodebyKey(SingleLinkedList list, int key)  
    {  
        Node<T> temp = list.head;  
        Node<T> prev = null;  
        if (temp != null && temp.data == key) {  
            list.head = temp.next;  
            return;  
        }  
        while (temp != null && temp.data != key) {  
            prev = temp;  
            temp = temp.next;  
        }  
        if (temp == null) {  
            return;  
        }  
        prev.next = temp.next;  
    } 
}
