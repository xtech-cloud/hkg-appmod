
using XTC.oelMVCS;

namespace HKG.Module
{
    public class ModuleRoot
    {
        public ModuleRoot()
        {
        }

        public void Inject(Framework _framework)
        {
            framework_ = _framework;
        }

        public void Register()
        {
            // 注册视图层
            framework_.getStaticPipe().RegisterView(CrossView.NAME, new CrossView());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(CrossService.NAME, new CrossService());

            // 注册数据层
            framework_.getStaticPipe().RegisterModel(Metatable.FormatModel.NAME, new Metatable.FormatModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(Metatable.FormatView.NAME, new Metatable.FormatView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(Metatable.FormatController.NAME, new Metatable.FormatController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(Metatable.FormatService.NAME, new Metatable.FormatService());

            // 注册数据层
            framework_.getStaticPipe().RegisterModel(Metatable.SourceModel.NAME, new Metatable.SourceModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(Metatable.SourceView.NAME, new Metatable.SourceView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(Metatable.SourceController.NAME, new Metatable.SourceController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(Metatable.SourceService.NAME, new Metatable.SourceService());

            // 注册数据层
            framework_.getStaticPipe().RegisterModel(Metatable.SchemaModel.NAME, new Metatable.SchemaModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(Metatable.SchemaView.NAME, new Metatable.SchemaView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(Metatable.SchemaController.NAME, new Metatable.SchemaController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(Metatable.SchemaService.NAME, new Metatable.SchemaService());


            // 注册数据层
            framework_.getStaticPipe().RegisterModel(Metatable.VocabularyModel.NAME, new Metatable.VocabularyModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(Metatable.VocabularyView.NAME, new Metatable.VocabularyView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(Metatable.VocabularyController.NAME, new Metatable.VocabularyController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(Metatable.VocabularyService.NAME, new Metatable.VocabularyService());

            // 注册数据层
            framework_.getStaticPipe().RegisterModel(Collector.DocumentModel.NAME, new Collector.DocumentModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(Collector.DocumentView.NAME, new Collector.DocumentView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(Collector.DocumentController.NAME, new Collector.DocumentController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(Collector.DocumentService.NAME, new Collector.DocumentService());

        }

        public void Cancel()
        {
            // 注销服务层
            framework_.getStaticPipe().CancelService(Metatable.FormatService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(Metatable.FormatController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(Metatable.FormatView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(Metatable.FormatModel.NAME);

            // 注销服务层
            framework_.getStaticPipe().CancelService(Metatable.SchemaService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(Metatable.SchemaController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(Metatable.SchemaView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(Metatable.SchemaModel.NAME);

            // 注销服务层
            framework_.getStaticPipe().CancelService(Metatable.SourceService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(Metatable.SourceController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(Metatable.SourceView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(Metatable.SourceModel.NAME);
    
            // 注销服务层
            framework_.getStaticPipe().CancelService(Metatable.VocabularyService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(Metatable.VocabularyController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(Metatable.VocabularyView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(Metatable.VocabularyModel.NAME);

            // 注销服务层
            framework_.getStaticPipe().CancelService(Collector.DocumentService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(Collector.DocumentController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(Collector.DocumentView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(Collector.DocumentModel.NAME);

            // 注销服务层
            framework_.getStaticPipe().CancelService(CrossService.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(CrossView.NAME);

        }

        private Framework framework_ = null;
    }
}
