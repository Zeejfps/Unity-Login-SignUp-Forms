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

            var emailTextFieldWidget = new TextFieldWidget();
            m_IdToWidgetMap.Add("email-field", emailTextFieldWidget);
            
            var loginSignUpPageWidget = new LoginSignUpPageWidget(emailTextFieldWidget);

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