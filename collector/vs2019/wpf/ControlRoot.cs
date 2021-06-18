
using XTC.oelMVCS;

namespace wpf
{
}

namespace hkg.collector
{
    public class ControlRoot
    {
        public ControlRoot()
        {
        }

        public void Inject(Framework _framework)
        {
            framework_ = _framework;
        }

        public void Register()
        {

            // 注册UI装饰
            DocumentFacade facadeDocument = new DocumentFacade();
            framework_.getStaticPipe().RegisterFacade(DocumentFacade.NAME, facadeDocument);
            DocumentControl controlDocument = new DocumentControl();
            controlDocument.facade = facadeDocument;
            DocumentControl.DocumentUiBridge uiDocumentBridge = new DocumentControl.DocumentUiBridge();
            uiDocumentBridge.control = controlDocument;
            facadeDocument.setUiBridge(uiDocumentBridge);
        
        }

        public void Cancel()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(DocumentFacade.NAME);
        
        }

        private Framework framework_ = null;
    }
}
