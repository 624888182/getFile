﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.18063
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此原始程式碼由 wsdl 版本=4.0.30319.17929 自動產生。
// 
  namespace wsdlLib {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BBRYServiceSoap", Namespace="http://www.efoxconn.com/fusereport/webservice")]
      public partial class BBRYService : System.Web.Services.Protocols.SoapHttpClientProtocol
      {
        
        private System.Threading.SendOrPostCallback ExecuteInsertRecodeHeadOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExecuteGetDataByDNNOperationCompleted;
        
        /// <remarks/>
        public BBRYService() {
           // this.Url = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
        }
        
        /// <remarks/>
        public event ExecuteInsertRecodeHeadCompletedEventHandler ExecuteInsertRecodeHeadCompleted;
        
        /// <remarks/>
        public event ExecuteGetDataByDNNCompletedEventHandler ExecuteGetDataByDNNCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.efoxconn.com/fusereport/webservice/ExecuteInsertRecodeHead", RequestNamespace="http://www.efoxconn.com/fusereport/webservice", ResponseNamespace="http://www.efoxconn.com/fusereport/webservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ExecuteInsertRecodeHead(string strUserName, string strPassword, MyDictionaryOfStringString dic) {
            object[] results = this.Invoke("ExecuteInsertRecodeHead", new object[] {
                        strUserName,
                        strPassword,
                        dic});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginExecuteInsertRecodeHead(string strUserName, string strPassword, MyDictionaryOfStringString dic, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ExecuteInsertRecodeHead", new object[] {
                        strUserName,
                        strPassword,
                        dic}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndExecuteInsertRecodeHead(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteInsertRecodeHeadAsync(string strUserName, string strPassword, MyDictionaryOfStringString dic) {
            this.ExecuteInsertRecodeHeadAsync(strUserName, strPassword, dic, null);
        }
        
        /// <remarks/>
        public void ExecuteInsertRecodeHeadAsync(string strUserName, string strPassword, MyDictionaryOfStringString dic, object userState) {
            if ((this.ExecuteInsertRecodeHeadOperationCompleted == null)) {
                this.ExecuteInsertRecodeHeadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteInsertRecodeHeadOperationCompleted);
            }
            this.InvokeAsync("ExecuteInsertRecodeHead", new object[] {
                        strUserName,
                        strPassword,
                        dic}, this.ExecuteInsertRecodeHeadOperationCompleted, userState);
        }
        
        private void OnExecuteInsertRecodeHeadOperationCompleted(object arg) {
            if ((this.ExecuteInsertRecodeHeadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteInsertRecodeHeadCompleted(this, new ExecuteInsertRecodeHeadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.efoxconn.com/fusereport/webservice/ExecuteGetDataByDNN", RequestNamespace="http://www.efoxconn.com/fusereport/webservice", ResponseNamespace="http://www.efoxconn.com/fusereport/webservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ExecuteGetDataByDNN(string strUserName, string strPassword, string strDNN) {
            object[] results = this.Invoke("ExecuteGetDataByDNN", new object[] {
                        strUserName,
                        strPassword,
                        strDNN});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginExecuteGetDataByDNN(string strUserName, string strPassword, string strDNN, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ExecuteGetDataByDNN", new object[] {
                        strUserName,
                        strPassword,
                        strDNN}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndExecuteGetDataByDNN(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteGetDataByDNNAsync(string strUserName, string strPassword, string strDNN) {
            this.ExecuteGetDataByDNNAsync(strUserName, strPassword, strDNN, null);
        }
        
        /// <remarks/>
        public void ExecuteGetDataByDNNAsync(string strUserName, string strPassword, string strDNN, object userState) {
            if ((this.ExecuteGetDataByDNNOperationCompleted == null)) {
                this.ExecuteGetDataByDNNOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteGetDataByDNNOperationCompleted);
            }
            this.InvokeAsync("ExecuteGetDataByDNN", new object[] {
                        strUserName,
                        strPassword,
                        strDNN}, this.ExecuteGetDataByDNNOperationCompleted, userState);
        }
        
        private void OnExecuteGetDataByDNNOperationCompleted(object arg) {
            if ((this.ExecuteGetDataByDNNCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteGetDataByDNNCompleted(this, new ExecuteGetDataByDNNCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.efoxconn.com/fusereport/webservice")]
    public partial class MyDictionaryOfStringString {
        
        private MyKeyValuePairOfStringString[] itemsField;
        
        /// <remarks/>
        public MyKeyValuePairOfStringString[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.efoxconn.com/fusereport/webservice")]
    public partial class MyKeyValuePairOfStringString {
        
        private string keyField;
        
        private string valueField;
        
        /// <remarks/>
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void ExecuteInsertRecodeHeadCompletedEventHandler(object sender, ExecuteInsertRecodeHeadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteInsertRecodeHeadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteInsertRecodeHeadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void ExecuteGetDataByDNNCompletedEventHandler(object sender, ExecuteGetDataByDNNCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteGetDataByDNNCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteGetDataByDNNCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}