
using System;
using System.Collections.Generic;
using Opc.Ua;

namespace Opc.Ua.Sample.BackgroundServer
{
    #region ITG3200State Class
    #if (!OPCUA_EXCLUDE_ITG3200State)
    /// <summary>
    /// Represents an instance of the ITG3200Type ObjectType.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    internal partial class ITG3200State : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ITG3200State(NodeState parent) : base(parent)
        {
        }
        
        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        /// <param name="namespaceUris">The namespace table containing the namespace URIs.</param>
        /// <returns>The node id.</returns>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return NodeId.Create(ITG3200.ObjectTypes.ITG3200Type, ITG3200.Namespaces.ITG3200, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="context">The system context.</param>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        /// <param name="context">The system context.</param>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACMAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvSVRHMzIwMP////8EYIAAAQAAAAEAEwAA" +
           "AElURzMyMDBUeXBlSW5zdGFuY2UBARsAAQEbAP////8FAAAAFWCJCgIAAAABAAUAAABHeXJvWAEBHAAA" +
           "LwEAQAkcAAAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBIAAALgBEIAAAAAEA" +
           "dAP/////AQH/////AAAAABVgiQoCAAAAAQAFAAAAR3lyb1kBASIAAC8BAEAJIgAAAAAL/////wEB////" +
           "/wEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBASYAAC4ARCYAAAABAHQD/////wEB/////wAAAAAVYIkK" +
           "AgAAAAEABQAAAEd5cm9aAQEoAAAvAQBACSgAAAAAC/////8BAf////8BAAAAFWCJCgIAAAAAAAcAAABF" +
           "VVJhbmdlAQEsAAAuAEQsAAAAAQB0A/////8BAf////8AAAAAFWCJCgIAAAABAAsAAABUZW1wZXJhdHVy" +
           "ZQEBLgAALwEAQAkuAAAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBMgAALgBE" +
           "MgAAAAEAdAP/////AQH/////AAAAABVgiQoCAAAAAQAGAAAAT25saW5lAQE0AAAvAQA9CTQAAAAAAf//" +
           "//8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets A description for the GyroX Variable.
        /// </summary>
        public AnalogItemState<double> GyroX
        {
            get
            {
                return m_gyroX;
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_gyroX, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_gyroX = value;
            }
        }

        /// <summary>
        /// Gets or sets A description for the GyroY Variable.
        /// </summary>
        public AnalogItemState<double> GyroY
        {
            get
            {
                return m_gyroY;
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_gyroY, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_gyroY = value;
            }
        }

        /// <summary>
        /// Gets or sets A description for the GyroZ Variable.
        /// </summary>
        public AnalogItemState<double> GyroZ
        {
            get
            {
                return m_gyroZ;
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_gyroZ, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_gyroZ = value;
            }
        }

        /// <summary>
        /// Gets or sets A description for the Temperature Variable.
        /// </summary>
        public AnalogItemState<double> Temperature
        {
            get
            {
                return m_temperature;
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_temperature, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_temperature = value;
            }
        }

        /// <summary>
        /// Gets or sets A description for the Online Variable.
        /// </summary>
        public DataItemState<bool> Online
        {
            get
            {
                return m_online;
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_online, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_online = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_gyroX != null)
            {
                children.Add(m_gyroX);
            }

            if (m_gyroY != null)
            {
                children.Add(m_gyroY);
            }

            if (m_gyroZ != null)
            {
                children.Add(m_gyroZ);
            }

            if (m_temperature != null)
            {
                children.Add(m_temperature);
            }

            if (m_online != null)
            {
                children.Add(m_online);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        /// <param name="context">The system context.</param>
        /// <param name="browseName">The browse name.</param>
        /// <param name="createOrReplace">A flag that specifies the action: to create or to replace.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>The instance state found; or NULL otherwise.</returns>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case ITG3200.BrowseNames.GyroX:
                {
                    if (createOrReplace)
                    {
                        if (GyroX == null)
                        {
                            if (replacement == null)
                            {
                                GyroX = new AnalogItemState<double>(this);
                            }
                            else
                            {
                                GyroX = (AnalogItemState<double>)replacement;
                            }
                        }
                    }

                    instance = GyroX;
                    break;
                }

                case ITG3200.BrowseNames.GyroY:
                {
                    if (createOrReplace)
                    {
                        if (GyroY == null)
                        {
                            if (replacement == null)
                            {
                                GyroY = new AnalogItemState<double>(this);
                            }
                            else
                            {
                                GyroY = (AnalogItemState<double>)replacement;
                            }
                        }
                    }

                    instance = GyroY;
                    break;
                }

                case ITG3200.BrowseNames.GyroZ:
                {
                    if (createOrReplace)
                    {
                        if (GyroZ == null)
                        {
                            if (replacement == null)
                            {
                                GyroZ = new AnalogItemState<double>(this);
                            }
                            else
                            {
                                GyroZ = (AnalogItemState<double>)replacement;
                            }
                        }
                    }

                    instance = GyroZ;
                    break;
                }

                case ITG3200.BrowseNames.Temperature:
                {
                    if (createOrReplace)
                    {
                        if (Temperature == null)
                        {
                            if (replacement == null)
                            {
                                Temperature = new AnalogItemState<double>(this);
                            }
                            else
                            {
                                Temperature = (AnalogItemState<double>)replacement;
                            }
                        }
                    }

                    instance = Temperature;
                    break;
                }

                case ITG3200.BrowseNames.Online:
                {
                    if (createOrReplace)
                    {
                        if (Online == null)
                        {
                            if (replacement == null)
                            {
                                Online = new DataItemState<bool>(this);
                            }
                            else
                            {
                                Online = (DataItemState<bool>)replacement;
                            }
                        }
                    }

                    instance = Online;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private AnalogItemState<double> m_gyroX;
        private AnalogItemState<double> m_gyroY;
        private AnalogItemState<double> m_gyroZ;
        private AnalogItemState<double> m_temperature;
        private DataItemState<bool> m_online;
        #endregion
    }
    #endif
    #endregion
}