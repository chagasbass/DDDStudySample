namespace OrganizaDespensa.SharedKernel.Core.Enums
{
    public class EventAction : Enumeration
    {
        public static EventAction INSERT { get; } = new EventAction(1, nameof(INSERT));
        public static EventAction UPDATE { get; } = new EventAction(1, nameof(UPDATE));
        public static EventAction DELETE { get; } = new EventAction(1, nameof(DELETE));

        public EventAction(int id,string name) : base(id, name) { }

    }
}
