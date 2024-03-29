
using System.Windows;
using XTC.oelMVCS;
using hkg.collector;
using System.Collections.Generic;

namespace app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Framework framework_ { get; set; }
        private ConsoleLogger logger_ { get; set; }
        private Config config_ { get; set; }
        private BlankModel blankModel { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 静态管线注册组件
            registerMVCS();

            ModuleRoot moduleRoot = new ModuleRoot();
            moduleRoot.Inject(framework_);
            moduleRoot.Register();
            ControlRoot controlRoot = new ControlRoot();
            controlRoot.Inject(framework_);
            controlRoot.Register();
            framework_.Setup();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            logger_ = new ConsoleLogger();
            config_ = new AppConfig();

            ConfigSchema config = new ConfigSchema();
            string domain_public = System.Environment.GetEnvironmentVariable("DOMAIN_PUBLIC");
            if (!string.IsNullOrEmpty(domain_public))
            {
                config.domain_public = domain_public;
            }
            string domain_private = System.Environment.GetEnvironmentVariable("DOMAIN_PRIVATE");
            if (!string.IsNullOrEmpty(domain_private))
            {
                config.domain_private = domain_private;
            }
            config_.Merge(System.Text.Json.JsonSerializer.Serialize(config));

            MainWindow mainWindow = new MainWindow();
            this.MainWindow = mainWindow;
            logger_.rtbLog = mainWindow.rtbLog;
            mainWindow.Show();

            framework_ = new Framework();
            framework_.setLogger(logger_);
            framework_.setConfig(config_);
            framework_.Initialize();

            base.OnStartup(e);

            Dictionary<string, Any> data = new Dictionary<string, Any>();
            data["accessToken"] = Any.FromString("");
            data["uuid"] = Any.FromString("");
            data["host"] = Any.FromString(domain_private);
            data["location"] = Any.FromString("private");
            blankModel.Broadcast("/Application/Auth/Signin/Success", data);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            framework_.Release();
            framework_ = null;
        }

        private void registerMVCS()
        {
            blankModel = new BlankModel();
            framework_.getStaticPipe().RegisterModel(BlankModel.NAME, blankModel);

            AppView appView = new AppView();
            framework_.getStaticPipe().RegisterView(AppView.NAME, appView);
        }
    }
}

