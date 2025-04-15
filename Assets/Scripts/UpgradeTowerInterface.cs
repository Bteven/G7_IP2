using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UpgradeTowerInterface
{
    void UpgradeOne();
    void UpgradeTwo();

    bool UpgradeOneDone { get; }
    bool UpgradeTwoDone { get; }

    // this will alow to check if the upgrades have been called already

}
