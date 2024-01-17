using UMSAGL.Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Visualization.Networking;
using Visualization.UI;

namespace Visualization.ClassDiagram
{
    public class ClassDiagramBuilderServer : ClassDiagramBuilder
    {
        public override void CreateGraph()
        {
            UIEditorManager.Instance.mainEditor.ClearDiagram();
            var graphGo = GameObject.Instantiate(DiagramPool.Instance.networkGraphPrefab);
            graphGo.name = "Graph";
            var unitsGo = GameObject.Instantiate(DiagramPool.Instance.networkUnitsPrefab);
            unitsGo.name = "Units";

            DiagramPool.Instance.ClassDiagram.graph = graphGo.GetComponent<Graph>();
            DiagramPool.Instance.ClassDiagram.graph.nodePrefab = DiagramPool.Instance.networkClassPrefab;
            GameObject.Find("DiagramSettings/Buttons/Edit").GetComponentInChildren<Button>().interactable = true;
            GameObject.Find("AnimationSettings/Buttons/Load").GetComponentInChildren<Button>().interactable = true;
            GameObject.Find("AnimationSettings/Buttons/Create").GetComponentInChildren<Button>().interactable = true;
            GameObject.Find("MaskingSettings/Buttons/Load").GetComponentInChildren<Button>().interactable = true;
        }

        public override void MakeNetworkedGraph()
        {
            var graphGo = DiagramPool.Instance.ClassDiagram.graph.gameObject;
            Debug.Assert(graphGo);

            var graphNo = graphGo.GetComponent<NetworkObject>();
            graphNo.Spawn();
            Spawner.Instance.SetNetworkObjectNameClientRpc(graphNo.name, graphNo.NetworkObjectId);

            var unitsGo = GameObject.Find("Units");
            var unitsNo = unitsGo.GetComponent<NetworkObject>();
            unitsNo.Spawn();
            Spawner.Instance.SetNetworkObjectNameClientRpc(unitsNo.name, unitsNo.NetworkObjectId);

            if (!unitsNo.TrySetParent(graphGo))
            {
                throw new InvalidParentException(unitsGo.name);
            }
            unitsGo.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            unitsGo.GetComponent<Transform>().position = new Vector3(0, 0, 0);
            graphGo.GetComponent<Graph>().units = unitsGo.transform;
            Spawner.Instance.GraphCreatedClientRpc();
        }
    }
}
