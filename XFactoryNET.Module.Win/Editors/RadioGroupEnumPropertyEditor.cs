using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.ExpressApp.Editors;

namespace XFactoryNET.Module.Win.Editors
{
    [PropertyEditor(typeof(Enum), false)]
    public class RadioGroupEnumPropertyEditor : EnumPropertyEditor
    {
        EnumDescriptor enumDescriptor;
        object noneValue;

        public RadioGroupEnumPropertyEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ImmediatePostData = model.ImmediatePostData;
        }

        protected override object CreateControlCore()
        {
            enumDescriptor = new EnumDescriptor(GetUnderlyingType());
            RadioGroup radioGroup = new RadioGroup();
            foreach (object enumValue in enumDescriptor.Values)
                radioGroup.Properties.Items.Add(new RadioGroupItem(enumValue, enumDescriptor.GetCaption(enumValue)));
            return (object)radioGroup;
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            return (RepositoryItem)new RepositoryItemRadioGroup();
        }

        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            base.SetupRepositoryItem(item);
            if (TypeHasFlagsAttribute())
            {
                enumDescriptor = new EnumDescriptor(GetUnderlyingType());
                var radioGroup = ((RepositoryItemRadioGroup)item);
                radioGroup.BeginUpdate();
                radioGroup.Items.Clear();
                noneValue = GetNoneValue();
                foreach (object value in enumDescriptor.Values)
                    if (!IsNoneValue(value))
                        radioGroup.Items.Add((RadioGroupItem)value);
                radioGroup.EndUpdate();
                radioGroup.ParseEditValue += checkedEdit_ParseEditValue;
                radioGroup.CustomDisplayText += checkedItem_CustomDisplayText;
            }
        }

        bool TypeHasFlagsAttribute()
        {
            return GetUnderlyingType().GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0;
        }

        bool IsNoneValue(object value)
        {
            if (value is string) return false;
            int result = int.MinValue;
            try
            {
                result = Convert.ToInt32(value);
            }
            catch
            {
            }
            return 0.Equals(result);
        }


        object GetNoneValue()
        {
            return Enum.ToObject(GetUnderlyingType(), 0);
        }

        void checkedEdit_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(e.Value)))
            {
                ((RadioGroup)sender).EditValue = noneValue;
                e.Handled = true;
            }
        }


        void checkedItem_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (!IsNoneValue(e.Value) || enumDescriptor == null) return;
            e.DisplayText = enumDescriptor.GetCaption(e.Value);
        }
    }
}
