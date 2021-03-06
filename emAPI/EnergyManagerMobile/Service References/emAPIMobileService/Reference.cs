﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace EnergyManagerMobile.emAPIMobileService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="emAPIMobileService.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/getPropertiesForUser", ReplyAction="http://tempuri.org/IService1/getPropertiesForUserResponse")]
        System.IAsyncResult BegingetPropertiesForUser(int userId, System.AsyncCallback callback, object asyncState);
        
        string EndgetPropertiesForUser(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/getMetersForProperty", ReplyAction="http://tempuri.org/IService1/getMetersForPropertyResponse")]
        System.IAsyncResult BegingetMetersForProperty(int propertyId, System.AsyncCallback callback, object asyncState);
        
        string EndgetMetersForProperty(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/createMeterReading", ReplyAction="http://tempuri.org/IService1/createMeterReadingResponse")]
        System.IAsyncResult BegincreateMeterReading(string date, int reading, int meterId, int propertyId, System.AsyncCallback callback, object asyncState);
        
        void EndcreateMeterReading(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : EnergyManagerMobile.emAPIMobileService.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class getPropertiesForUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public getPropertiesForUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class getMetersForPropertyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public getMetersForPropertyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<EnergyManagerMobile.emAPIMobileService.IService1>, EnergyManagerMobile.emAPIMobileService.IService1 {
        
        private BeginOperationDelegate onBegingetPropertiesForUserDelegate;
        
        private EndOperationDelegate onEndgetPropertiesForUserDelegate;
        
        private System.Threading.SendOrPostCallback ongetPropertiesForUserCompletedDelegate;
        
        private BeginOperationDelegate onBegingetMetersForPropertyDelegate;
        
        private EndOperationDelegate onEndgetMetersForPropertyDelegate;
        
        private System.Threading.SendOrPostCallback ongetMetersForPropertyCompletedDelegate;
        
        private BeginOperationDelegate onBegincreateMeterReadingDelegate;
        
        private EndOperationDelegate onEndcreateMeterReadingDelegate;
        
        private System.Threading.SendOrPostCallback oncreateMeterReadingCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<getPropertiesForUserCompletedEventArgs> getPropertiesForUserCompleted;
        
        public event System.EventHandler<getMetersForPropertyCompletedEventArgs> getMetersForPropertyCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> createMeterReadingCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult EnergyManagerMobile.emAPIMobileService.IService1.BegingetPropertiesForUser(int userId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegingetPropertiesForUser(userId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string EnergyManagerMobile.emAPIMobileService.IService1.EndgetPropertiesForUser(System.IAsyncResult result) {
            return base.Channel.EndgetPropertiesForUser(result);
        }
        
        private System.IAsyncResult OnBegingetPropertiesForUser(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int userId = ((int)(inValues[0]));
            return ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).BegingetPropertiesForUser(userId, callback, asyncState);
        }
        
        private object[] OnEndgetPropertiesForUser(System.IAsyncResult result) {
            string retVal = ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).EndgetPropertiesForUser(result);
            return new object[] {
                    retVal};
        }
        
        private void OngetPropertiesForUserCompleted(object state) {
            if ((this.getPropertiesForUserCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.getPropertiesForUserCompleted(this, new getPropertiesForUserCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void getPropertiesForUserAsync(int userId) {
            this.getPropertiesForUserAsync(userId, null);
        }
        
        public void getPropertiesForUserAsync(int userId, object userState) {
            if ((this.onBegingetPropertiesForUserDelegate == null)) {
                this.onBegingetPropertiesForUserDelegate = new BeginOperationDelegate(this.OnBegingetPropertiesForUser);
            }
            if ((this.onEndgetPropertiesForUserDelegate == null)) {
                this.onEndgetPropertiesForUserDelegate = new EndOperationDelegate(this.OnEndgetPropertiesForUser);
            }
            if ((this.ongetPropertiesForUserCompletedDelegate == null)) {
                this.ongetPropertiesForUserCompletedDelegate = new System.Threading.SendOrPostCallback(this.OngetPropertiesForUserCompleted);
            }
            base.InvokeAsync(this.onBegingetPropertiesForUserDelegate, new object[] {
                        userId}, this.onEndgetPropertiesForUserDelegate, this.ongetPropertiesForUserCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult EnergyManagerMobile.emAPIMobileService.IService1.BegingetMetersForProperty(int propertyId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegingetMetersForProperty(propertyId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string EnergyManagerMobile.emAPIMobileService.IService1.EndgetMetersForProperty(System.IAsyncResult result) {
            return base.Channel.EndgetMetersForProperty(result);
        }
        
        private System.IAsyncResult OnBegingetMetersForProperty(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int propertyId = ((int)(inValues[0]));
            return ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).BegingetMetersForProperty(propertyId, callback, asyncState);
        }
        
        private object[] OnEndgetMetersForProperty(System.IAsyncResult result) {
            string retVal = ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).EndgetMetersForProperty(result);
            return new object[] {
                    retVal};
        }
        
        private void OngetMetersForPropertyCompleted(object state) {
            if ((this.getMetersForPropertyCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.getMetersForPropertyCompleted(this, new getMetersForPropertyCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void getMetersForPropertyAsync(int propertyId) {
            this.getMetersForPropertyAsync(propertyId, null);
        }
        
        public void getMetersForPropertyAsync(int propertyId, object userState) {
            if ((this.onBegingetMetersForPropertyDelegate == null)) {
                this.onBegingetMetersForPropertyDelegate = new BeginOperationDelegate(this.OnBegingetMetersForProperty);
            }
            if ((this.onEndgetMetersForPropertyDelegate == null)) {
                this.onEndgetMetersForPropertyDelegate = new EndOperationDelegate(this.OnEndgetMetersForProperty);
            }
            if ((this.ongetMetersForPropertyCompletedDelegate == null)) {
                this.ongetMetersForPropertyCompletedDelegate = new System.Threading.SendOrPostCallback(this.OngetMetersForPropertyCompleted);
            }
            base.InvokeAsync(this.onBegingetMetersForPropertyDelegate, new object[] {
                        propertyId}, this.onEndgetMetersForPropertyDelegate, this.ongetMetersForPropertyCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult EnergyManagerMobile.emAPIMobileService.IService1.BegincreateMeterReading(string date, int reading, int meterId, int propertyId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegincreateMeterReading(date, reading, meterId, propertyId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void EnergyManagerMobile.emAPIMobileService.IService1.EndcreateMeterReading(System.IAsyncResult result) {
            base.Channel.EndcreateMeterReading(result);
        }
        
        private System.IAsyncResult OnBegincreateMeterReading(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string date = ((string)(inValues[0]));
            int reading = ((int)(inValues[1]));
            int meterId = ((int)(inValues[2]));
            int propertyId = ((int)(inValues[3]));
            return ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).BegincreateMeterReading(date, reading, meterId, propertyId, callback, asyncState);
        }
        
        private object[] OnEndcreateMeterReading(System.IAsyncResult result) {
            ((EnergyManagerMobile.emAPIMobileService.IService1)(this)).EndcreateMeterReading(result);
            return null;
        }
        
        private void OncreateMeterReadingCompleted(object state) {
            if ((this.createMeterReadingCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.createMeterReadingCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void createMeterReadingAsync(string date, int reading, int meterId, int propertyId) {
            this.createMeterReadingAsync(date, reading, meterId, propertyId, null);
        }
        
        public void createMeterReadingAsync(string date, int reading, int meterId, int propertyId, object userState) {
            if ((this.onBegincreateMeterReadingDelegate == null)) {
                this.onBegincreateMeterReadingDelegate = new BeginOperationDelegate(this.OnBegincreateMeterReading);
            }
            if ((this.onEndcreateMeterReadingDelegate == null)) {
                this.onEndcreateMeterReadingDelegate = new EndOperationDelegate(this.OnEndcreateMeterReading);
            }
            if ((this.oncreateMeterReadingCompletedDelegate == null)) {
                this.oncreateMeterReadingCompletedDelegate = new System.Threading.SendOrPostCallback(this.OncreateMeterReadingCompleted);
            }
            base.InvokeAsync(this.onBegincreateMeterReadingDelegate, new object[] {
                        date,
                        reading,
                        meterId,
                        propertyId}, this.onEndcreateMeterReadingDelegate, this.oncreateMeterReadingCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override EnergyManagerMobile.emAPIMobileService.IService1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<EnergyManagerMobile.emAPIMobileService.IService1>, EnergyManagerMobile.emAPIMobileService.IService1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<EnergyManagerMobile.emAPIMobileService.IService1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BegingetPropertiesForUser(int userId, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = userId;
                System.IAsyncResult _result = base.BeginInvoke("getPropertiesForUser", _args, callback, asyncState);
                return _result;
            }
            
            public string EndgetPropertiesForUser(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("getPropertiesForUser", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BegingetMetersForProperty(int propertyId, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = propertyId;
                System.IAsyncResult _result = base.BeginInvoke("getMetersForProperty", _args, callback, asyncState);
                return _result;
            }
            
            public string EndgetMetersForProperty(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("getMetersForProperty", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BegincreateMeterReading(string date, int reading, int meterId, int propertyId, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[4];
                _args[0] = date;
                _args[1] = reading;
                _args[2] = meterId;
                _args[3] = propertyId;
                System.IAsyncResult _result = base.BeginInvoke("createMeterReading", _args, callback, asyncState);
                return _result;
            }
            
            public void EndcreateMeterReading(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("createMeterReading", _args, result);
            }
        }
    }
}
