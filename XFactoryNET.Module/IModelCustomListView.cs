using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module
{
    public interface IModelCustomListView : IModelListView
    {

        string LayoutSettings { get; set; }

    }
}
