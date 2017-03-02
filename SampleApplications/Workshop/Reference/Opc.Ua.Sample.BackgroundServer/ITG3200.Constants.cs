
using System;


namespace Opc.Ua.Sample.BackgroundServer.ITG3200
{
    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    internal static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the ITG3200Type ObjectType.
        /// </summary>
        public const uint ITG3200Type = 27;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    internal static partial class Variables
    {
        /// <summary>
        /// The identifier for the ITG3200Type_GyroX Variable.
        /// </summary>
        public const uint ITG3200Type_GyroX = 28;

        /// <summary>
        /// The identifier for the ITG3200Type_GyroX_EURange Variable.
        /// </summary>
        public const uint ITG3200Type_GyroX_EURange = 32;

        /// <summary>
        /// The identifier for the ITG3200Type_GyroY Variable.
        /// </summary>
        public const uint ITG3200Type_GyroY = 34;

        /// <summary>
        /// The identifier for the ITG3200Type_GyroY_EURange Variable.
        /// </summary>
        public const uint ITG3200Type_GyroY_EURange = 38;

        /// <summary>
        /// The identifier for the ITG3200Type_GyroZ Variable.
        /// </summary>
        public const uint ITG3200Type_GyroZ = 40;

        /// <summary>
        /// The identifier for the ITG3200Type_GyroZ_EURange Variable.
        /// </summary>
        public const uint ITG3200Type_GyroZ_EURange = 44;

        /// <summary>
        /// The identifier for the ITG3200Type_Temperature Variable.
        /// </summary>
        public const uint ITG3200Type_Temperature = 46;

        /// <summary>
        /// The identifier for the ITG3200Type_Temperature_EURange Variable.
        /// </summary>
        public const uint ITG3200Type_Temperature_EURange = 50;

        /// <summary>
        /// The identifier for the ITG3200Type_Online Variable.
        /// </summary>
        public const uint ITG3200Type_Online = 52;
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    internal static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the ITG3200Type ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type = new ExpandedNodeId(ITG3200.ObjectTypes.ITG3200Type, ITG3200.Namespaces.ITG3200);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    internal static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the ITG3200Type_GyroX Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroX = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroX, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_GyroX_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroX_EURange = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroX_EURange, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_GyroY Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroY = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroY, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_GyroY_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroY_EURange = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroY_EURange, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_GyroZ Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroZ = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroZ, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_GyroZ_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_GyroZ_EURange = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_GyroZ_EURange, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_Temperature Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_Temperature = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_Temperature, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_Temperature_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_Temperature_EURange = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_Temperature_EURange, ITG3200.Namespaces.ITG3200);

        /// <summary>
        /// The identifier for the ITG3200Type_Online Variable.
        /// </summary>
        public static readonly ExpandedNodeId ITG3200Type_Online = new ExpandedNodeId(ITG3200.Variables.ITG3200Type_Online, ITG3200.Namespaces.ITG3200);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    internal static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the GyroX component.
        /// </summary>
        public const string GyroX = "GyroX";

        /// <summary>
        /// The BrowseName for the GyroY component.
        /// </summary>
        public const string GyroY = "GyroY";

        /// <summary>
        /// The BrowseName for the GyroZ component.
        /// </summary>
        public const string GyroZ = "GyroZ";

        /// <summary>
        /// The BrowseName for the ITG3200Type component.
        /// </summary>
        public const string ITG3200Type = "ITG3200Type";

        /// <summary>
        /// The BrowseName for the Online component.
        /// </summary>
        public const string Online = "Online";

        /// <summary>
        /// The BrowseName for the Temperature component.
        /// </summary>
        public const string Temperature = "Temperature";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    internal static partial class Namespaces
    {

        public const string OpcUa = "http://opcfoundation.org/UA/";

        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";

        /// <summary>
        /// The URI for the ITG3200 namespace (.NET code namespace is 'ITG3200').
        /// </summary>
        public const string ITG3200 = "http://opcfoundation.org/UA/ITG3200";
    }
    #endregion
}