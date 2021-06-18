
using System;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class VocabularyController : Controller
    {
        public const string NAME = "hkg.metatable.VocabularyController";

        private VocabularyView view { get; set; }

        protected override void preSetup()
        {
            view = findView(VocabularyView.NAME) as VocabularyView;
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyController");
        }
        public void List(Model.Status _reply, VocabularyModel.VocabularyStatus _status, Proto.VocabularyEntity[] _list)
        {
            _status.vocabulary.Clear();
            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
            }
            else
            {
                foreach (var v in _list)
                {
                    var source = new VocabularyModel.VocabularyStatus.Vocabulary();
                    source.entity = v;
                    _status.vocabulary.Add(source);
                }
            }
            view.RefreshList(_status.vocabulary);
        }

        public void Get(VocabularyModel.VocabularyStatus _status, string _uuid)
        {
            var Vocabulary = _status.vocabulary.Find((_item) =>
            {
                return _item.entity._uuid.AsString().Equals(_uuid);
            });
            if (null == Vocabulary)
            {
                view.Alert("Vocabulary not found");
                return;
            }
            view.RefreshOne(Vocabulary);
        }
    }
}
