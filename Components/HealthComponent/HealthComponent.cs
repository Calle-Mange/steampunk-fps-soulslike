using Godot;
using System;

public partial class HealthComponent : Node
{
    [ExportSubgroup("Health settings")]
    [Export] int MaxHealth = 100;

    public int Health { get; set; }

    public override void _Ready()
    {
        Health = MaxHealth;
    }

    public void Damage()
    {
        GD.Print($"Health reduced to: {Health}");

        if (Health <= 0)
        {
            GD.Print("Health is zero or below, triggering death logic.");
        }
    }
}
