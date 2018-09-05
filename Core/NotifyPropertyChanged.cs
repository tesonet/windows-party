using FluentValidation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using static System.String;

namespace Core
{
    public class NotifyPropertyChanged : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        private IValidator _validator;
        #endregion

        protected void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                if (propertyName == null)
                {
                    propertyName = String.Empty;
                }

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            var expression = property.Body as MemberExpression;
            if (expression != null)
            {
                var member = expression.Member;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public bool IsValid => IsNullOrEmpty(Error);


        public void Validate()
        {
            if (_validator == null)
            {
                _validator = CreateValidator();
            }
            if (_validator != null)
            {

                var validationResults = _validator.Validate(this);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);

                }
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                if (_validator == null)
                {
                    _validator = CreateValidator();
                }
                if (_validator != null)
                {

                    var validationResults = _validator.Validate(this);

                    if (!validationResults.IsValid)
                    {
                        return Join(Environment.NewLine, validationResults.Errors.Where(x => x.PropertyName == propertyName).Select(x => x.ErrorMessage));
                    }
                }

                return null;
            }

        }

        protected virtual IValidator CreateValidator()
        {
            return null;
        }

        public virtual string Error
        {
            get
            {
                if (_validator == null)
                {
                    _validator = CreateValidator();
                }
                if (_validator != null)
                {
                    var validationResults = _validator.Validate(this);
                    if (!validationResults.IsValid)
                    {
                        return Join(Environment.NewLine, validationResults.Errors.Select(x => x.ErrorMessage));
                    }
                }
                return null;
            }
        }
    }
}
