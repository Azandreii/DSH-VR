using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool isAwaitingTaskCheckable { get; set; }
    bool isBoredCheckable { get; set; }
    bool isWaitingForApprovalCheckable { get; set; }
    bool isWorkingCheckable { get; set; }
    bool isUnavailableCheckable { get; set; }
    bool isHighFivedCheckable { get; set; }

    void SetIsAwaitingTaskState(bool _isAwaitingTaskCheckable);
    void SetIsBoredStatus (bool _isBoredCheckable);
    void SetIsWaitingForApprovalStatus(bool _isWaitingForApprovalCheckable);
    void SetIsWorkingStatus (bool _isWorkingCheckable);
    void SetIsUnavailable (bool _isUnavailableCheckable);
    void SetIsHighFived (bool _isHighFivedCheckable);
}
