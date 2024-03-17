using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class HighlightSubject
{
    private List<Observer> Observers = new List<Observer>();
    public int HighlightInt{get; private set;} = 0;

    public void Attach(Observer observer)
    {
        this.Observers.Add(observer);
    }

    public void Unregister(Observer observer)
    {
        this.Observers.Remove(observer);
    }

    public void Notify() {
        foreach (Observer observer in Observers)
        {
            observer.Update();
        }
    }
    
    public void IncrementHighlightLevel() {
        HighlightInt++;
        Notify();
    }

    public void DecrementHighlightLevel() {
        if (HighlightInt > 0)
        {
            HighlightInt--;
            Notify();
        }
        
    }



}



}
