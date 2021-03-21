namespace OrganizaDespensa.Extensions.Entities
{
    public  class ProblemDetail
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int HttpCode { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
        public string Instance { get; set; }
        public string Link { get; set; }
        public string DataType { get; set; }

        public ProblemDetail()
        {
            DataType = @"application/problem+json";
        }
    }
}
