
using System;
using System.Threading;
using System.Collections.Generic;
using Opc.Ua;
using Opc.Ua.Server;

namespace Opc.Ua.Sample.BackgroundServer
{

    internal static partial class Namespaces
    {
        /// <summary>
        /// The namespace for the nodes provided by the server.
        /// </summary>
        internal const string BackgroundServer = "http://opcfoundation.org/UA/Sample/BackgroundServer";
    }


    /// <summary>
    /// A node manager for a server that provides an implementation of the OPC UA features.
    /// </summary>
    internal class BackgroundServerNodeManager : CustomNodeManager2
    {
        #region Constructors
        /// <summary>
        /// Initializes the node manager.
        /// </summary>
        public BackgroundServerNodeManager(IServerInternal server, ApplicationConfiguration configuration)
            : base(server, configuration, Namespaces.BackgroundServer)
        {
            SystemContext.NodeIdFactory = this;            
        }
        #endregion

        #region INodeIdFactory Members
        /// <summary>
        /// Creates the NodeId for the specified node.
        /// </summary>
        public override NodeId New(ISystemContext context, NodeState node)
        {
            return GenerateNodeId();
        }
        #endregion

        #region INodeManager Members
        /// <summary>
        /// Does any initialization required before the address space can be used.
        /// </summary>
        /// <remarks>
        /// The externalReferences is an out parameter that allows the node manager to link to nodes
        /// in other node managers. For example, the 'Objects' node is managed by the CoreNodeManager and
        /// should have a reference to the root folder node(s) exposed by this node manager.  
        /// </remarks>
        public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            lock (Lock)
            {

                // create the refrigerator folder.
                FolderState folder = new FolderState(null);
                folder.NodeId = new NodeId("Devices", NamespaceIndex);
                folder.BrowseName = new QualifiedName("Devices", NamespaceIndex);
                folder.DisplayName = folder.BrowseName.Name;
                folder.TypeDefinitionId = Opc.Ua.ObjectTypeIds.FolderType;

                IList<IReference> references = null;

                if (!externalReferences.TryGetValue(Opc.Ua.ObjectIds.ObjectsFolder, out references))
                {
                    externalReferences[Opc.Ua.ObjectIds.ObjectsFolder] = references = new List<IReference>();
                }

                references.Add(new NodeStateReference(ReferenceTypeIds.Organizes, false, folder.NodeId));
                folder.AddReference(ReferenceTypeIds.Organizes, true, Opc.Ua.ObjectIds.ObjectsFolder);

                // save the node for later lookup.
                AddPredefinedNode(SystemContext, folder);

                // create the Device instance object
                CreateDevice(SystemContext, folder);                
            }
        }


        /// <summary>
        /// Frees any resources allocated for the address space.
        /// </summary>
        public override void DeleteAddressSpace()
        {
            lock (Lock)
            {
                
                m_simulationTimer.Dispose();
            }
        }

        /// <summary>
        /// Returns a unique handle for the node.
        /// </summary>
        protected override NodeHandle GetManagerHandle(ServerSystemContext context, NodeId nodeId, IDictionary<NodeId, NodeState> cache)
        {
            lock (Lock)
            {
                // quickly exclude nodes that are not in the namespace. 
                if (!IsNodeIdInNamespace(nodeId))
                {
                    return null;
                }

                NodeState node = null;

                if (PredefinedNodes != null && !PredefinedNodes.TryGetValue(nodeId, out node))
                {
                    return null;
                }

                NodeHandle handle = new NodeHandle();

                handle.NodeId = nodeId;
                handle.Node = node;
                handle.Validated = true;

                return handle;
            }
        }

        /// <summary>
        /// Verifies that the specified node exists.
        /// </summary>
        protected override NodeState ValidateNode(
            ServerSystemContext context,
            NodeHandle handle,
            IDictionary<NodeId, NodeState> cache)
        {
            // not valid if no root.
            if (handle == null)
            {
                return null;
            }

            // check if previously validated.
            if (handle.Validated)
            {
                return handle.Node;
            }

            // TBD

            return null;
        }
        /// <summary>
        /// Gernerates a new numeric node id for the actual namespace index
        /// </summary>
        /// <returns></returns>
        private NodeId GenerateNodeId()
        {
            uint id = Utils.IncrementIdentifier(ref m_lastUsedId);
            return new NodeId(id, NamespaceIndex);
        }

        #endregion

        #region Private Methods


        private void CreateDevice(ServerSystemContext context, FolderState folder)
        {
            m_device = new ITG3200State(folder);
            string name = "ITG3200";

            m_device.Create(
                context,
                null,
                new QualifiedName(name, NamespaceIndex),
                null,
                true);

            folder.AddReference(Opc.Ua.ReferenceTypeIds.Organizes, false, m_device.NodeId);
            m_device.AddReference(Opc.Ua.ReferenceTypeIds.Organizes, true, folder.NodeId);

            m_device.GyroX.Value = 0;
            m_device.GyroY.Value = 0;
            m_device.GyroZ.Value = 0;
            m_device.Temperature.Value = 0;
            m_device.Online.Value = false;
            
            AddPredefinedNode(context, m_device);
            try
            {
                m_device.InitI2CGyro();
            }
            catch
            {
                // Trace 
            }
            finally
            {
                m_simulationTimer = new Timer(RunReadDevice, null, 1000, 1000);
            }
            
        }

        private void RunReadDevice(object state)
        {
            try
            {
                lock (Lock)
                {
                    m_device.ReadDevice();
                }
            }
            catch
            {
                // trace
            }
        }
        #endregion

        #region Private Attributes
        //  simulation timer
        private Timer m_simulationTimer; 
        private long m_lastUsedId = 0;
        ITG3200State m_device;
        #endregion
    }
}
