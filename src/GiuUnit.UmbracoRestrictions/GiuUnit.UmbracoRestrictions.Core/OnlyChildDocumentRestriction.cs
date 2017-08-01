using System.Linq;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;

namespace GiuUnit.UmbracoRestrictions.Core
{
    public class OnlyChildDocumentRestriction : IRestriction
    {
        private RestrictionsConfigRoot _config;
        private const string ruleName = "onlyChildDocument";

        public OnlyChildDocumentRestriction(RestrictionsConfigRoot config)
        {
            _config = config;
        }

        public void OnPublish(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            var rule = _config.RulesNode.RuleList.FirstOrDefault(x => x.Name == ruleName);

            if (rule == null)
                return;

            var aliases = rule.AddList.Select(x => x.DocTypeAlias);

            if (!aliases.Any())
                return;

            //one of the documents we publish has a restriction
            var restrictedEntities = e.PublishedEntities.Where(x => aliases.Contains(x.ContentType.Alias));

            if (!restrictedEntities.Any())
                return;

            foreach (var entity in restrictedEntities)
            {
                //element at the root level, we ignore, rootSingletonDoc restriction already available for that
                if (entity.ParentId == -1)
                    continue;

                //umbraco context is already defined as we are in one of the pluggins
                var siblingsAliases = Umbraco.Web.UmbracoContext.Current.ContentCache.GetById(entity.ParentId).Children.Select(x => x.DocumentTypeAlias);

                //any siblings with the same doc type ? 
                var cachedDocumentsCondition = restrictedEntities.Any(x => siblingsAliases.Contains(x.ContentType.Alias));

                //documents with the restriction have been found
                if (cachedDocumentsCondition)
                {
                    e.CancelOperation(new EventMessage("Content Restriction", rule.ErrorMessage, EventMessageType.Error));
                }
            }
        }
    }
}
