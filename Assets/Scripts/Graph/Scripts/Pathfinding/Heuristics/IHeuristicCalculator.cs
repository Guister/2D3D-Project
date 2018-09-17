using UnityEngine;
using System.Collections;

public interface IHeuristicCalculator{

	float Calculate (IGraphNode origin, IGraphNode target);
}
