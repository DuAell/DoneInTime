using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Reflection;
using System.ComponentModel;

namespace DoneInTime.ViewModel
{
    abstract public class ViewModelBase : DynamicObject
    {
        public object WrappedDomainObject;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            string propertyName = binder.Name;
            PropertyInfo property = this.WrappedDomainObject.GetType().GetProperty(propertyName);

            if (property == null || property.CanRead == false)
            {
                result = null;
                return false;
            }

            result = property.GetValue(this.WrappedDomainObject, null);
            return true;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {

            string propertyName = binder.Name;
            PropertyInfo property = this.WrappedDomainObject.GetType().GetProperty(propertyName);

            if (property == null || property.CanWrite == false)
                return false;

            property.SetValue(this.WrappedDomainObject, value, null);

            NotifyPropertyChanged(propertyName);
            return true;
        }

        #region "Events"
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
