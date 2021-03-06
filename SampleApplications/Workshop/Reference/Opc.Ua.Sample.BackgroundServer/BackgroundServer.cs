

using System;
using System.Collections.Generic;
using Opc.Ua.Configuration;
using Opc.Ua.Server;


namespace Opc.Ua.Sample.BackgroundServer
{
    /// <summary>
    /// Implements a basic Sample Server.
    /// </summary>
    /// <remarks>
    /// Each server instance must have one instance of a StandardServer object which is
    /// responsible for reading the configuration file, creating the endpoints and dispatching
    /// incoming requests to the appropriate handler.
    /// 
    /// This sub-class specifies non-configurable metadata such as Product Name and initializes
    /// the NodeManagementNodeManager which provides access to the data exposed by the Server.
    /// </remarks>
    internal partial class BackgroundServer : StandardServer
    {
        #region Overridden Methods
        /// <summary>
        /// Creates the node managers for the server.
        /// </summary>
        /// <remarks>
        /// This method allows the sub-class create any additional node managers which it uses. The SDK
        /// always creates a CoreNodeManager which handles the built-in nodes defined by the specification.
        /// Any additional NodeManagers are expected to handle application specific nodes.
        /// </remarks>
        protected override MasterNodeManager CreateMasterNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        {

            List<INodeManager> nodeManagers = new List<INodeManager>();

            // create the custom node managers.
            m_nodeManager = new BackgroundServerNodeManager(server, configuration);
            nodeManagers.Add(m_nodeManager);
            
            // create master node manager.
            return new MasterNodeManager(server, configuration, null, nodeManagers.ToArray());
        }

        /// <summary>
        /// Loads the non-configurable properties for the application.
        /// </summary>
        /// <remarks>
        /// These properties are exposed by the server but cannot be changed by administrators.
        /// </remarks>
        protected override ServerProperties LoadServerProperties()
        {
            ServerProperties properties = new ServerProperties();

            properties.ManufacturerName = "OPC Foundation";
            properties.ProductName      = "Opc.Ua.Sample.BackgroundServer";
            properties.ProductUri       = "http://opcfoundation.org/UA/Sample/BackgroundServer";
            properties.SoftwareVersion  = Utils.GetAssemblySoftwareVersion();
            properties.BuildNumber      = Utils.GetAssemblyBuildNumber();
            properties.BuildDate        = Utils.GetAssemblyTimestamp();

            return properties; 
        }

        #endregion

        #region Private Fields
        
        /// <summary>
        /// The sample node manager able to handle UA features
        /// </summary>
        private BackgroundServerNodeManager m_nodeManager;
        
        #endregion
    }
}
