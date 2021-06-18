
using XTC.oelMVCS;

namespace wpf
{
}

namespace hkg.metatable
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
            FormatFacade facadeFormat = new FormatFacade();
            framework_.getStaticPipe().RegisterFacade(FormatFacade.NAME, facadeFormat);
            FormatControl controlFormat = new FormatControl();
            controlFormat.facade = facadeFormat;
            FormatControl.FormatUiBridge uiFormatBridge = new FormatControl.FormatUiBridge();
            uiFormatBridge.control = controlFormat;
            facadeFormat.setUiBridge(uiFormatBridge);
        
            // 注册UI装饰
            SchemaFacade facadeSchema = new SchemaFacade();
            framework_.getStaticPipe().RegisterFacade(SchemaFacade.NAME, facadeSchema);
            SchemaControl controlSchema = new SchemaControl();
            controlSchema.facade = facadeSchema;
            SchemaControl.SchemaUiBridge uiSchemaBridge = new SchemaControl.SchemaUiBridge();
            uiSchemaBridge.control = controlSchema;
            facadeSchema.setUiBridge(uiSchemaBridge);
        
            // 注册UI装饰
            SourceFacade facadeSource = new SourceFacade();
            framework_.getStaticPipe().RegisterFacade(SourceFacade.NAME, facadeSource);
            SourceControl controlSource = new SourceControl();
            controlSource.facade = facadeSource;
            SourceControl.SourceUiBridge uiSourceBridge = new SourceControl.SourceUiBridge();
            uiSourceBridge.control = controlSource;
            facadeSource.setUiBridge(uiSourceBridge);
        
            // 注册UI装饰
            VocabularyFacade facadeVocabulary = new VocabularyFacade();
            framework_.getStaticPipe().RegisterFacade(VocabularyFacade.NAME, facadeVocabulary);
            VocabularyControl controlVocabulary = new VocabularyControl();
            controlVocabulary.facade = facadeVocabulary;
            VocabularyControl.VocabularyUiBridge uiVocabularyBridge = new VocabularyControl.VocabularyUiBridge();
            uiVocabularyBridge.control = controlVocabulary;
            facadeVocabulary.setUiBridge(uiVocabularyBridge);
        
        }

        public void Cancel()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(FormatFacade.NAME);
        
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(SchemaFacade.NAME);
        
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(SourceFacade.NAME);
        
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(VocabularyFacade.NAME);
        
        }

        private Framework framework_ = null;
    }
}
