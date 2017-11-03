# PureMVC C#: WPF Example
An example how PureMVC can be implemented in C#

# Fundamentals
PureMVC is a lightweight framework for creating applications based upon the model–view–controller (MVC) design pattern. PureMVC is available for several programming languages, e.g. ActionScript 3, C#, Java, JavaScript, C++, Objective-C, etc.
and allows the development of modular applications (MultiCore-Version).

Model, View and Controller are represented by three singletons, which are managed by the Facade.

All acteurs are able to interact with each other by Notifictions.

##Start with the facade
Whenever you work with PureMVC, you must understand that coding always starts with the facade.
The facade is a layer that links the framework, the MVC code and Main class; In this case it is MainWindow.xaml.cs, which can be easily generated in our VisualStudio environment.

```csharp
using PureMVC.Patterns.Facade;
using System;

namespace PureWPF
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationFacade facade = (ApplicationFacade)Facade.GetInstance("ApplicationFacade", 
                                       () => new ApplicationFacade("ApplicationFacade"));
            facade.Start(this);
        }       
      
    }
}
```

## Setting up the ApplicationFacade
The next step requires the creation of an ApplicationFacade, which inherits from Facade.
This class is clearly structured and contains the following methods and properties:

- A list of constants to send notifications through the different tiers.
- A constructor that requires a key string. (This string is only required for the MultiCore version, to differentiate between the several modules.)
- An overwritten InitializeController method to register all commands.
- A startup method which triggers a notification, when the facade has been instantiated in our MainWindow class.

```csharp
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using PureWPF.Mvc.Controller.Macro;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF
{
    /// <summary>
    /// ApplicationFacade is a concrete facade. It manages commands, mediators and proxys.
    ///  Generally it will be instantiated if the application has been completely created.
    /// </summary>
    public class ApplicationFacade : Facade, IFacade
    {
        //Notification messages for communication between the different acteurs.
        public const string STARTAPP      = "StartApp";
        public const string APP_READY     = "ApplicationReady";
        public const string USER_SELECTED = "UserSelected";
        public const string NEW_USER      = "NewUser";
        public const string SAVE_USER     = "SafeUser";
        public const string REMOVE_USER   = "RemoveUser";

        /// <summary>
        /// Multiton ApplicationFacade factory method.
        /// </summary>
        public ApplicationFacade(string key) : base(key){}

        /// <summary>
        /// <b>initializeController</b> registers commands with the controller.
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            this.RegisterCommand(STARTAPP, () => new StartCommand());
            this.RegisterCommand(SAVE_USER, () => new UserCommand());
            this.RegisterCommand(REMOVE_USER, () => new UserCommand());
        }
        
        /// <summary>
        /// Application startup
        /// </summary>
        /// <param name="app"></param>
        public void Start(MainWindow app)
        {
            SendNotification(STARTAPP, app);
        }
    }
}
```

## The Start command
The start method dispatches a StartApp-notification which triggers the StartCommand.cs file.
This class is a a so called MacroCommand and is able to delegate tasks to referenced sub-commands.

```csharp
using PureMVC.Patterns.Command;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF.Mvc.Controller.Macro
{
    public class StartCommand : MacroCommand
    {
        public StartCommand()
        {
            AddSubCommand(() => new ModelCommand());
            AddSubCommand(() => new ViewCommand());
        }
    }
}
```

These sub-commands can be used to register the model and the view at the facade.
As you can see below:

### ModelCommand
```csharp
using PureWPF.PureMVC.Patterns.Command;
using System;
using PureWPF.Mvc.Model;

namespace PureWPF.Mvc.Controller.Simple
{
    public class ModelCommand : MapCommand
    {

        public ModelCommand()
        {
            MapHandler(ApplicationFacade.STARTAPP, customHandler);
        }

        private void customHandler(string arg1, object arg2, string arg3)
        {
            Console.WriteLine("ModelCommand executed");
            Facade.RegisterProxy(new UserProxy());
        }
    }
}
```


### ViewCommand
```csharp
using PureWPF.Mvc.View;
using PureWPF.PureMVC.Patterns.Command;
using System;

namespace PureWPF.Mvc.Controller.Simple
{
  
    public class ViewCommand : MapCommand
    {

        public ViewCommand()
        {
            MapHandler(ApplicationFacade.STARTAPP, customHandler);
        }

        private void customHandler(string s, Object o, string arg3)
        {

            Console.WriteLine("ViewCommand executed ");
            MainWindow window = (MainWindow) o;

            Facade.RegisterMediator(new UserListMediator(window.UserList));
            Facade.RegisterMediator(new UserProfileMediator(window.UserProfile));
            //In some cases it might be useful to inform other acteurs that the application
            //has been successfully created.
            SendNotification(ApplicationFacade.APP_READY,null,null);
        }
    }
}
```

