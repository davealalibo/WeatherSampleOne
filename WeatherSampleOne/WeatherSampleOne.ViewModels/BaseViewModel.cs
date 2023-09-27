using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentValidation.Results;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;

namespace WeatherSampleOne.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public ApiError NetworkError { get; set; }

        public bool HasErrors => Errors.Any();
        public bool HasError(string key) => Errors.ContainsKey(key);

        public Dictionary<string, string> Errors { get; private set; } = new Dictionary<string, string>();

        public void SetServiceErrors(ApiError NetworkError)
        {
            if (NetworkError != null)
            {
                if (NetworkError.ModelState != null)
                {
                    foreach (var key in NetworkError.ModelState?.Keys)
                    {
                        Errors[key] = NetworkError.ModelState[key]?.FirstOrDefault();
                    }
                }
                else if (!string.IsNullOrEmpty(NetworkError.Message))
                {
                    Errors["ErrorMessage"] = NetworkError.Message;
                }
            }
        }

        public void SetValidationErrors(ValidationResult validationResult)
        {
            Errors.Clear();
            foreach (var failure in validationResult.Errors)
            {
                if (Errors.ContainsKey(failure.PropertyName))
                    continue;
                Errors[failure.PropertyName] = failure.ErrorMessage;
            }
        }

        protected bool Validate<TModel>(TModel model)
            where TModel : IValidated
        {
            var validationResult = model.Validate();
            SetValidationErrors(validationResult);
            return validationResult.IsValid;
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            RaisePropertyChanged(false, property);
        }

        public bool IsModified { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(bool causesModification,
            [CallerMemberName] string property = "")
        {
            IsModified = IsModified || causesModification;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

