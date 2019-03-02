using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Boilerplate.Feature.Navigation.Models
{
    public interface IHomeModel
    {
        Guid Id { get; }
        string Title { get; }
        string Content { get; }
    }
}