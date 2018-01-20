using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsHelper.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<BindableObject>
    {
        // Important note: it works incorrectly if we change event name during the life cycle of the object.
        // To add this we have to add PropertyChanged handler to corresponding BindableProperty (EventName).

        #region Fields

        private Delegate handler = null;
        private EventInfo eventInfo = null;

        #endregion

        #region Bindable properties

        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty ConverterProperty =
            BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty ConverterParameterProperty =
            BindableProperty.Create("ConverterParameter", typeof(object), typeof(EventToCommandBehavior), null);

        #endregion

        #region Properties

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        public object ConverterParameter
        {
            get => GetValue(ConverterParameterProperty);
            set => SetValue(ConverterParameterProperty, value);
        }

        #endregion

        #region Methods

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent();
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            DeregisterEvent();
            base.OnDetachingFrom(bindable);
        }

        protected void RegisterEvent()
        {
            if (string.IsNullOrWhiteSpace(EventName)) return;
            
            eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);
            if (eventInfo == null)
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.", EventName));

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, handler);
        }

        protected void DeregisterEvent()
        {
            eventInfo?.RemoveEventHandler(AssociatedObject, handler);
        }

        protected virtual void OnEvent(object sender, object eventArgs)
        {
            if (Command == null) return;

            object resolvedParameter;
            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), ConverterParameter, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
                Command.Execute(resolvedParameter);
        }

        #endregion
    }
}
