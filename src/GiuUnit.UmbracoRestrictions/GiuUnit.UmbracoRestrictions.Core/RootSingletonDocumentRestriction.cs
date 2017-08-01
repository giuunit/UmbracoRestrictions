using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using System.Linq;

namespace GiuUnit.UmbracoRestrictions.Core
{
    public class RootSingletonDocumentRestriction : IRestriction
    {
        private RestrictionsConfigRoot _config;
        private const string ruleName = "rootSingletonDocument";

        public RootSingletonDocumentRestriction(RestrictionsConfigRoot config)
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
                //element not at the root level
                if (entity.ParentId != -1)
                    continue;

                //umbraco context is already defined as we are in one of the pluggins
                var siblingsAliases = Umbraco.Web.UmbracoContext.Current.ContentCache.GetAtRoot()
                    .Where(x => x.Id != entity.Id)
                    .Select(x => x.DocumentTypeAlias)
                    .ToList();

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
