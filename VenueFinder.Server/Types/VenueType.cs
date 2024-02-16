using VenueFinder.Domain.Entities;

namespace VenueFinder.API.Types
{
    public class VenueType : ObjectType<Venue>
    {
        protected override void Configure(IObjectTypeDescriptor<Venue> descriptor)
        {
            descriptor.Field(t => t.Id).Type<StringType>();
            descriptor.Field(t => t.Name).Type<StringType>();
            descriptor.Field(t => t.Category).Type<StringType>();
            descriptor.Field(t => t.LastUpdated).Ignore();
            descriptor.Field(t => t.GeolocationDegrees).Type<StringType>();
        }
    }
}
