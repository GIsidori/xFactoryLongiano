// Developer Express Code Central Example:
// How to store users' model differences separately for each user in the database
// 
// This example illustrates how to store users' model differences separately for
// each user in an XAF Windows Forms application. The complete description is
// available in the How to: Store Model Differences in Database
// (http://documentation.devexpress.com/#Xaf/CustomDocument3337) topic.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E968

using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security.Strategy;

namespace XFactoryNET.Module
{
    [Browsable(false)]
    public abstract class ModelDiffsBase : BaseObject {
        public ModelDiffsBase(Session session) : base(session) { }

        [Association("ModelDiffs-Aspects"), Aggregated]
        public XPCollection<ModelAspect> Aspects {
            get { return GetCollection<ModelAspect>("Aspects"); }
        }
    }

    [Browsable(false)]
    public class ModelDiffs : ModelDiffsBase {
        public ModelDiffs(Session session) : base(session) { }
    }

    [Browsable(false)]
    public class ModelUserDiffs : ModelDiffsBase {
        public ModelUserDiffs(Session session) : base(session) { }

        public SecuritySystemUserBase User
        {
            get { return GetPropertyValue<SecuritySystemUserBase>("User"); }
            set { SetPropertyValue("User", value); }
        }
    }

    [Browsable(false)]
    public class ModelAspect : BaseObject {
        public ModelAspect(Session session) : base(session) { }

        [Association("ModelDiffs-Aspects")]
        public ModelDiffsBase ModelDifferences {
            get { return GetPropertyValue<ModelDiffsBase>("ModelDifferences"); }
            set { SetPropertyValue("ModelDifferences", value); }
        }
        public string Aspect {
            get { return GetPropertyValue<string>("Aspect"); }
            set { SetPropertyValue<string>("Aspect", value); }
        }

        [Size(SizeAttribute.Unlimited)]
        public string XmlData {
            get { return GetPropertyValue<string>("XmlData"); }
            set { SetPropertyValue("XmlData", value); }
        }
    }
}
