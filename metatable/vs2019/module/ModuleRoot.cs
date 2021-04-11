
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
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

            // 注册数据层
            framework_.getStaticPipe().RegisterModel(SourceModel.NAME, new SourceModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(SourceView.NAME, new SourceView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(SourceController.NAME, new SourceController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(SourceService.NAME, new SourceService());
    
            // 注册数据层
            framework_.getStaticPipe().RegisterModel(VocabularyModel.NAME, new VocabularyModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(VocabularyView.NAME, new VocabularyView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(VocabularyController.NAME, new VocabularyController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(VocabularyService.NAME, new VocabularyService());
    
        }

        public void Cancel()
        {

            // 注销服务层
            framework_.getStaticPipe().CancelService(SourceService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(SourceController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(SourceView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(SourceModel.NAME);
    
            // 注销服务层
            framework_.getStaticPipe().CancelService(VocabularyService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(VocabularyController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(VocabularyView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(VocabularyModel.NAME);
    
        }

        private Framework framework_ = null;
    }
}
