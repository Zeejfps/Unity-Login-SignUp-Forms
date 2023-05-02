using Login;
using LoginSignUpPage;
using Services;
using UnityEngine;

namespace UGUI
{
    public sealed class UGUILoginSignUpWidgetView : UGUIWidgetView<ILoginSignUpPageWidget>
    {
        [SerializeField] private UGUITabWidgetView m_LoginFormTabWidgetView;
        [SerializeField] private UGUITabWidgetView m_SignUpFormTabWidgetView;
        [SerializeField] private UGUILoginFormWidgetView m_LoginFormWidgetView;
        [SerializeField] private UGUISignUpFormWidgetView m_SignUpFormWidgetView;

        private LoginSignUpPageWidgetPresenter Presenter { get; set; }

        protected override void Start()
        {
            base.Start();
            var loginService = Z.Get<ILoginService>();
            var signUpService = Z.Get<ISignUpService>();
            var popupService = Z.Get<IPopupManager>();
            var loginSignUpPageWidget = Z.Get<ILoginSignUpPageWidget>();

            Presenter = new LoginSignUpPageWidgetPresenter(popupService, loginService, signUpService, loginSignUpPageWidget);
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
            Presenter.Dispose();
            Presenter = null;
            base.OnDestroy();
        }
    }
}