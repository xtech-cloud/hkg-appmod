
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.builder
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
            framework_.getStaticPipe().RegisterModel(DocumentModel.NAME, new DocumentModel());
            // 注册视图层
            framework_.getStaticPipe().RegisterView(DocumentView.NAME, new DocumentView());
            // 注册控制层
            framework_.getStaticPipe().RegisterController(DocumentController.NAME, new DocumentController());
            // 注册服务层
            framework_.getStaticPipe().RegisterService(DocumentService.NAME, new DocumentService());
    
            framework_.getStaticPipe().RegisterModel(QueryModel.NAME, new QueryModel());
        }

        public void Cancel()
        {

            // 注销服务层
            framework_.getStaticPipe().CancelService(DocumentService.NAME);
            // 注销控制层
            framework_.getStaticPipe().CancelController(DocumentController.NAME);
            // 注销视图层
            framework_.getStaticPipe().CancelView(DocumentView.NAME);
            // 注销数据层
            framework_.getStaticPipe().CancelModel(DocumentModel.NAME);
    
            framework_.getStaticPipe().CancelModel(QueryModel.NAME);
        }

        private Framework framework_ = null;
    }
}
