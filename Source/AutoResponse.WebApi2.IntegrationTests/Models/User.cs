namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System;

    public class User
    {
        public User(Guid id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
        }

        public Guid Id { get; }

        public string UserName { get; }
    }
}