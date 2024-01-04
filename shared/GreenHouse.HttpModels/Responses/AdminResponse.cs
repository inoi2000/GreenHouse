
namespace GreenHouse.HttpModels.Responses
{
    public class AdminResponse
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;

        public AdminResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
