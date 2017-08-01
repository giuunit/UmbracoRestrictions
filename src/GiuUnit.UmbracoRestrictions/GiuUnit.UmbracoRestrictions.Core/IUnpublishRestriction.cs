using Umbraco.Core.Services;

namespace GiuUnit.UmbracoRestrictions.Core
{
    interface IUnpublishRestriction
    {
        void OnUnpublish(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e);
    }
}
