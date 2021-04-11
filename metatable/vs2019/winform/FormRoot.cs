
using XTC.oelMVCS;

namespace HKG.Module.Metatable
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
            SourceFacade facadeSource = new SourceFacade();
            framework_.getStaticPipe().RegisterFacade(SourceFacade.NAME, facadeSource);
            SourcePanel panelSource = new SourcePanel();
            panelSource.facade = facadeSource;
            SourcePanel.SourceUiBridge uiSourceBridge = new SourcePanel.SourceUiBridge();
            uiSourceBridge.panel = panelSource;
            facadeSource.setUiBridge(uiSourceBridge);
        
            // 注册UI装饰
            VocabularyFacade facadeVocabulary = new VocabularyFacade();
            framework_.getStaticPipe().RegisterFacade(VocabularyFacade.NAME, facadeVocabulary);
            VocabularyPanel panelVocabulary = new VocabularyPanel();
            panelVocabulary.facade = facadeVocabulary;
            VocabularyPanel.VocabularyUiBridge uiVocabularyBridge = new VocabularyPanel.VocabularyUiBridge();
            uiVocabularyBridge.panel = panelVocabulary;
            facadeVocabulary.setUiBridge(uiVocabularyBridge);
        
        }

        public void Cancel()
        {

            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(SourceFacade.NAME);
        
            // 注销UI装饰
            framework_.getStaticPipe().CancelFacade(VocabularyFacade.NAME);
        
        }

        private Framework framework_ = null;
    }
}
