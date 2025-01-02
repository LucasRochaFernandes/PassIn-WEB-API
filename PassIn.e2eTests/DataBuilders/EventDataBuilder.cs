using Bogus;
using PassIn.Infrastructure.Entities;

namespace PassIn.e2eTests.DataBuilders;
public class EventDataBuilder : Faker<Event>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Details { get; set; } 
    public string? Slug { get; private set; } 
    public int? Maximum_Attendees { get; set; } 

    public EventDataBuilder() {
        RuleFor(e => e.Title, f => string.IsNullOrEmpty(Title) ? f.Lorem.Word() : Title);
        RuleFor(e => e.Details, f => string.IsNullOrEmpty(Details) ? f.Lorem.Paragraph(6) : Details);
        RuleFor(e => e.Maximum_Attendees, f=> Maximum_Attendees == null ? f.Random.Int(1, 10) : Maximum_Attendees);
    }

    public Event Build()
    {
        var eventEntity = Generate();
        eventEntity.Slug = eventEntity.Title.ToLower().Replace(" ", "-");
        return eventEntity;
    }
}
