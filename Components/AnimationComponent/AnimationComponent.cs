using Godot;
using System;

public partial class AnimationComponent : Node
{
    [ExportSubgroup("Animation settings")]
    [Export] AnimationPlayer AnimationPlayer;

    public void PlayAniamtion(string animationName)
    {
        AnimationPlayer.Play(animationName);
    }
}
