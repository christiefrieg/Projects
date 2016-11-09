using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Interfaces
{
    public interface IViewModel
    {
        string Title { get; set; }
        string PartialViewPath { get; set; }
        void InitializePanel();
    }
}