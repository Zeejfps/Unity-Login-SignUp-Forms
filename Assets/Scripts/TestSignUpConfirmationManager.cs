using System;
using YADBF;

namespace Login
{
    public sealed class TestSignUpConfirmationManager : ISignUpConfirmationManager
    {
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<Action> ConfirmActionProp { get; } = new();
        public ObservableProperty<string> ConfirmationCodeTextProp { get; } = new();
    }
}