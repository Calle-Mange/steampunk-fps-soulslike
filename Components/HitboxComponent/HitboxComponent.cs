using Godot;
using System;

public partial class HitboxComponent : CollisionShape3D
{
    [ExportSubgroup("Components")]
    [Export] HealthComponent HealthComponent;

    public override void _Ready()
    {
        
    }

    public void Damage()
    {
        HealthComponent.Damage();
    }
}