## Creating a proxy
A proxy is a representation of the model in PureMVC. These classes can be used to manage all data transactions.
A proxy can be used to retrieve data from a remote database, data from an XML file or to store entries only at runtime.
However, all records should be mapped in value objects to ensure an easy data access at runtime.

### UserProxy.cs
```csharp
using PureMVC.Interfaces;
using PureMVC.Patterns.Proxy;
using PureWPF.Mvc.Model.Vo;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PureWPF.Mvc.Model
{
    /// <summary>
    /// UserProxy manages all UserVO instances.
    /// For test purposes four objects are added at runtime.
    /// </summary>
    public class UserProxy : Proxy, IProxy
    {
        public new const string NAME = "UserProxy";
        //static iterating variable.
        private static int iterator = 1;


        public UserProxy() : base(NAME, new ObservableCollection<UserVO>())
        {
            AddItem(new UserVO("pgriffin", "Peter", "Griffin", "peter@web123.com"));
            AddItem(new UserVO("lgriffin", "Lois", "Griffin", "lgriffin@web123.com"));
            AddItem(new UserVO("jswanson", "Joe", "Swanson", "jswanson@web123.com"));
            AddItem(new UserVO("cbrown", "Cleveland", "Brown", "cleveland@web123.com"));
        }
        /// <summary>
        /// Returns a list with UserVOs
        /// </summary>
        public IList<UserVO> Users
        {
            get { return (IList<UserVO>) Data; }
        }
        /// <summary>
        /// Adds an user to the Users list.
        /// </summary>
        /// <param name="user"></param>
        public void AddItem(UserVO user)
        {
            user.Id = iterator++;
            Users.Add(user);
        }
        /// <summary>
        /// Updates existing instances.
        /// </summary>
        /// <param name="user"></param>
        public void UpdateItem(UserVO user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName.Equals(user.UserName))
                {
                    Users[i] = user;
                    break;
                }
            }

        }
        /// <summary>
        /// Deletes an existing instance
        /// </summary>
        /// <param name="user"></param>
        public void DeleteItem(UserVO user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName.Equals(user.UserName))
                {
                    Users.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
```

### UserVO.cs
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureWPF.Mvc.Model.Vo
{
    /// <summary>
    /// UserVO maps the users data to an object.
    /// These objects can be managed by UserProxy.
    /// </summary>
    public class UserVO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }


        public string GivenName => LastName + ", " + FirstName;



        public bool IsValid
        {
            get 
              { 
                return !string.IsNullOrEmpty(UserName) && 
                       !string.IsNullOrEmpty(FirstName) && 
                       !string.IsNullOrEmpty(LastName); 
              }
        }

        public UserVO(string userName, string firstName, string lastName, string email, int id = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;

        }
    }
}
```

## Creating a Mediator
To ensure loose coupling, views interact with the framework only in a indirect way.
This can be realized by the usage of Mediators. These mediators listen for events that are triggered by the view component and send a notification to the framework, if it is necessary.

A mediator class should contain the following elements:

- A NAME constant to identify a mediator. Without this constant a mediator can not be retrieved by PureMVC. 
- The contstructor with view parameter to map the corresponding view with its mediator.
- A getter method that returns the view in appropriate way.
- An overwritten OnRegister method to add event listeners to the view getter.
- If the view is not permanent you should also call OnRemove to get rid of the view components and its references. Otherwise the object and the mediator cannot properly garbage collected.
- And if the view dispatches any events, you should also add a handler method to send a notification to another mediator or to a command.

```csharp

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureWPF.Mvc.Model;
using PureWPF.Mvc.Model.Vo;
using PureWPF.Mvc.View.Components;
using PureWPF.PureMVC.Patterns;

namespace PureWPF.Mvc.View
{

    public class UserListMediator : MapMediator
    {
        public new const string NAME = "UserListMediator";

        private UserProxy userProxy;
        private UserList view => (UserList)ViewComponent;

        public UserListMediator(UserList view):base(NAME, view){}
        public override void OnRegister()
        {
            base.OnRegister();
            userProxy = (UserProxy)Facade.RetrieveProxy(UserProxy.NAME);
            view.SelectUser += new EventHandler(userList_SelectUser);
            //...           
            view.LoadUsers(userProxy.Users);
        }


        private void userList_SelectUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.USER_SELECTED, view.SelectedUser, null);
            Console.WriteLine("User selected");
        }

        //....
    }
}
```


