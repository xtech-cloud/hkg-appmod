
using XTC.oelMVCS;

namespace HKG.Module
{
    public class FormRoot
    {
        public FormRoot()
        {
        }

        public void Inject(Framework _framework)
        {
            framework_ = _framework;
        }

        public void Register()
        {
            // 注册UI装饰
            CrossFacade facadeCross = new CrossFacade();
            framework_.getStaticPipe().RegisterFacade(CrossFacade.NAME, facadeCross);
   

            // 注册UI装饰
            Metatable.SourceFacade facadeSource = new Metatable.SourceFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.SourceFacade.NAME, facadeSource);
            Metatable.SourcePanel panelSource = new Metatable.SourcePanel();
            panelSource.facade = facadeSource;
            Metatable.SourcePanel.SourceUiBridge uiSourceBridge = new Metatable.SourcePanel.SourceUiBridge();
            uiSourceBridge.panel = panelSource;
            facadeSource.setUiBridge(uiSourceBridge);

            // 注册UI装饰
            Metatable.VocabularyFacade facadeVocabulary = new Metatable.VocabularyFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.VocabularyFacade.NAME, facadeVocabulary);
            Metatable.VocabularyPanel panelVocabulary = new Metatable.VocabularyPanel();
            panelVocabulary.facade = facadeVocabulary;
            Metatable.VocabularyPanel.VocabularyUiBridge uiVocabularyBridge = new Metatable.VocabularyPanel.VocabularyUiBridge();
            uiVocabularyBridge.panel = panelVocabulary;
            facadeVocabulary.setUiBridge(uiVocabularyBridge);

            // 注册UI装饰
            Collector.DocumentFacade facadeDocument = new Collector.DocumentFacade();
            framework_.getStaticPipe().RegisterFacade(Collector.DocumentFacade.NAME, facadeDocument);
            Collector.DocumentPanel panelDocument = new Collector.DocumentPanel();
            panelDocument.facade = facadeDocument;
            panelDocument.crossFacade = facadeCross;
            Collector.DocumentPanel.DocumentUiBridge uiDocumentBridge = new Collector.DocumentPanel.DocumentUiBridge();
            uiDocumentBridge.panel = panelDocument;
            facadeDocument.setUiBridge(uiDocumentBridge);

        }

        public void Cancel()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Metatable.SourceFacade.NAME);
        
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Metatable.VocabularyFacade.NAME);

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Collector.DocumentFacade.NAME);

        }

        private Framework framework_ = null;
    }
}
