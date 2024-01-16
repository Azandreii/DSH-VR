using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable 
{
   bool isGivenWorkCheckable { get; set; }
   bool isBoredCheckable { get; set; }
   bool isWaitingForApprovalCheckable {  get; set; }
   bool isWorkingCheckable { get; set; }
   bool isUnavailableCheckable {  get; set; }

    void SetIsGivenWorkStatus (bool isGivenWorkCheckable);
    void SetIsBoredStatus (bool isBoredCheckable);
    void SetIsWaitingForApprovalStatus(bool isWaitingForApprovalCheckable);
    void SetIsWorkingStatus (bool isWorkingCheckable);
    void SetIsUnavailable (bool isUnavailable);
}
