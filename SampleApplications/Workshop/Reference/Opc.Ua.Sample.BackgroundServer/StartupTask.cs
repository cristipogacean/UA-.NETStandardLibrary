using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel.Background;
using Opc.Ua.Configuration;

namespace Opc.Ua.Sample.BackgroundServer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            deferral = taskInstance.GetDeferral();

            // helper to let Opc.Ua Utils find the localFolder in the environment
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            Utils.DefaultLocalFolder = localFolder.Path;

            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationName = "Opc.Ua.Sample.BackgroundServer";
            application.ApplicationType = ApplicationType.Server;
            application.ConfigSectionName = application.ApplicationName;

            string applicationConfigurationFile = localFolder.Path + String.Format(@"\{0}.Config.xml", application.ApplicationName);
            bool applicationConfigurationLoadedFromFile = false;
            try
            {
                await application.LoadApplicationConfiguration(applicationConfigurationFile, true);

                // check if the configuration is valid for a server application.
                await application.ApplicationConfiguration.Validate(ApplicationType.Server);
                applicationConfigurationLoadedFromFile = true;
            }
            catch (Exception ex)
            {
                Utils.Trace("Exception:" + ex.Message);
            }

            if (!applicationConfigurationLoadedFromFile)
            {
                // Set configuartion parameters
                application.ApplicationConfiguration = new ApplicationConfiguration();


                //  Enable tracing 
                application.ApplicationConfiguration.TraceConfiguration = new TraceConfiguration();
                application.ApplicationConfiguration.TraceConfiguration.OutputFilePath = localFolder.Path + @"\logs\Opc.Ua.Sample.BackgroundServer.log.txt";
                application.ApplicationConfiguration.TraceConfiguration.DeleteOnLoad = true;
                application.ApplicationConfiguration.TraceConfiguration.TraceMasks = 523;
                application.ApplicationConfiguration.TraceConfiguration.ApplySettings();

                // application information.
                application.ApplicationConfiguration.ApplicationName = application.ApplicationName;
                application.ApplicationConfiguration.ApplicationUri = String.Format("urn:{0}:OPCFoundation:Sample:BackgroundServer", System.Net.Dns.GetHostName());
                application.ApplicationConfiguration.ProductUri = "http://opcfoundation.org/UA/Sample/BackgroundServer";
                application.ApplicationConfiguration.ApplicationType = ApplicationType.Server;

                application.ApplicationConfiguration.SecurityConfiguration = new SecurityConfiguration();
                application.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate = new CertificateIdentifier();
                application.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate.StorePath = localFolder.Path + @"\pki\own";
                application.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate.SubjectName = application.ApplicationName;
                application.ApplicationConfiguration.SecurityConfiguration.TrustedPeerCertificates.StorePath = localFolder.Path + @"\pki\trusted";
                application.ApplicationConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StorePath = localFolder.Path + @"\pki\issuer";
                application.ApplicationConfiguration.SecurityConfiguration.RejectedCertificateStore = new CertificateStoreIdentifier();
                application.ApplicationConfiguration.SecurityConfiguration.RejectedCertificateStore.StorePath = localFolder.Path + @"\pki\rejected";
                application.ApplicationConfiguration.SecurityConfiguration.AutoAcceptUntrustedCertificates = true;

                // server configuration data
                application.ApplicationConfiguration.ServerConfiguration = new ServerConfiguration();
                application.ApplicationConfiguration.ServerConfiguration.BaseAddresses.Add(String.Format("opc.tcp://{0}:51210/UA/Sample/BackgroundServer", System.Net.Dns.GetHostName()));
                application.ApplicationConfiguration.ServerConfiguration.BaseAddresses.Add(String.Format("https://{0}:51212/UA/Sample/BackgroundServer", System.Net.Dns.GetHostName()));

                System.Net.IPAddress[] addresses = await System.Net.Dns.GetHostAddressesAsync(System.Net.Dns.GetHostName());

                foreach (var address in addresses)
                {
                    if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                        continue;
                    if (System.Net.IPAddress.IsLoopback(address))
                        continue;

                    application.ApplicationConfiguration.ServerConfiguration.AlternateBaseAddresses.Add(String.Format("opc.tcp://{0}:51210/UA/Sample/BackgroundServer", address.ToString()));
                    application.ApplicationConfiguration.ServerConfiguration.AlternateBaseAddresses.Add(String.Format("https://{0}:51212/UA/Sample/BackgroundServer", address.ToString()));
                }

                application.ApplicationConfiguration.ServerConfiguration.DiagnosticsEnabled = true;

                //  endpoint's security policies to expose
                application.ApplicationConfiguration.ServerConfiguration.SecurityPolicies = new ServerSecurityPolicyCollection();
                application.ApplicationConfiguration.ServerConfiguration.SecurityPolicies.Add(new ServerSecurityPolicy()
                {
                    SecurityLevel = 1,
                    SecurityMode = MessageSecurityMode.None,
                    SecurityPolicyUri = "http://opcfoundation.org/UA/SecurityPolicy#None"
                });
                application.ApplicationConfiguration.ServerConfiguration.SecurityPolicies.Add(new ServerSecurityPolicy()
                {
                    SecurityLevel = 2,
                    SecurityMode = MessageSecurityMode.Sign,
                    SecurityPolicyUri = "http://opcfoundation.org/UA/SecurityPolicy#Basic256"
                });
                application.ApplicationConfiguration.ServerConfiguration.SecurityPolicies.Add(new ServerSecurityPolicy()
                {
                    SecurityLevel = 3,
                    SecurityMode = MessageSecurityMode.SignAndEncrypt,
                    SecurityPolicyUri = "http://opcfoundation.org/UA/SecurityPolicy#Basic128Rsa15"
                });
                application.ApplicationConfiguration.ServerConfiguration.SecurityPolicies.Add(new ServerSecurityPolicy()
                {
                    SecurityLevel = 3,
                    SecurityMode = MessageSecurityMode.SignAndEncrypt,
                    SecurityPolicyUri = "http://opcfoundation.org/UA/SecurityPolicy#Basic256Sha256"
                });


                //  endpoint user token policies to expose
                application.ApplicationConfiguration.ServerConfiguration.UserTokenPolicies = new UserTokenPolicyCollection();
                application.ApplicationConfiguration.ServerConfiguration.UserTokenPolicies.Add(new UserTokenPolicy() { TokenType = UserTokenType.Anonymous });
                application.ApplicationConfiguration.ServerConfiguration.UserTokenPolicies.Add(new UserTokenPolicy() { TokenType = UserTokenType.UserName });

                application.ApplicationConfiguration.DisableHiResClock = true;

            }

            // Allow the current window to activate since the stack initialization below can take some time
            // and the app can be terminated by the runtime if this takes too long

            //  await Task.Delay(1);

            try
            {
                if (!applicationConfigurationLoadedFromFile)
                { 
                    // check if the configuration is valid for a server application.
                    await application.ApplicationConfiguration.Validate(ApplicationType.Server);
                }

                // check the application certificate.
                await application.CheckApplicationInstanceCertificate(false, 0);

                if (!application.ApplicationConfiguration.SecurityConfiguration.AutoAcceptUntrustedCertificates)
                {
                    application.ApplicationConfiguration.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
                }

                if (!applicationConfigurationLoadedFromFile)
                {
                    application.ApplicationConfiguration.SaveToFile(applicationConfigurationFile);
                }

                // start the server.
                await application.Start(new Opc.Ua.Sample.BackgroundServer.BackgroundServer());

            }
            catch (Exception ex)
            {
                Utils.Trace("Exception:" + ex.Message);
                deferral.Complete();
            }
        }

        #region Event Handlers
        /// <summary>
        /// Handles a certificate validation error.
        /// </summary>
        void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {

        }
        #endregion
    }


}

