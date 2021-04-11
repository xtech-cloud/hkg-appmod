

using System;
using System.Windows.Forms;
using HKG.Module.Metatable;
using XTC.oelMVCS;

namespace app
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RootForm rootForm = new RootForm();

            ConsoleLogger logger = new ConsoleLogger();
            logger.rtbLog = rootForm.getLoggerUi();
            Config config = new AppConfig();
            ConfigSchema schema = new ConfigSchema();
            schema.domain = rootForm.getDomainUi().Text;
            config.Merge(System.Text.Json.JsonSerializer.Serialize(schema));
            Framework framework = new Framework();
            framework.setLogger(logger);
            framework.setConfig(config);
            framework.Initialize();

            AppFacade appFacade = new AppFacade();
            appFacade.rootForm = rootForm;
            framework.getStaticPipe().RegisterFacade(AppFacade.NAME, appFacade);
            AppView appView = new AppView();
            framework.getStaticPipe().RegisterView(AppView.NAME, appView);

            // 注册模块窗体
            FormRoot formRoot = new FormRoot();
            formRoot.Inject(framework);
            formRoot.Register();
            // 注册模块逻辑
            ModuleRoot moduleRoot = new ModuleRoot();
            moduleRoot.Inject(framework);
            moduleRoot.Register();

            framework.Setup();

            Application.Run(rootForm);

            moduleRoot.Cancel();
            formRoot.Cancel();

            framework.Dismantle();
            framework.Release();
        }
    }
}
