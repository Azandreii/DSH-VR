using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable 
{
   bool isGivenWorkCheckable { get; set; }
   bool isBoredCheckable { get; set; }
   bool isFinishedCheckable {  get; set; }
   bool isHighfivedCheckable { get; set; }
   bool isWorkingCheckable { get; set; }
   bool isGoingToWorkCheckable { get; set; }

    void SetIsGivenWorkStatus (bool isGivenWorkCheckable);
    void SetIsBoredStatus (bool isBoredCheckable);
    void SetIsFinishedStatus (bool isFinishedCheckable);
    void SetIsHighfivedStatus (bool isHighfivedCheckable);
    void SetIsWorkingStatus (bool isWorkingCheckable);
    void SetGoingToWorkStatus (bool isGoingToWorkCheckable);
}
