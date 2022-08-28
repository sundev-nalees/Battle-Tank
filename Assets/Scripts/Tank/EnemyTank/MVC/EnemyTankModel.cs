using UnityEngine;
public class EnemyTankModel 
{
    private TankScriptableObject tankObject;

    private Transform[] wayPoints;

    EnemyTankController controller;

    public EnemyTankModel(TankScriptableObject tankScriptableObject,Transform[] _targetPoints)
    {
        this.tankObject = tankScriptableObject;
        this.wayPoints = _targetPoints;
    }

    public TankScriptableObject GetTankObjects()
    {
        return tankObject;
    }
    public void SetTankController(EnemyTankController _controller)
    {
        controller = _controller;
    }


    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }
}
