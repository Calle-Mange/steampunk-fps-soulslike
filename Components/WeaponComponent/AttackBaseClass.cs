using Godot;
using System;

public partial class AttackBaseClass : Node
{
    [ExportSubgroup("Attack settings")]
    [Export] int AttackDamage = 10;
}
