using System.Collections.Generic;
using Bindings;
using Common.Widgets;
using Login;
using LoginForm;
using LoginSignUpPage;
using Services;
using SignUpForm;
using UnityEngine;
using Validators;

namespace UGUI
{
    public sealed class UGUILoginSignUpWidgetView : UGUIWidgetView<ILoginSignUpPageWidget>, IWidgetProvider
    {
        [SerializeField] private UGUITabWidgetView m_LoginFormTabWidgetView;
        [SerializeField] private UGUITabWidgetView m_SignUpFormTabWidgetView;
        [SerializeField] private UGUILoginFormWidgetView m_LoginFormWidgetView;
        [SerializeField] private UGUISignUpFormWidgetView m_SignUpFormWidgetView;

        private LoginSignUpPageWidgetPresenter Presenter { get; set; }
        private ILoginFormWidgetPresenter LoginFormWidgetPresenter { get; set; }
        private ISignUpFormWidgetPresenter SignUpFormWidgetPresenter { get; set; }
    
        protected override void Awake()
        {
            base.Awake();

            var loginService = Z.Get<ILoginService>();
            var signUpService = Z.Get<ISignUpService>();
            var popupService = Z.Get<IPopupManager>();

            var emailFieldWidget = new TextFieldWidget();
            m_IdToWidgetMap.Add("email-field", emailFieldWidget);

            var submitLoginFormButtonWidget = new ButtonWidget();
            m_IdToWidgetMap.Add("submit-login-form-button", submitLoginFormButtonWidget);

            var submitSignUpFormButtonWidget = new ButtonWidget();
            m_IdToWidgetMap.Add("submit-signup-form-button", submitSignUpFormButtonWidget);

            var passwordField = new PasswordFieldWidget();
            m_IdToWidgetMap.Add("password-field", passwordField);

            var confirmPasswordField = new PasswordFieldWidget();
            m_IdToWidgetMap.Add("confirm-password-field", confirmPasswordField);

            var rememberMeToggle = new ToggleWidget();
            m_IdToWidgetMap.Add("rememberme-toggle", rememberMeToggle);

            var usernameFieldWidget = new TextFieldWidget();
            m_IdToWidgetMap.Add("username-field", usernameFieldWidget);
            
            var loginFormWidget = new LoginFormWidget(emailFieldWidget, passwordField, rememberMeToggle, submitLoginFormButtonWidget);
            var signUpFormWidget = new SignUpFormWidget(emailFieldWidget, usernameFieldWidget, passwordField, confirmPasswordField, submitSignUpFormButtonWidget);
            
            var loginSignUpPageWidget = new LoginSignUpPageWidget(loginFormWidget, signUpFormWidget);

            var emailValidator = new RegexEmailValidator();
            var passwordValidators = new IPasswordValidator[]
            {
                new MinLengthPasswordValidator(3),
                new MinDigitsPasswordValidator(1),
                new MinUpperCaseCharactersPasswordValidator(1),
                new MinLowerCaseCharactersPasswordValidator(1),
                new MinSpecialCharactersPasswordValidator(1)
            };

            LoginFormWidgetPresenter = new LoginFormWidgetPresenter(popupService, loginService, emailValidator,
                loginSignUpPageWidget.LoginFormWidget);
            
            SignUpFormWidgetPresenter = new SignUpFormWidgetPresenter(signUpService, emailValidator, passwordValidators,
                loginSignUpPageWidget.SignUpFormWidget);
            
            Presenter = new LoginSignUpPageWidgetPresenter(loginSignUpPageWidget, LoginFormWidgetPresenter, SignUpFormWidgetPresenter);
        
            Model = loginSignUpPageWidget;
            Model.IsVisibleProperty.Set(true);
        }

        protected override void OnBindToModel(ILoginSignUpPageWidget model)
        {
            base.OnBindToModel(model);
            m_LoginFormWidgetView.Model = model.LoginFormWidget;
            m_SignUpFormWidgetView.Model = model.SignUpFormWidget;
            m_LoginFormTabWidgetView.Model = model.LoginFormTabWidget;
            m_SignUpFormTabWidgetView.Model = model.SignUpFormTabWidget;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift))
                Presenter.ProcessInputEvent(InputEvent.FocusNext);
            else if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
                Presenter.ProcessInputEvent(InputEvent.FocusPrevious);
            else if (Input.GetKeyDown(KeyCode.Return))
                Presenter.ProcessInputEvent(InputEvent.Submit);
        }

        protected override void OnDestroy()
        {
            LoginFormWidgetPresenter.Dispose();
            Presenter.Dispose();
            Presenter = null;
            base.OnDestroy();
        }

        private readonly Dictionary<string, IWidget> m_IdToWidgetMap = new();

        public TWidget Get<TWidget>(string widgetId) where TWidget : class, IWidget
        {
            if (m_IdToWidgetMap.TryGetValue(widgetId, out var widget) && widget is TWidget w)
                return w;
            return null;
        }
    }
}