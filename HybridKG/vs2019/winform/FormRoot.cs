
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

            registerMetatable(facadeCross);
            registerCollector(facadeCross);
            registerBuilder(facadeCross);
        }

        public void Cancel()
        {
            cancelMetatable();
            cancelCollector();
            cancelBuilder();

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(CrossFacade.NAME);
        }

        private void registerMetatable(CrossFacade _facadeCross)
        {
            // 注册UI装饰
            Metatable.FormatFacade facadeFormat = new Metatable.FormatFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.FormatFacade.NAME, facadeFormat);
            Metatable.FormatPanel panelFormat = new Metatable.FormatPanel();
            panelFormat.facade = facadeFormat;
            Metatable.FormatPanel.FormatUiBridge uiFormatBridge = new Metatable.FormatPanel.FormatUiBridge();
            uiFormatBridge.panel = panelFormat;
            facadeFormat.setUiBridge(uiFormatBridge);

            // 注册UI装饰
            Metatable.SourceFacade facadeSource = new Metatable.SourceFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.SourceFacade.NAME, facadeSource);
            Metatable.SourcePanel panelSource = new Metatable.SourcePanel();
            panelSource.facade = facadeSource;
            Metatable.SourcePanel.SourceUiBridge uiSourceBridge = new Metatable.SourcePanel.SourceUiBridge();
            uiSourceBridge.panel = panelSource;
            facadeSource.setUiBridge(uiSourceBridge);

            // 注册UI装饰
            Metatable.SchemaFacade facadeSchema = new Metatable.SchemaFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.SchemaFacade.NAME, facadeSchema);
            Metatable.SchemaPanel panelSchema = new Metatable.SchemaPanel();
            panelSchema.facade = facadeSchema;
            Metatable.SchemaPanel.SchemaUiBridge uiSchemaBridge = new Metatable.SchemaPanel.SchemaUiBridge();
            uiSchemaBridge.panel = panelSchema;
            facadeSchema.setUiBridge(uiSchemaBridge);

            // 注册UI装饰
            Metatable.VocabularyFacade facadeVocabulary = new Metatable.VocabularyFacade();
            framework_.getStaticPipe().RegisterFacade(Metatable.VocabularyFacade.NAME, facadeVocabulary);
            Metatable.VocabularyPanel panelVocabulary = new Metatable.VocabularyPanel();
            panelVocabulary.facade = facadeVocabulary;
            Metatable.VocabularyPanel.VocabularyUiBridge uiVocabularyBridge = new Metatable.VocabularyPanel.VocabularyUiBridge();
            uiVocabularyBridge.panel = panelVocabulary;
            facadeVocabulary.setUiBridge(uiVocabularyBridge);
        }

        private void cancelMetatable()
        {
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Metatable.SourceFacade.NAME);

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Metatable.SchemaFacade.NAME);

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Metatable.VocabularyFacade.NAME);

        }

        private void registerCollector(CrossFacade _facadeCross)
        {
            // 注册UI装饰
            Collector.DocumentFacade facadeDocument = new Collector.DocumentFacade();
            framework_.getStaticPipe().RegisterFacade(Collector.DocumentFacade.NAME, facadeDocument);
            Collector.DocumentPanel panelDocument = new Collector.DocumentPanel();
            panelDocument.facade = facadeDocument;
            panelDocument.crossFacade = _facadeCross;
            Collector.DocumentPanel.DocumentUiBridge uiDocumentBridge = new Collector.DocumentPanel.DocumentUiBridge();
            uiDocumentBridge.panel = panelDocument;
            facadeDocument.setUiBridge(uiDocumentBridge);
        }

        private void cancelCollector()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Collector.DocumentFacade.NAME);

        }


        private void registerBuilder(CrossFacade _facadeCross)
        {

            // 注册UI装饰
            Builder.DocumentFacade facadeDocument = new Builder.DocumentFacade();
            framework_.getStaticPipe().RegisterFacade(Builder.DocumentFacade.NAME, facadeDocument);
            Builder.DocumentPanel panelDocument = new Builder.DocumentPanel();
            panelDocument.facade = facadeDocument;
            panelDocument.crossFacade = _facadeCross;
            Builder.DocumentPanel.DocumentUiBridge uiDocumentBridge = new Builder.DocumentPanel.DocumentUiBridge();
            uiDocumentBridge.panel = panelDocument;
            facadeDocument.setUiBridge(uiDocumentBridge);
        }

        private void cancelBuilder()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(Builder.DocumentFacade.NAME);

        }


        private Framework framework_ = null;
    }
}
