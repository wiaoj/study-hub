﻿namespace DesignPatterns.Structural.Decorator.Structural;
public class ConcreteDecoratorB : Decorator {
    public override void Operation() {
        base.Operation();
        AddedBehavior();
        Console.WriteLine("ConcreteDecoratorB.Operation()");
    }

    private void AddedBehavior() {
        Console.WriteLine("Behavior added");
    }
}