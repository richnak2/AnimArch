using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class HighlightSubject
{
    private List<HighlightObserver> Observers = new List<HighlightObserver>();
    public int HighlightInt{get; private set;} = 0;

    public void Attach(HighlightObserver observer)
    {
        this.Observers.Add(observer);
    }

    public void Unregister(HighlightObserver observer)
    {
        this.Observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (HighlightObserver observer in Observers)
        {
            observer.Update();
        }
    }
    
    public void IncrementHighlightLevel()
    {
        HighlightInt++;
        Notify();
    }

    public void DecrementHighlightLevel()
    {
        if (HighlightInt > 0)
        {
            HighlightInt--;
            Notify();
        }
        
    }



}



}
