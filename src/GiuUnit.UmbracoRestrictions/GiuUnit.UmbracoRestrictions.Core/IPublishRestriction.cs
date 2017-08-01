using Umbraco.Core.Models;

namespace GiuUnit.UmbracoRestrictions.Core
{
    interface IPublishRestriction
    {
        void OnPublish(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<IContent> e);
    }
}
