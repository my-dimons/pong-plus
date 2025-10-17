using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Example Upgrade", order = 0)]
public class ExampleUpgrade : Upgrade
{
    [Space(10)]
    [Header("Example Upgrade")]
    public string example;

    public override void ApplyUpgrade()
    {
        AppliedUpgrade();
    }
}
