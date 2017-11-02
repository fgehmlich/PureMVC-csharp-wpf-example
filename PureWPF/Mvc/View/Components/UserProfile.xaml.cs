using PureWPF.Mvc.Model.Vo;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PureWPF.Mvc.View.Components
{
    /// <summary>
    /// Logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : UserControl
    {
        private UserVO m_user;
        public UserVO User
        {
            get { return m_user; }
        }
       


        public UserProfile()
        {
            InitializeComponent();
        }

        public void setFormMode()
        {
            bool state = User!=null;
            formGrid.IsEnabled = state;
        }

        public void ClearForm()
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new ClearFormDelegate(ClearForm));
                return;
            }

            m_user = null;
            formGrid.DataContext = null;
            firstName.Text = lastName.Text = email.Text = userName.Text = "";
        }
        private delegate void ClearFormDelegate();


        public void ShowUser(UserVO user)
        {

            if (user == null)
            {
                ClearForm();
            }
            else
            {
                m_user = user;
                formGrid.DataContext = user;
                firstName.Text = user.FirstName;
                lastName.Text = user.LastName;
                email.Text = user.Email;
                userName.Text = user.UserName;
                firstName.Focus();
     
            }
        }
        




        /// <summary>
        /// The update user event
        /// </summary>
        public event EventHandler UpdateUser;

        /// <summary>
        /// Fires the update user event
        /// </summary>
        /// <param name="args">The arguments for the event</param>
        protected virtual void OnUpdateUser(EventArgs args)
        {
            if (UpdateUser != null) UpdateUser(this, args);
        }
    
        protected virtual void SetUpdateUser()
        {
            OnUpdateUser(new EventArgs());
        }


        /// <summary>
        /// The cancel user event
        /// </summary>
        public event EventHandler CancelUser;

        /// <summary>
        /// Fires the cancel user event
        /// </summary>
        /// <param name="args">The arguments for the event</param>
        protected virtual void OnCancelUser(EventArgs args)
        {
            if (CancelUser != null) CancelUser(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SetCancelUser()
        {
            OnCancelUser(new EventArgs());
        }
        

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            int _id = m_user.Id;
            m_user = new UserVO(userName.Text, firstName.Text, lastName.Text, email.Text, _id);
            
            if (m_user.IsValid)
            {
                SetUpdateUser();
            }
            ClearForm();
    
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            SetCancelUser();
            ClearForm();
            setFormMode();
        }
        
    }
}
