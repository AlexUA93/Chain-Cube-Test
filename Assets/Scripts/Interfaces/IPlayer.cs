using System;
public interface IPlayer : IMove, IAction
{
    public Action ACTION { get; set; }

    public void SetView(CubeView view, float force);
}
